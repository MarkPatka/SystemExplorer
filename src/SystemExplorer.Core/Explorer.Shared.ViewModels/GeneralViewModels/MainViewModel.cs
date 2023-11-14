﻿using Explorer.Shared.ViewModels.BaseModels.Abstract;
using Explorer.Shared.ViewModels.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Explorer.Shared.ViewModels.BaseModels;

public class MainViewModel : BaseViewModel
{
    #region Private Variables
    private string filePath = string.Empty;
    private string name = string.Empty;
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
    public string Name
    {
        get => name;
        set
        {
            Set(ref name, value);
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
        Name = "This computer";

        foreach (var dir in Directory.GetLogicalDrives()) 
            Directories.Add(new DirectoryViewModel(new DirectoryInfo(dir)));
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
            Name = directoryViewModel.Name;

            Directories.Clear();

            var dirInfo = new DirectoryInfo(FilePath);
            try
            {
                foreach (var dir in dirInfo.GetDirectories())
                {
                    Directories.Add(new DirectoryViewModel(dir));
                }
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException("You don`t have rights to access this folder.");
                }
            }

            foreach (var file in dirInfo.GetFiles())
                Directories.Add(new FileViewModel(file));

        }
    }
    #endregion
}
