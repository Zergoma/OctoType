using OctoType.Application;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Factories
{
    public interface IExerciceGeneratorViewFactory
    {
        Task<Result<ContentPage>> CreateExerciceGeneratorView();
        Task<Result<ContentPage>> CreateExerciceUpdaterView(Guid exerciceToUpdate);
    }
}