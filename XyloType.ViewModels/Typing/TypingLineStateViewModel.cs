using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using XyloType.Domain.Typing;
using XyloType.Domain.Typing.Analysis;
using XyloType.Application.Interfaces.Typing;

namespace XyloType.ViewModels.Typing;

public partial class TypingLineStateViewModel : ObservableObject
{
    public TypingLine Model { get; }
    private readonly ITypingTheme _theme;
    public ObservableCollection<TypingCharStateViewModel> Characters { get; } = [];

    public TypingLineStateViewModel(
        ITypingTheme theme,
        TypingLine model)
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
            Characters.Add(new TypingCharStateViewModel(_theme, c));
        }
    }

    public Dictionary<char, CharStats> GetLineCharStats()
    {
        Dictionary<char, CharStats> dictMetric = [];

        foreach (TypingCharStateViewModel item in Characters)
        {
            if (dictMetric.TryGetValue(item.Character, out CharStats? metric))
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
                CharStats charMetric = new ();

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
