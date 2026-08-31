using XyloType.Domain.Entities;

namespace XyloType.Domain.Models;

public readonly record struct KeyInfo(KeyboardRow Row, Finger Finger, bool ExtrenalAccent = false);
