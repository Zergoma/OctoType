using XyloType.Application;
using XyloType.Application.DTOs;
using XyloType.Application.Interfaces;

namespace XyloType.Services;

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

    public void SetKeyboardType(int keyBoardLayoutId)
    {
        var itemResu = GetKeyboardType();
        if(!itemResu.Success)
        {
            Preferences.Default.Set(
             "selected_keyboard",
             keyBoardLayoutId);
            return;
        }

        if (itemResu.GetValue == keyBoardLayoutId)
            return;

        Preferences.Default.Set(
             "selected_keyboard",
             keyBoardLayoutId);
    }
}
