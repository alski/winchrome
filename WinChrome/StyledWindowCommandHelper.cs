namespace WinChrome
{
    using System.Windows;
    using System.Windows.Input;

    using Microsoft.Windows.Shell;

    /// <summary>Helper commands for the <see cref="StyledWindow"/>.</summary>
    public class StyledWindowCommandHelper
    {
        /// <summary>Gets or sets the window.</summary>
        public Window Window { get; set; }

        /// <summary>The on system close window command.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void OnSystemCloseWindowCommand(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this.Window);
        }

        /// <summary>The on system minimize window command.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void OnSystemMinimizeWindowCommand(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this.Window);
        }

        /// <summary>The on system maximize window command.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void OnSystemMaximizeWindowCommand(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this.Window);
        }

        /// <summary>The on system restore window command.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void OnSystemRestoreWindowCommand(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this.Window);
        }
    }
}