namespace WinChrome.Win7Demo
{
    using System.Windows;
    using System.Windows.Controls;

    using WinChrome.View;
    using WinChrome.ViewModel;

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

            ////this.GoHomeCommand = ReactiveCommand.Create(_ => true, this.GoHome);

            ////// Increase the max concurrency to ensure that we don't de-focus the search control and prevent searching
            ////var command = new ReactiveAsyncCommand(maximumConcurrent: 5);
            ////command.RegisterAsyncAction(
            ////    x =>
            ////    {
            ////        Debug.WriteLine("{0:hh:mm:ss.fffff}:Search for {1} on {2}", DateTime.Now, x, Thread.CurrentThread.ManagedThreadId);
            ////    });

            ////this.SearchCommand = command;
            ////command.ObserveOnDispatcher().Subscribe(
            ////    _ =>
            ////    {
            ////        Debug.WriteLine("{0:hh:mm:ss.fffff}:UI Update for Search for {1} on {2}", DateTime.Now, this.SearchText, Thread.CurrentThread.ManagedThreadId);
            ////    });
        }

        // ReSharper disable UnusedAutoPropertyAccessor.Global
        // ReSharper disable MemberCanBePrivate.Global

        /////// <summary>Gets the go home command.</summary>
        ////public ReactiveCommand GoHomeCommand { get; private set; }

        // ReSharper restore UnusedAutoPropertyAccessor.Global
        // ReSharper restore MemberCanBePrivate.Global
        ////

        /// <summary>The go home.</summary>
        /// <param name="parameter">The parameter.</param>
        private void GoHome(object parameter)
        {
            this.Navigate(
                new HomePage
                {
                    DataContext = this.DataContext
                });
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(
                new HomePage { DataContext = this.GetDataContext() });
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