using OctoType.Application;

namespace OctoType.Factories
{
    public interface IExerciceGeneratorViewFactory
    {
        Task<Result<ContentPage>> CreateExerciceGeneratorView();
    }
}