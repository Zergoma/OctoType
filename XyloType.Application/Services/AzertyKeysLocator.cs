using XyloType.Application.Interfaces;
using XyloType.Domain.Enums;
using XyloType.Domain.Models;

namespace XyloType.Application.Services;

public class AzertyKeysLocator : IKeyboardKeysLocator
{
    private readonly Dictionary<char, KeyInfo> _azerty = [];
    private readonly KeyboardLayout _keyboardLayout;

    public IReadOnlyDictionary<char, KeyInfo> KeyLocator => _azerty;

    public KeyboardLayout GetKeyboardType => _keyboardLayout;

    public AzertyKeysLocator()
    {
        _azerty = AzertKeyLocatorsBuilder.BuildMap();
        _keyboardLayout = KeyboardLayout.AzertyFr;
    }
}
