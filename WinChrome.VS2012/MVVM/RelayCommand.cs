using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WinChrome.VS2012Demo.MVVM
{
    public class RelayCommand<T> : ICommand
    {
        public bool IsExectuable { get; set; }
        public Action<T> ExecuteAction { get; set; }

        public bool CanExecute(object parameter)
        {
            return IsExectuable;    
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ExecuteAction((T)parameter);
        }
    }
}
