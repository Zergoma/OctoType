using XyloType.MVVM.Views;
using XyloType.ViewModels.ExercicesGenerator;

using XyloType.Application;
using XyloType.Application.Interfaces;
using XyloType.Application.Interfaces.Typing;

namespace XyloType.Factories;

public class ExerciceGeneratorViewFactory : IExerciceGeneratorViewFactory
{
    private readonly IPseudoWordBatchGenerator _pseudoWordBatchGenerator;
    private readonly ITypingExercicesManager _typingExerciceManager;
    private readonly ITypingExercicesStorage _typingExercicePersistence;
    private readonly ISaveTypingExerciceUseCase _saveUseCase;
    private readonly IUserKeyboardLayoutPreferenceService _userKeyboardPreferenceService;


    private readonly IGenerationTypeSourceAvailableService _generationTypeSource;
    private readonly IKeyBoardLayoutAvailableService _keyboardLayoutAvailableService;
    private readonly ILanguageAvailableService _languageAvailableService;

    public ExerciceGeneratorViewFactory(
        IPseudoWordBatchGenerator pseudoWordBatchGenerator,
        ITypingExercicesManager typingExerciceManager,
        ITypingExercicesStorage typingExercicePersistence,
        ISaveTypingExerciceUseCase saveUseCase,
        IUserKeyboardLayoutPreferenceService userKeyboardPreferenceService,

        IGenerationTypeSourceAvailableService generationTypeSource,
        IKeyBoardLayoutAvailableService keyboardLayoutAvailableService,
        ILanguageAvailableService languageAvailableService)
    {
        _pseudoWordBatchGenerator = pseudoWordBatchGenerator;
        _typingExerciceManager = typingExerciceManager;
        _typingExercicePersistence = typingExercicePersistence;
        _saveUseCase = saveUseCase;
        _userKeyboardPreferenceService = userKeyboardPreferenceService;
        _generationTypeSource = generationTypeSource;
        _keyboardLayoutAvailableService = keyboardLayoutAvailableService;
        _languageAvailableService = languageAvailableService;
    }


    public async Task<Result<ContentPage>> CreateExerciceGeneratorView()
    {
        ExerciceGeneratorViewModel exerciceGeneratorViewmodel
            = new(
                _pseudoWordBatchGenerator,
                _typingExerciceManager,
                _typingExercicePersistence,
                _saveUseCase,
                _generationTypeSource,
                _keyboardLayoutAvailableService,
                _languageAvailableService);


        Result<int> userPreferenceKeyboardCodeResult = _userKeyboardPreferenceService.GetKeyboardType();
        if (!userPreferenceKeyboardCodeResult.Success)
        {
            return Result<ContentPage>
                .Fail(userPreferenceKeyboardCodeResult.Error);
        }

        await exerciceGeneratorViewmodel.InitializeAsync(userPreferenceKeyboardCodeResult.GetValue);

        ExerciceGeneratorView typingView = new(exerciceGeneratorViewmodel);

        return Result<ContentPage>
            .Ok(typingView);
    }

    public async Task<Result<ContentPage>> CreateExerciceUpdaterView(Guid exerciceToUpdate)
    {
        ExerciceGeneratorViewModel exerciceGeneratorViewmodel
            = new(
                _pseudoWordBatchGenerator,
                _typingExerciceManager,
                _typingExercicePersistence,
                _saveUseCase,
                _generationTypeSource,
                _keyboardLayoutAvailableService,
                _languageAvailableService);


        Result<int> userPreferenceKeyboardCodeResult = _userKeyboardPreferenceService.GetKeyboardType();
        if (!userPreferenceKeyboardCodeResult.Success)
        {
            return Result<ContentPage>
                .Fail(userPreferenceKeyboardCodeResult.Error);
        }

        await exerciceGeneratorViewmodel.InitFromExercice(exerciceToUpdate, userPreferenceKeyboardCodeResult.GetValue);

        ExerciceGeneratorView typingView = new(exerciceGeneratorViewmodel);

        return Result<ContentPage>
            .Ok(typingView);
    }
}