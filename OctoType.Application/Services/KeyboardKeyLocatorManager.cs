using OctoType.Application.Interfaces;
using OctoType.Domain.Enums;

namespace OctoType.Application.Services;

public class KeyboardKeyLocatorManager : IKeyboardKeyLocatorManager
{
    private readonly Dictionary<KeyboardLayout, IKeyboardKeyLocator> _keyLocators = [];
    public KeyboardKeyLocatorManager(IEnumerable<IKeyboardKeyLocator> keyboardKeyLocators)
    {
        foreach (var keylocator in keyboardKeyLocators)
        {
            _keyLocators[keylocator.GetKeyboardType] = keylocator;
        }
    }

    public IKeyboardKeyLocator? GetKeyBoardKeyLocator(KeyboardLayout keyBoardLayout)
    {
        if (!_keyLocators.ContainsKey(keyBoardLayout))
            return null;

        return _keyLocators[keyBoardLayout];
    }
}