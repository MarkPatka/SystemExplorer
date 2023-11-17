using SystemExplorer.Shared.BaseModels.Abstract;

namespace SystemExplorer.Shared.Entities;

public abstract class FileEntityViewModel : BaseViewModel
{
    public string FullName { get; } = null!;


    protected FileEntityViewModel(string fullName)
    {
        FullName = fullName;
    }
}
