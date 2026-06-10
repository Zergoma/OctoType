using OctoType.Domain.Entities;
using OctoType.Domain.Enums;

namespace OctoType.Domain.Models;

public struct WordSearchCriteria
{
    public string[]? LanguagesCodes { get; set; }

    public KeyboardLayout? Layout { get; set; }

    public KeyboardRow? RowMask { get; set; }

    public Finger? FingerMask { get; set; }

    public int? MinLength { get; set; }

    public int? MaxLength { get; set; }
}
