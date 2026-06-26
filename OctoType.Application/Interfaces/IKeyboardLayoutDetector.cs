using OctoType.Application.DTOs;

namespace OctoType.Application.Interfaces
{
    public interface IKeyboardLayoutDetector
    {
        Result<KeyboardLayoutEnumDto> DetectKeyboardLayout();
    }
}