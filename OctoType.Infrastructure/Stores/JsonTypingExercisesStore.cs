using System.Text.Json;

using OctoType.Application;
using OctoType.Application.Interfaces;
using OctoType.Application.Models.Typing.Exercices;

namespace OctoType.Infrastructure.Stores;

public class JsonTypingExercisesStore : IExerciseSettingsStore
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    public async Task<Result<TypingExercices>> LoadAsync(string path)
    {
        if (!File.Exists(path))
            return Result<TypingExercices>
                .Fail($"File doesn't exist: {path}");

        string json =
            await File.ReadAllTextAsync(path);

        TypingExercices? data = JsonSerializer.Deserialize<TypingExercices>(json, Options);

        return data != null
            ? Result<TypingExercices>.Ok(data)
            : Result<TypingExercices>.Fail($"Deserialization failed for {path}");

    }

    public async Task<Result<bool>> SaveAsync(TypingExercices settings, string path)
    {
        string json = JsonSerializer.Serialize(settings, Options);
        if (string.IsNullOrWhiteSpace(json))
        {
            return Result<bool>
                .Fail("The serialized json is empty");
        }

        await File.WriteAllTextAsync(path, json);
        return Result<bool>
            .Ok(true);
    }
}
