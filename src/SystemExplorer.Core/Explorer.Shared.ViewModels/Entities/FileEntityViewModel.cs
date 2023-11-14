using Explorer.Shared.ViewModels.BaseModels.Abstract;

namespace Explorer.Shared.ViewModels.Entities;

public abstract class FileEntityViewModel : BaseViewModel
{
    public string FullName { get; } = null!;


    protected FileEntityViewModel(string fullName)
    {
        FullName = fullName;
    }
}
