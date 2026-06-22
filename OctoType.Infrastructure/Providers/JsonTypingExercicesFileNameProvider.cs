using OctoType.Application.Interfaces;

namespace OctoType.Infrastructure.Providers;

public class JsonTypingExercicesFileNameProvider : ITypingExercicesFileNameProvider
{
    public string GetFileName()
        => "Exercices.json";

}