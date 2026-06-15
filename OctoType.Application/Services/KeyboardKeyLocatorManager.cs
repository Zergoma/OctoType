using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;
using OctoType.Domain.Enums;
using OctoType.Application.Mappers;

namespace OctoType.Application.Services;

public class KeyboardKeyLocatorManager : IKeyboardKeyLocatorManager
{
    private readonly Dictionary<KeyboardLayout, IKeyboardKeysLocator> _keyLocators = [];
    public KeyboardKeyLocatorManager(IEnumerable<IKeyboardKeysLocator> keyboardKeyLocators)
    {
        foreach (var keylocator in keyboardKeyLocators)
        {
            _keyLocators[keylocator.GetKeyboardType] = keylocator;
        }
    }

    public Result<IKeyboardKeysLocator> GetKeyBoardKeyLocator(KeyBoardLayoutDto keyBoardLayout)
    {
        Result<KeyboardLayout> enumDomainResu = keyBoardLayout.KeyBoardCode.ToDomainEnum();
        if(!enumDomainResu.Success)
        {
            return Result<IKeyboardKeysLocator>.Fail(enumDomainResu.Error);
        }

        KeyboardLayout keyboardLayoutCode = enumDomainResu.GetValue;

        if (!_keyLocators.ContainsKey(keyboardLayoutCode))
            return Result<IKeyboardKeysLocator>
                .Fail($"No keysLocator defined for {keyBoardLayout}");

        return Result<IKeyboardKeysLocator>
            .Ok(_keyLocators[keyboardLayoutCode]);
    }
}