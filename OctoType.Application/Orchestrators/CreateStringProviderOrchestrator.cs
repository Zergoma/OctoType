using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Application.Models.Typing;
using OctoType.Application.Models.Typing.Exercices;
using OctoType.Application.ValueObjects;

namespace OctoType.Application.Orchestrators;

public class CreateStringProviderOrchestrator : ICreateStringProviderOrchestrator
{
    private readonly IPseudoWordBatchGenerator _pseudoWordBatchGenerator;
    private readonly ITypingExerciseWordNumberService _typingExerciceWordNumberService;
    private readonly ITypingExerciseLineNumberService _typingExerciceLineNumberService;
    private readonly IEditorSplitCharProvider _editorSplitCharProvider;

    public CreateStringProviderOrchestrator(
        IPseudoWordBatchGenerator pseudoWordBatchGenerator,
        ITypingExerciseWordNumberService typingExerciceWordNumberService,
        ITypingExerciseLineNumberService typingExerciceLineNumberService,
        IEditorSplitCharProvider editorSplitCharProvider)
    {
        _pseudoWordBatchGenerator = pseudoWordBatchGenerator;
        _typingExerciceWordNumberService = typingExerciceWordNumberService;
        _typingExerciceLineNumberService = typingExerciceLineNumberService;
        _editorSplitCharProvider = editorSplitCharProvider;
    }

    public Result<IStringsProvider> Create(
        TypingExercise exercice,
        KeyBoardLayoutDto selectedKeyboard)
    {
        List<TypingExerciseConfiguration> selectedKeyboardTypingExerciceConfigurations
            = [.. exercice.ExerciceConfigs.Where(x => x.KeyboardLayout.KeyBoardCode == selectedKeyboard.KeyBoardCode)];

        if (selectedKeyboardTypingExerciceConfigurations.Count == 0)
        {
            return Result<IStringsProvider>
                .Fail($"No configuration found for keyboard type {selectedKeyboard.KeyBoardHumanFriendly}");
        }

        TypingExerciseConfiguration? staticConfiguration
             = selectedKeyboardTypingExerciceConfigurations.FirstOrDefault(x => x.TextData.StaticTextData != null);

        if (staticConfiguration != null)
        {
            return Result<IStringsProvider>
                .Ok(new TypingExerciceStaticDataProducer(staticConfiguration, _editorSplitCharProvider));
        }

        TypingExerciseConfiguration? dynamicPseudoWordConfiguration
             = selectedKeyboardTypingExerciceConfigurations.FirstOrDefault(x => x.TextData.DynamicTextData?.GeneratedTypeSource == GeneratedTypeSource.PseudoWords);

        if (dynamicPseudoWordConfiguration != null)
        {
            return Result<IStringsProvider>
                .Ok(new TypingExerciceDynamicPseudoWordsProducer(
                    dynamicPseudoWordConfiguration,
                    _pseudoWordBatchGenerator,
                    _typingExerciceWordNumberService,
                    _typingExerciceLineNumberService
                    ));
        }


        TypingExerciseConfiguration? dynamicWordConfiguration
             = selectedKeyboardTypingExerciceConfigurations.FirstOrDefault(x => x.TextData.DynamicTextData?.GeneratedTypeSource == GeneratedTypeSource.Words);
        if (dynamicWordConfiguration != null)
        {
            // TODO
            // need db access
            return Result<IStringsProvider>
                .Fail("not yet implemented");

            //return Result<IStringsProvider>
            //    .Ok(new TypingExerciceDynamicPseudoWordsProducer(
            //        dynamicPseudoWordConfiguration,
            //        _pseudoWordBatchGenerator,
            //        _typingExerciceWordNumberService
            //        ));
        }

        return Result<IStringsProvider>
            .Fail("No static or dynamic configuration found");
    }
}

public class TypingExerciceStaticDataProducer : IStringsProvider
{
    private readonly TypingExerciseConfiguration _exerciceConfiguration;
    private readonly char _splitChar;

    public TypingExerciceStaticDataProducer(
        TypingExerciseConfiguration exerciceConfiguration,
        IEditorSplitCharProvider editorSplitCharProvider)
    {
        _exerciceConfiguration = exerciceConfiguration;
        _splitChar = editorSplitCharProvider.GetSplitCharacter();
    }

    public async Task<Result<IEnumerable<string>>> GetStringsAsync()
    {
        string generatedRawStored = _exerciceConfiguration.TextData.StaticTextData!.GeneratedText;

        string[] rawList = generatedRawStored.Split(_splitChar);
        List<string> filteredLines = [];
        foreach (var line in rawList)
        {
            // For stability, we filtred the control char
            string cleaned = new([.. line.Where(c => !char.IsControl(c))]);
            if(string.IsNullOrWhiteSpace(cleaned))
            {
                continue;
            }
            filteredLines.Add(cleaned);
        }

        return Result<IEnumerable<string>>
            .Ok(filteredLines);
    }
}

public class TypingExerciceDynamicPseudoWordsProducer : IStringsProvider
{
    private readonly TypingExerciseConfiguration _exerciceConfiguration;
    private readonly IPseudoWordBatchGenerator _pseudoWordBatchGenerator;
    private readonly ITypingExerciseWordNumberService _typingExerciceWordNumberService;
    private readonly ITypingExerciseLineNumberService _typingExerciceLineNumberService;

    public TypingExerciceDynamicPseudoWordsProducer(
        TypingExerciseConfiguration exerciceConfiguration,
        IPseudoWordBatchGenerator pseudoWordBatchGenerator,
        ITypingExerciseWordNumberService typingExerciceWordNumberService,
        ITypingExerciseLineNumberService typingExerciceLineNumberService)
    {
        _pseudoWordBatchGenerator = pseudoWordBatchGenerator;
        _typingExerciceWordNumberService = typingExerciceWordNumberService;
        _exerciceConfiguration = exerciceConfiguration;
        _typingExerciceLineNumberService = typingExerciceLineNumberService;
    }

    public async Task<Result<IEnumerable<string>>> GetStringsAsync()
    {
        TypingTextData typingTextData = _exerciceConfiguration.TextData;


        List<string> all = [];

        for (int i = 0; i < _typingExerciceLineNumberService.LineNumber; i++)
        {
            Result<List<string>> generatedPseudoWordResult =
                _pseudoWordBatchGenerator.Generate(
                    _typingExerciceWordNumberService.ItemNumber,
                    new PseudoWordOptions()
                    {
                        AllowedChars = typingTextData.AllowedLetters,
                        MinLength = typingTextData.DynamicTextData!.LengthMin,
                        MaxLength = typingTextData.DynamicTextData!.LengthMax,
                    });

            if (!generatedPseudoWordResult.Success)
            {
                return Result<IEnumerable<string>>
                    .Fail(generatedPseudoWordResult.Error);
            }

            all.Add(string.Join(' ', generatedPseudoWordResult.GetValue));
        }



        return Result<IEnumerable<string>>
            .Ok(all);
    }


}


public class TypingExerciceDynamicWords
{
    public Result<string> GetData { get; set; }

    // repository à la bdd requit => request la bdd avec les critères
}