namespace Explorer.Shared.ViewModels.Entities;

public sealed class DirectoryViewModel : FileEntityViewModel
{
    public string FullName { get; set; }

    public DirectoryViewModel(string directoryName) 
        : base(directoryName)
    {
        FullName = directoryName;
    }

    public DirectoryViewModel(DirectoryInfo directoryName)
        : base(directoryName.FullName)
    {
        FullName = directoryName.FullName;
    }
}
