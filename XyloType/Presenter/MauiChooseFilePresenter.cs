using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using XyloType.Application;
using XyloType.Application.Interfaces;

namespace XyloType.Presenter;

public class MauiChooseFilePresenter : IChoosePath
{
    public async Task<Result<string?>> SelectPathAsync()
    {
        try
        {
            FileResult? result =
                await FilePicker.Default.PickAsync();

            if (result != null)
            {
                return Result<string?>
                    .Ok(result.FullPath);
            }
        }
        catch (Exception ex)
        {
            return Result<string?>
                .Fail($"Error on file selection: {ex.Message}");

        }

        // cancel
        return Result<string?>
            .Ok(null);
    }
}
