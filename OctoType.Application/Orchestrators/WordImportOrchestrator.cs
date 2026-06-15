using Microsoft.Extensions.Logging;

using OctoType.Application.Interfaces;
using OctoType.Application.Models;
using OctoType.Application.ValueObjects;
using OctoType.Domain.Entities;

namespace OctoType.Application.Orchestrators;

public class WordImportOrchestrator : IWordImportOrchestrator
{
    private readonly IDactyloRepository _repository;
    private readonly IWordBatchProcessorOrchestrator _wordBatchProcessorOrchestrator;
    private readonly IWordStreamReader _wordStreamingService;
    private readonly ILogger<WordImportOrchestrator> _logger;

    private const int BatchSize = 2000;

    public WordImportOrchestrator(
        IDactyloRepository dactyloRepository,
        IWordBatchProcessorOrchestrator processor,
        IWordStreamReader wordStreamingService,
        ILogger<WordImportOrchestrator> logger)
    {
        _repository = dactyloRepository;
        _wordBatchProcessorOrchestrator = processor;
        _wordStreamingService = wordStreamingService;
        _logger = logger;
    }

    public async Task<Result<bool>> ImportAsync(
        string filePath,
        string languageCode,
        IKeyboardKeysLocator layout)
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
                var resuProcess = await FlushBatch(batch, existingWords, languageCode, layout, NoMapWords);
                batch.Clear();
                if(!resuProcess.Success)
                {
                    return Result<bool>
                        .Fail(resuProcess.Error);
                }
            }
        }

        if (batch.Count > 0)
        {
            await FlushBatch(batch, existingWords, languageCode, layout, NoMapWords);
        }

        return Result<bool>
            .Ok(true);
    }

    private async Task<Result<bool>> FlushBatch(
        Dictionary<string, int> batch,
        Dictionary<string, Word> existingWords,
        string languageCode,
        IKeyboardKeysLocator layout,
        HashSet<string> NoMapWords)
    {
        Result<WordProcessResult> resultProcess =
            _wordBatchProcessorOrchestrator.Process(batch, existingWords, languageCode, layout);

        if (!resultProcess.Success)
        {
            return Result<bool>
                .Fail(resultProcess.Error);
        }

        WordProcessResult result = resultProcess.Value!;

        // add the newly added words to the dictionnary
        foreach (Word w in result.NewWords)
        {
            existingWords[w.Text] = w;
        }
        _logger.LogInformation(
            "Add {NewWordsCount} new word(s)",
            result.NewWords.Length);

        // Keep in-memory txt that failed
        foreach (string txt in result.NoMapWords)
        {
            NoMapWords.Add(txt);
            _logger.LogWarning(
                "Analysis failed for text {FailedTxt} on layout {Layout}",
                txt,
                layout);
        }

        // enforce via parameter name because same type
        await _repository.PersistWordsAsync(
            newWords: result.NewWords,
            updatedWords: result.UpdatedWords);

        return Result<bool>
            .Ok(true);
    }
}


