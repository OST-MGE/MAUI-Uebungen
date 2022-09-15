using System.Windows.Input;

namespace U12.Infrastructure;

public sealed class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    #region ICommand

    public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

    public void Execute(object parameter) => _execute();

    public event EventHandler CanExecuteChanged;

    #endregion

    #region Public Methods

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}