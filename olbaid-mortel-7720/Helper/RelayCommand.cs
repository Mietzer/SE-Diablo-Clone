using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace olbaid_mortel_7720.Helper
{
  public class RelayCommand : ICommand
  {

    #region Properties
    private Action<object> execute;
    private Func<bool> canExecute;
    #endregion Properties
    public RelayCommand(Action<object> execute, Func<bool> canExecute)
    {
      this.execute = execute;
      this.canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    => canExecute();

    public void Execute(object? parameter)
    => execute(parameter);

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }
  }
}
