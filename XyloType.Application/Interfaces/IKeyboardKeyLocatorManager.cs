using XyloType.Application.DTOs;

namespace XyloType.Application.Interfaces
{
    public interface IKeyboardKeyLocatorManager
    {
        Result<IKeyboardKeysLocator> GetKeyBoardKeyLocator(KeyBoardLayoutDto keyBoardLayout);
    }
}