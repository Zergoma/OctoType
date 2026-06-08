using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using AppInterfaces = OctoType.Application.Interfaces;
using OctoType.Interfaces;
using OctoType.Models.UI.Typing;
using OctoType.Domain.Enums;

namespace OctoType.MVVM.ViewModels;

public partial class TypingViewModel : ObservableObject
{
    public event Action<TypingLineState?>? CurrentLineChanged;

    private readonly AppInterfaces.IStringsProviderService _stringsProviderService;
    private readonly AppInterfaces.IInputCharMapperService _charMapper;
    private readonly ITypingLineStateFactory _typingLineFactory;

    public TypingViewModel(
        AppInterfaces.IStringsProviderService stringsProviderService,
        AppInterfaces.IInputCharMapperService charMapper,
        ITypingLineStateFactory typingLineFactory)
    {
        _stringsProviderService = stringsProviderService;
        _charMapper = charMapper;
        _typingLineFactory = typingLineFactory;
    }
    public ObservableCollection<TypingLineState> Lines { get; } = [];


    [ObservableProperty]
    public partial TypingLineState? CurrentLine { get; set; } = null;

    partial void OnCurrentLineChanged(TypingLineState? value)
    {
        CurrentLineChanged?.Invoke(value);
    }


    private TypingCharState? CurrentCharacter => CurrentLine?.Current;


    public async Task LoadTextAsync()
    {
        Lines.Clear();

        string[] dataLines =
            [.. (await _stringsProviderService.GetStringsAsync())];

        ArgumentOutOfRangeException.ThrowIfLessThan(dataLines.Length, 1);

        foreach (string line in dataLines)
        {
            TypingLineState lineVm = _typingLineFactory.Create(line);
            Lines.Add(lineVm);
        }

        CurrentLine = Lines.First();
        CurrentLine.StartLine();
    }

    private TypingLineState? NextLine(TypingLineState? currentLine)
    {
        if(currentLine == null || Lines == null)
            return null;

        int currentLineIdx = Lines.IndexOf(currentLine);
        if (currentLineIdx == -1)
            return null;

        int nextIdx = currentLineIdx + 1;
        if (nextIdx >= Lines.Count)
            return null;

        return Lines[nextIdx]; ;
    }

    private bool GoToNextLine()
    {
        CurrentLine?.EndLine();
        TypingLineState? nextLine = NextLine(CurrentLine);

        CurrentLine = nextLine;
        CurrentLine?.StartLine();

        return CurrentLine != null;
    }

    public TypingStatus ProcessInput(char input)
    {
        if (CurrentCharacter == null || CurrentLine == null)
        {
            return TypingStatus.Ended;
        }

        bool success = CurrentCharacter.ChallengeValue(_charMapper.Map(input));
        
        if (success)
        {
            if(!CurrentLine.MoveToNextCharacter())
            {
                if (!GoToNextLine())
                {
                    return TypingStatus.Ended;
                }
            }
        }

        return TypingStatus.InProgress;
    }
}
