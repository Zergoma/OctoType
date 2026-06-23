using OctoType.Application;
using OctoType.Application.Interfaces;

namespace OctoType.Factories
{
    public interface ITypingViewFactory
    {
        Task<Result<ContentPage>> CreateTypingViewAsync(IStringsProvider stringProvider, INavigationService navigationService);
    }
}