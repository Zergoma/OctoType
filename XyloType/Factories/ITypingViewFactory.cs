using XyloType.Application;
using XyloType.Application.Interfaces;

namespace XyloType.Factories
{
    public interface ITypingViewFactory
    {
        Task<Result<ContentPage>> CreateTypingViewAsync(IStringsProvider stringProvider, INavigationService navigationService);
    }
}