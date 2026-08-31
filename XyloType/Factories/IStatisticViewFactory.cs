using XyloType.Application;
using XyloType.Domain.Typing.Analysis;

namespace XyloType.Factories
{
    public interface IStatisticViewFactory
    {
        Task<Result<ContentPage>> Create(Dictionary<char, CharStats> stat);
    }
}