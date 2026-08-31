namespace XyloType.Application.Interfaces;

public interface IUserKeyboardLayoutPreferenceService
{
    Result<int> GetKeyboardType();
    void SetKeyboardType(int keyBoardLayoutDtoId);
}