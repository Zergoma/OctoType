using OctoType.Application.DTOs;

namespace OctoType.Application.Interfaces
{
    public interface IKeyboardKeyLocatorManager
    {
        Result<IKeyboardKeysLocator> GetKeyBoardKeyLocator(KeyBoardLayoutDto keyBoardLayout);
    }
}