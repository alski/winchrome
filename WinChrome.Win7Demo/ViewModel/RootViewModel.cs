using demoMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinChrome.ViewModel
{
    public class RootViewModel
    {
        public RootViewModel()
        {
            SearchCommand = new Command
            {
                CanExecuteFunc = _ => true,
                ExecuteAction = _ => { }
            };
        }

        public Command SearchCommand { get; private set; }
    }
}
