namespace WinChrome
{
    using System.Windows;
    using System.Windows.Input;

    using Microsoft.Windows.Shell;
using System.Windows.Controls;

    /// <summary>Extension methods for <see cref="StyledWindow"/>s. Done this way to avoid inheritance issues.</summary>
    public static class StyledWindowExtension
    {
        /// <summary>Initializes styled window.</summary>
        /// <param name="window">The window.</param>
        public static void InitializeStyledWindow(this Window window)
        {
            var helper = new StyledWindowCommandHelper { Window = window };

            window.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, helper.OnSystemCloseWindowCommand));
            window.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, helper.OnSystemMaximizeWindowCommand));
            window.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, helper.OnSystemMinimizeWindowCommand));
            window.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, helper.OnSystemRestoreWindowCommand));
        }
    }
}