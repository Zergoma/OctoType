using OctoType.Application.Interfaces;
using OctoType.Domain.Enums;
using OctoType.Domain.Models;

namespace OctoType.Application.Services;

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
