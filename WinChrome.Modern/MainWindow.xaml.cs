namespace WinChrome.ModernDemo
{
    using demoMVVM;
    using System.Windows;
    using System.Windows.Controls;
    using WinChrome.ModernDemo.View;
    using WinChrome.ModernDemo.ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>Initialises a new instance of the <see cref="MainWindow"/> class.</summary>
        public MainWindow()
        {
            ////if (Debugger.IsAttached)
            ////    BindingErrorTraceListener.SetTrace();

            this.InitializeComponent();

            var root = new RootViewModel();
            this.DataContext = root;
            //this.navigationTree.DataContext = root;

            GoPage1Command = new Command
            {
                CanExecuteFunc = _ => true,
                ExecuteAction = _ => this.Navigate(new Page1 { DataContext = this.DataContext })
            };

            GoPage2Command = new Command
            {
                CanExecuteFunc = _ => true,
                ExecuteAction = _ => this.Navigate(new Page2 { DataContext = this.DataContext })
            };

            GoPage3Command = new Command
            {
                CanExecuteFunc = _ => true,
                ExecuteAction = _ => this.Navigate(new Page3 { DataContext = this.DataContext })
            };

        }

        public Command GoPage1Command { get; set; }
        public Command GoPage2Command { get; set; }
        public Command GoPage3Command { get; set; }


        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            GoPage1Command.Execute(null);
        }

        private RootViewModel GetDataContext()
        {
            if (this.NavigationService != null
                && this.NavigationService.Content != null
                && ((Page)this.NavigationService.Content).DataContext is RootViewModel)
            {
                return (RootViewModel)((Page)this.NavigationService.Content).DataContext;
            }

            return (RootViewModel)this.DataContext;
        }
    }
}