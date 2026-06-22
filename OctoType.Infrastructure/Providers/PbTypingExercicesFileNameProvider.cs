using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure.Providers;

public class PbTypingExercicesFileNameProvider : ITypingExercicesFileNameProvider
{
    public string GetFileName()
        => "Exercices.pb";

}
