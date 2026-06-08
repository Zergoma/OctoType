using OctoType.Domain.Enums;

namespace OctoType.Application.Interfaces
{
    public interface IKeyboardKeyLocatorManager
    {
        IKeyboardKeyLocator? GetKeyBoardKeyLocator(KeyboardLayout keyBoardLayout);
    }
}