using System.Text.Json;

using OctoType.Application.Interfaces;
using OctoType.Application.Models;

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
    public async Task<TypingExercices?> LoadAsync(string path)
    {
        if (!File.Exists(path))
            return null;

        string json =
            await File.ReadAllTextAsync(path);

        return JsonSerializer.Deserialize<TypingExercices>(json, Options);
    }

    public async Task SaveAsync(TypingExercices settings, string path)
    {
        string json = JsonSerializer.Serialize(settings, Options);
        await File.WriteAllTextAsync(path, json);
    }
}
