using System.Text.Json;

using OctoType.Application.Interfaces;
using OctoType.Application.Models;

namespace OctoType.Infrastructure.Stores;

public class JsonExerciseSettingsStore : IExerciseSettingsStore
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    public async Task<TypingExerciceSetting?> LoadAsync(string settings, string path)
    {
        string json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<TypingExerciceSetting>(json);
    }

    public async Task SaveAsync(TypingExerciceSetting settings, string path)
    {
        string json = JsonSerializer.Serialize(settings, Options);
        await File.WriteAllTextAsync(path, json);
    }
}
