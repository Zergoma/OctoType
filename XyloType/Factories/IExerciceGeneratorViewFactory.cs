using XyloType.Application;

namespace XyloType.Factories
{
    public interface IExerciceGeneratorViewFactory
    {
        Task<Result<ContentPage>> CreateExerciceGeneratorView();
        Task<Result<ContentPage>> CreateExerciceUpdaterView(Guid exerciceToUpdate);
    }
}