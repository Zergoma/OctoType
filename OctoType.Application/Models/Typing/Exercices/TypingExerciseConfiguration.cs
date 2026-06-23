using OctoType.Application.DTOs;

namespace OctoType.Application.Models.Typing.Exercices;

public class TypingExerciseConfiguration
{
    public KeyBoardLayoutDto KeyboardLayout { get; set; }
    public TypingTextData TextData{ get; set; }
}

public class TypingTextData
{
    public string AllowedLetters { get; set; } = string.Empty;
    public TypingTextDataStatic? StaticTextData { get; set; }
    public TypingTextDataDynamic? DynamicTextData { get; set; }
}

public class TypingTextDataStatic
{
    public string GeneratedText { get; set; } = string.Empty;
}

public class TypingTextDataDynamic
{
    public GeneratedTypeSource GeneratedTypeSource { get; set; }
    public int LengthMin { get; set; }
    public int LengthMax { get; set; }

    public List<string> LanguagesSelected { get; set; } = [];
}