namespace WinChrome.UI
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>The navigation page.</summary>
    public class NavigationPage
    {
        /// <summary>The alert property.</summary>
        public static readonly DependencyProperty AlertProperty = DependencyProperty.RegisterAttached(
            "Alert",
            typeof(UIElement), 
            typeof(NavigationPage));

        /// <summary>The vertical scroll property.</summary>
        public static readonly DependencyProperty VerticalScrollProperty = DependencyProperty.RegisterAttached(
            "VerticalScroll",
            typeof(ScrollBarVisibility),
            typeof(NavigationPage),
            new PropertyMetadata(ScrollBarVisibility.Auto));

        /// <summary>Sets the alert <see cref="UIElement"/>.</summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        [DebuggerStepThrough]
// ReSharper disable UnusedMember.Global
        public static void SetAlert(DependencyObject element, UIElement value)
// ReSharper restore UnusedMember.Global
        {
            element.SetValue(AlertProperty, value);
        }

        /// <summary>Gets the <see cref="UIElement"/> that will be displayed as an alert.</summary>
        /// <param name="element">The element.</param>
        /// <returns>The System.Windows.UIElement.</returns>
        [DebuggerStepThrough]
// ReSharper disable UnusedMethodReturnValue.Global
        public static UIElement GetAlert(DependencyObject element)
// ReSharper restore UnusedMethodReturnValue.Global
        {
            return (UIElement)element.GetValue(AlertProperty);
        }

        /// <summary>Sets the VerticalScroll.</summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        [DebuggerStepThrough]
// ReSharper disable UnusedMember.Global
        public static void SetVerticalScroll(DependencyObject element, ScrollBarVisibility value)
// ReSharper restore UnusedMember.Global
        {
            element.SetValue(VerticalScrollProperty, value);
        }

        /// <summary>Gets the VerticalScroll</summary>
        /// <param name="element">The element.</param>
        /// <returns>The System.Windows.UIElement.</returns>
        [DebuggerStepThrough]
// ReSharper disable UnusedMethodReturnValue.Global
        public static ScrollBarVisibility GetVerticalScroll(DependencyObject element)
// ReSharper restore UnusedMethodReturnValue.Global
        {
            return (ScrollBarVisibility)element.GetValue(VerticalScrollProperty);
        }
    }
}
