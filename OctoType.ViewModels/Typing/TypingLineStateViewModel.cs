using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using AppInterfacesTyping = OctoType.Application.Interfaces.Typing;
using DomainTyping = OctoType.Domain.Typing;

namespace OctoType.ViewModels.Typing;

public partial class TypingLineStateViewModel : ObservableObject
{
    public DomainTyping.TypingLine Model { get; }
    private readonly AppInterfacesTyping.ITypingTheme _theme;
    public ObservableCollection<TypingCharStateViewModel> Characters { get; } = [];

    public TypingLineStateViewModel(
        AppInterfacesTyping.ITypingTheme theme,
        DomainTyping.TypingLine model)
    {
        _theme = theme;
        Model = model;

        Build();
    }

    private void Build()
    {
        Characters.Clear();

        foreach (DomainTyping.TypingChar c in Model.Characters)
        {
            Characters.Add(new TypingCharStateViewModel(_theme, c));
        }
    }
}
