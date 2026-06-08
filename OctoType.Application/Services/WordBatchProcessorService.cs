using Microsoft.Extensions.Logging;

using OctoType.Application.Interfaces;
using OctoType.Application.ValueObjects;
using OctoType.Domain.Entities;
using OctoType.Domain.Enums;

namespace OctoType.Application.Services;

public sealed class WordBatchProcessorService : IWordBatchProcessorService
{
    private readonly IKeyboardAnalyzerService _keyboardAnalyzerService;

    public WordBatchProcessorService(
        IKeyboardAnalyzerService keyboardAnalyzerService)
    {
        _keyboardAnalyzerService = keyboardAnalyzerService;
    }

    public WordProcessResult Process(
        IReadOnlyDictionary<string, int> batch,
        IReadOnlyDictionary<string, Word> existingWords,
        string languageCode,
        KeyboardLayout layout)
    {
        List<Word> newWords = [];
        List<Word> updatedWords = [];
        List<string> noMapWords = [];

        foreach ((string text, int count) in batch)
        {
            if (noMapWords.Contains(text))
                continue;

            if (existingWords.TryGetValue(text, out Word? word))
            {
                word.OccurrenceCount += count;

                if (!word.AnalyseExists(layout))
                {
                    Result<WordAnalysis> resu = EnsureAnalysis(text, layout);
                    if (resu.Success)
                    {
                        word.AddAnalysis(resu.Value!);
                    }
                    else
                    {
                        // word exists but no map for this layout
                        // blacklist it
                        noMapWords.Add(text);
                    }
                }


                updatedWords.Add(word);
                continue;
            }

            WordAnalysis? analysis =
                _keyboardAnalyzerService.Analyze(text, layout);

            if (analysis is null)
            {
                noMapWords.Add(text);
                continue;
            }

            Word newWord = new()
            {
                Text = text,
                LanguageCode = languageCode,
                Length = text.Length,
                OccurrenceCount = count
            };

            newWord.AddAnalysis(analysis);
            newWords.Add(newWord);
        }

        return new WordProcessResult(
            NewWords: [.. newWords],
            UpdatedWords: [.. updatedWords],
            NoMapWords: [.. noMapWords]);
    }

    private Result<WordAnalysis> EnsureAnalysis(
        string text,
        KeyboardLayout layout)
    {
        WordAnalysis? analysis =
            _keyboardAnalyzerService.Analyze(text, layout);

        if (analysis is null)
            return Result<WordAnalysis>
                .Fail($"""Analysis failed for text : {text} on layout: {layout}""");

        return Result<WordAnalysis>
            .Ok(analysis);
    }
}