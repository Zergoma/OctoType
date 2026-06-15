using OctoType.Application.DTOs;

namespace OctoType.Application.Models;


//public class TypingExercices
//{
//    public List<TypingExerciceSetting> Exercices { get; set; } = [];
//}


public class TypingExerciceSetting
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string? Language { get; set; }

    public TypingExerciseSettingStatic? StaticSettings { get; set; }

    public TypingExerciceSettingDynamic? DynamicSettings { get; set; }
}


public class AllowLetter
{
    public string Letters { get; set; } = string.Empty;

    public KeyBoardLayoutDto KeyboardLayout { get; set; }
}

/// <summary>
/// Used for generated text saved 
/// </summary>
public class TypingExerciseSettingStatic
{
    public List<AllowLetter> AllowLettersConfig { get; set; } = [];
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// Used for dynamic generation
/// </summary>
public class TypingExerciceSettingDynamic
{
    public List<AllowLetter> AllowLettersConfig { get; set; } = [];
}

