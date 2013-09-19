using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinChrome.UI;
using WinChrome.VS2012Demo.Pages;

namespace WinChrome.VS2012Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            //PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.All;
            //BindingErrorTraceListener.SetTrace();
            InitializeComponent();

            Navigate(new DemoPage(this));            
            //focusMe.Focus();
        }

    }
}
