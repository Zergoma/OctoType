using OctoType.Application;
using OctoType.Application.DTOs;
using OctoType.Application.Interfaces;

namespace OctoType.Services;

public class MauiUserKeyboardLayoutPreferenceService : IUserKeyboardLayoutPreferenceService
{
    private readonly IKeyBoardLayoutDtoFactory _keyboardFactory;

    public MauiUserKeyboardLayoutPreferenceService(IKeyBoardLayoutDtoFactory keyboardFactory)
    {
        _keyboardFactory = keyboardFactory;
    }

    public Result<int> GetKeyboardType()
    {
        bool exists = Preferences.ContainsKey("selected_keyboard");
        if (!exists)
            return Result<int>.Fail("No user preference for selected_keyboard");

        return Result<int>
            .Ok(Preferences.Default.Get("selected_keyboard", (int)KeyboardLayoutEnumDto.AzertyFr));
    }

    public void SetKeyboardType(KeyBoardLayoutDto keyBoardLayoutDto)
    {
        var itemResu = GetKeyboardType();
        if(!itemResu.Success)
        {
            Preferences.Default.Set(
             "selected_keyboard",
             (int)keyBoardLayoutDto.KeyBoardCode);
            return;
        }

        if (itemResu.GetValue == (int)keyBoardLayoutDto.KeyBoardCode)
            return;

        Preferences.Default.Set(
             "selected_keyboard",
             (int)keyBoardLayoutDto.KeyBoardCode);
    }
}
