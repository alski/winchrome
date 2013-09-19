using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WinChrome.UI;
using WinChrome.VS2012Demo.MVVM;

namespace WinChrome.VS2012Demo.ViewModel
{
    using WinChrome.VS2012Demo.Pages;

    public class DemoPageViewModel
    {
        private SearchableNavigationWindow _window;

        public DemoPageViewModel(SearchableNavigationWindow window)
        {
            _window = window;
            SetColorCommand = new RelayCommand<Color> { IsExectuable = true, ExecuteAction = SetGlowColor };
            GoNextCommand = new RelayCommand<object> { IsExectuable = true, ExecuteAction = GoNext };
        }

        private void GoNext(object obj)
        {
            _window.NavigationService.Navigate(new DemoPage2 { DataContext = this });
        }

        public RelayCommand<Color> SetColorCommand { get; private set; }
        public RelayCommand<object> GoNextCommand { get; private set; }
        
        private void SetGlowColor(Color newColor)
        {
            _window.SetValue(VS2012.GlowColorProperty, newColor);
        }

    }
}
