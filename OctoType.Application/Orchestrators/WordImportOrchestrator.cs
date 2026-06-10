using Microsoft.Extensions.Logging;

using OctoType.Application.Interfaces;
using OctoType.Application.Services;
using OctoType.Application.ValueObjects;
using OctoType.Domain.Entities;
using OctoType.Domain.Enums;
using OctoType.Domain.Models;

namespace OctoType.Application.Orchestrators;

public class WordImportOrchestrator : IWordImportServiceOrchestrator
{
    private readonly IDactyloRepository _repository;
    private readonly IWordBatchProcessorService _processor;
    private readonly IWordStreamReader _wordStreamingService;
    private readonly ILogger<WordImportOrchestrator> _logger;

    private const int BatchSize = 2000;

    public WordImportOrchestrator(
        IDactyloRepository dactyloRepository,
        IWordBatchProcessorService processor,
        IWordStreamReader wordStreamingService,
        ILogger<WordImportOrchestrator> logger)
    {
        _repository = dactyloRepository;
        _processor = processor;
        _wordStreamingService = wordStreamingService;
        _logger = logger;
    }

    public async Task ImportAsync(
        string filePath,
        string languageCode,
        KeyboardLayout layout)
    {
        Dictionary<string, int> batch = new(StringComparer.OrdinalIgnoreCase);

        HashSet<string> NoMapWords = [];

        WordSearchCriteria searchCriteria =
            new WordQueryBuilder()
            .WithLanguages(languageCode)
            .Build();

        Dictionary<string, Word> existingWords =
            (await _repository.SearchAsync(searchCriteria))
            .ToDictionary(w => w.Text);


        await foreach (string word in _wordStreamingService.ReadWordsAsync(filePath))
        {
            if (NoMapWords.Contains(word))
                continue;

            if (!batch.TryAdd(word, 1))
                batch[word]++;

            if (batch.Count >= BatchSize)
            {
                await FlushBatch(batch, existingWords, languageCode, layout, NoMapWords);
                batch.Clear();
            }
        }

        if (batch.Count > 0)
        {
            await FlushBatch(batch, existingWords, languageCode, layout, NoMapWords);
        }
    }

    private async Task FlushBatch(
        Dictionary<string, int> batch,
        Dictionary<string, Word> existingWords,
        string languageCode,
        KeyboardLayout layout,
        HashSet<string> NoMapWords)
    {
        WordProcessResult result =
            _processor.Process(batch, existingWords, languageCode, layout);

        // add the newly added words to the dictionnary
        foreach (Word w in result.NewWords)
        {
            existingWords[w.Text] = w;
        }
        _logger.LogInformation($"Add {result.NewWords.Length} new word(s)");

        // Keep in-memory txt that failed
        foreach (string txt in result.NoMapWords)
        {
            NoMapWords.Add(txt);
            _logger.LogWarning($"Analysis failed for text {txt} on layout {layout}");
        }

        // enforce via parameter name because same type
        await _repository.PersistWordsAsync(
            newWords: result.NewWords, 
            updatedWords: result.UpdatedWords);
    }
}


