using OctoType.Application;

namespace OctoType.Infrastructure.Providers.Windows;

using System.Runtime.InteropServices;

using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;

public class WindowsKeyboardLayoutDetector : IKeyboardLayoutDetector
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetKeyboardLayout(uint idThread);

    public Result<KeyboardLayoutEnumDto> DetectKeyboardLayout()
    {
        IntPtr hkl = GetKeyboardLayout(0);

        // Les 16 bits de poids faible contiennent le LANGID
        string layout = ((uint)hkl & 0xFFFF).ToString("X4");
        return MapToDo(layout);
    }

    private static Result<KeyboardLayoutEnumDto> MapToDo(string layout)
    {
        return layout switch
        {
            "040C" => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.AzertyFr),
            "0409" => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.QwertyUs),
            // "0809" => KeyboardLayoutEnumDto.QwertyUk,
            "0407" => Result<KeyboardLayoutEnumDto>.Ok(KeyboardLayoutEnumDto.QwertzDe),
            _ => Result<KeyboardLayoutEnumDto>.Fail($"Keyboard auto detection have no map for {layout}")
        };
    }
}
