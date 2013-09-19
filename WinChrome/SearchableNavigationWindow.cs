using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WinChrome
{
    using System.Threading;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    /// <summary>The searchable navigation window.</summary>
    [TemplatePart(Name = "PART_SearchTextBox", Type = typeof(TextBox))]
    public class SearchableNavigationWindow : NavigationWindow
    {
        /// <summary>The alert property.</summary>
        public static readonly DependencyProperty NavigationProperty = DependencyProperty.Register(
            "Navigation",
            typeof(UIElement),
            typeof(SearchableNavigationWindow));

        /// <summary>The global content property.</summary>
        public static readonly DependencyProperty GlobalContentProperty = DependencyProperty.Register(
            "GlobalContent",
            typeof(UIElement),
            typeof(SearchableNavigationWindow));

        public static UIElement GetGlobalContent(UIElement window)
        {
            return (UIElement)window.GetValue(GlobalContentProperty);
        }

        public static void SetGlobalContent(UIElement window, UIElement glow)
        {
            window.SetValue(GlobalContentProperty, glow);
        }

        /// <summary>Attached property to determine if textboxes that perform searches should be hidden if the window does not have a search command.</summary>
        public static readonly DependencyProperty HideIfNoSearchProperty = DependencyProperty.RegisterAttached(
            "HideIfNoSearch",
            typeof(bool),
            typeof(TextBox), 
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetHideIfNoSearch(TextBox textbox)
        {
            return (bool)textbox.GetValue(HideIfNoSearchProperty);
        }

        public static void SetHideIfNoSearch(TextBox textbox, bool value)
        {
            textbox.SetValue(HideIfNoSearchProperty, value);
        }

        /// <summary>The search command property.</summary>
        public static readonly DependencyProperty SearchCommandProperty = DependencyProperty.Register(
            "SearchCommand",
            typeof(ICommand),
            typeof(SearchableNavigationWindow),
            new PropertyMetadata(
                null,
                SearchCommandChanged));

        /// <summary>The search command target property.</summary>
        public static readonly DependencyProperty SearchCommandTargetProperty = DependencyProperty.Register(
            "SearchCommandTarget",
            typeof(object),
            typeof(SearchableNavigationWindow));

        /// <summary>The search text property.</summary>
        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register(
            "SearchText",
            typeof(string),
            typeof(SearchableNavigationWindow),
            new PropertyMetadata(
                null,
                SearchTextChanged));

        public SearchableNavigationWindow()
        {
            this.InitializeStyledWindow();
            this.Loaded += this.SearchableNavigationWindowLoaded;
        }

        /// <summary>Gets or sets the navigation <see cref="UIElement"/>.</summary>
        public UIElement Navigation
        {
            get { return (UIElement)this.GetValue(NavigationProperty); }
            set { this.SetValue(NavigationProperty, value); }
        }

        /// <summary>Gets or sets the search command.</summary>
        public ICommand SearchCommand
        {
            get { return (ICommand)this.GetValue(SearchCommandProperty); }
            set { this.SetValue(SearchCommandProperty, value); }
        }


        /// <summary>Gets or sets the search text.</summary>
        public string SearchText
        {
            get { return (string)this.GetValue(SearchTextProperty); }
            set { this.SetValue(SearchTextProperty, value); }
        }

        /// <summary>Gets or sets the search command target.</summary>
        public IInputElement SearchCommandTarget
        {
            get { return (IInputElement)this.GetValue(SearchCommandTargetProperty); }
            set { this.SetValue(SearchCommandTargetProperty, value); }
        }


        private static void SearchCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var searcher = (SearchableNavigationWindow)d;
            searcher.ChangeCommand((ICommand)e.NewValue, (ICommand)e.OldValue);
        }

        private void ChangeCommand(ICommand newCommand, ICommand oldCommand)
        {
            if (oldCommand != null)
            {
                oldCommand.CanExecuteChanged -= this.CanExecuteChanged;
            }

            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += this.CanExecuteChanged;
            }
            else
            {
                SetSearchVisibilty();
            }
        }

        private void SetSearchVisibilty()
        {
            var searchBox = this.Template.FindName("PART_SearchBox", this) as TextBox;
            if (searchBox != null)
            {
                var hide = (bool)searchBox.GetValue(SearchableNavigationWindow.HideIfNoSearchProperty);
                if (hide)
                    searchBox.Visibility = SearchCommand == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {
            var searchBox = this.Template.FindName("PART_SearchBox", this) as TextBox;

            if (searchBox != null)
            {
                if (this.SearchCommand != null)
                {
                    bool enabled;
                    var routedCommand = this.SearchCommand as RoutedCommand;
                    enabled = routedCommand != null
                        ? routedCommand.CanExecute(this.SearchText, this.SearchCommandTarget)
                        : this.SearchCommand.CanExecute(this.SearchText);

                    searchBox.IsEnabled = enabled;
                }
                else
                {
                    searchBox.IsEnabled = false;
                }
            }
        }

        private static void SearchTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var searcher = d as SearchableNavigationWindow;

            if (searcher != null
                && searcher.SearchCommand != null
                && searcher.SearchCommand.CanExecute(searcher.SearchText))
            {
                searcher.SearchCommand.Execute(searcher.SearchText);
            }
        }

        private void SearchableNavigationWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.SetSearchVisibilty();
            this.CanExecuteChanged(null, null);
        }
    }
}
