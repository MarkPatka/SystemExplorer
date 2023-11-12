using Explorer.Shared.ViewModels.BaseModels.Abstract;
using Explorer.Shared.ViewModels.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Explorer.Shared.ViewModels.BaseModels;

public class MainViewModel : BaseViewModel
{
    #region Private Variables
    private string filePath = string.Empty;
    private ObservableCollection<FileEntityViewModel> directories = new();
    private FileEntityViewModel selectedFile;
    #endregion

    #region Public Properties
    public string FilePath
    {
        get => filePath;
        set
        {
            Set(ref filePath, value);
            OnPropertyChanged();
        }
    }
    public ObservableCollection<FileEntityViewModel> Directories
    {
        get => directories;
        set
        {
            Set(ref directories, value);
            OnPropertyChanged();
        }
    }

    public FileEntityViewModel SelectedFile 
    { 
        get => selectedFile;
        set 
        {
            Set(ref selectedFile, value);
            OnPropertyChanged();
        }
    }

    #endregion

    #region Ctor
    public MainViewModel()
    {
        FilePath = Environment.SystemDirectory;

        foreach (var dir in Directory.GetLogicalDrives()) 
            Directories.Add(new DirectoryViewModel(dir));
    }
    #endregion

    #region Commands
    public ICommand OpenCommand => new DelegateCommand(Open);
    #endregion

    #region Private Methods
    private void Open(object? parameter = null)
    {
        if (parameter is DirectoryViewModel directoryViewModel)
        {
            FilePath = directoryViewModel.FullName;

            Directories.Clear();

            var dirInfo = new DirectoryInfo(FilePath);

            foreach (var dir in dirInfo.GetDirectories())
                Directories.Add(new DirectoryViewModel(dir));

            foreach (var file in dirInfo.GetFiles())
                Directories.Add(new FileViewModel(file));

        }
    }
    #endregion
}
