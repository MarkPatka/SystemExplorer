using System.Windows.Input;

namespace Explorer.Shared.ViewModels.Entities;

public class DelegateCommand : ICommand
{
    private readonly Action<object> _open;
    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action<Object> open)
    {
        _open = open;
    }

    public bool CanExecute(object? parameter) 
        => true;

    public void Execute(object? parameter)
    {
        _open?.Invoke(parameter);
    }
}
