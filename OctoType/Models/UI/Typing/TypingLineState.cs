using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using OctoType.Domain.Typing;
using OctoType.Interfaces;

namespace OctoType.Models.UI.Typing;

public partial class TypingLineState : ObservableObject
{
    public TypingLine Model { get; }
    private readonly ITypingTheme _theme;
    public ObservableCollection<TypingCharState> Characters { get; } = [];

    public TypingLineState(ITypingTheme theme, TypingLine model)
    {
        _theme = theme;
        Model = model;

        Build();
    }

    private void Build()
    {
        Characters.Clear();

        foreach (TypingChar c in Model.Characters)
        {
            Characters.Add(new TypingCharState(_theme, c));
        }
    }
}
