using XyloType.Application;
using XyloType.Application.DTOs;

namespace XyloType.Application.Interfaces
{
    public interface IKeyboardLayoutDetector
    {
        Result<KeyboardLayoutEnumDto> DetectKeyboardLayout();
    }
}