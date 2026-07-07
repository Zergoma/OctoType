using OctoType.Application;
using OctoType.Domain.Typing.Analysis;

namespace OctoType.Factories
{
    public interface IStatisticViewFactory
    {
        Task<Result<ContentPage>> Create(Dictionary<char, CharStats> stat);
    }
}