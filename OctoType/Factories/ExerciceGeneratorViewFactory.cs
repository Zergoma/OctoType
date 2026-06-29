using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Application.Interfaces.Typing;
using OctoType.MVVM.Views;
using OctoType.ViewModels.Exercices;

namespace OctoType.Factories;

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
        ExerciceGeneratorViewModel typingviewmodel
            = new(
                _pseudoWordBatchGenerator,
                _typingExerciceManager,
                _typingExercicePersistence,
                _saveUseCase,
                _userKeyboardPreferenceService,
                _generationTypeSource,
                _keyboardLayoutAvailableService,
                _languageAvailableService);


        await typingviewmodel.InitializeAsync();

        ExerciceGeneratorView typingView = new(typingviewmodel);

        return Result<ContentPage>
            .Ok(typingView);
    }
}