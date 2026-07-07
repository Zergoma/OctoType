using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;


using AppInterfacesTyping = OctoType.Application.Interfaces.Typing;
using DomainTyping = OctoType.Domain.Typing;
using DomainAnalysis = OctoType.Domain.Typing.Analysis;

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

    public Dictionary<char, DomainAnalysis.CharStats> GetLineCharStats()
    {
        Dictionary<char, DomainAnalysis.CharStats> dictMetric = [];

        foreach (TypingCharStateViewModel item in Characters)
        {
            if (dictMetric.TryGetValue(item.Character, out DomainAnalysis.CharStats? metric))
            {
                metric.NbOccurence++;
                metric.RespondeTime = item.ResponseTime;

                if (item.Errors.Count > 0)
                {
                    metric.NbCharError++;
                    metric.RealErrors.AddRange(item.Errors);
                }
            }
            else
            {
                DomainAnalysis.CharStats charMetric = new ();

                charMetric.RespondeTime = item.ResponseTime;


                if (item.Errors.Count != 0)
                {
                    charMetric.NbCharError = 1;
                    charMetric.RealErrors = [.. item.Errors];
                }

                dictMetric[item.Character] = charMetric;
            }
        }
        return dictMetric;
    }
}
