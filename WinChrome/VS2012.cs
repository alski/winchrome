using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace WinChrome
{
    public class VS2012
    {
        private const double GLOW_BORDER_MARGIN = 5.0d;

        /// <summary>The glow <see cref="Color"/><see cref="DependencyProperty"/>.</summary>
        public static readonly DependencyProperty GlowColorProperty =
             DependencyProperty.RegisterAttached(
                 "GlowColor",
                 typeof(Color),
                 typeof(VS2012),
                 new FrameworkPropertyMetadata(Colors.DarkOrange, FrameworkPropertyMetadataOptions.Inherits));

        public static Color GetGlowColor(UIElement uiElement)
        {
            return (Color)uiElement.GetValue(GlowColorProperty);
        }

        public static void SetGlowColor(UIElement uiElement, Color glow)
        {
            uiElement.SetValue(GlowColorProperty, glow);
        }

        /// <summary>The glow <see cref="Color"/><see cref="DependencyProperty"/>.</summary>
        public static readonly DependencyProperty GlowBorderProperty =
             DependencyProperty.RegisterAttached(
                 "GlowBorder",
                 typeof(bool),
                 typeof(VS2012),
                 new PropertyMetadata(false, new PropertyChangedCallback(ChangeGlowBorder)));

        public static bool GetGlowBorder(Border border)
        {
            return (bool)border.GetValue(GlowBorderProperty);
        }

        public static void SetGlowBorder(Border border, bool isSet)
        {
            border.SetValue(GlowBorderProperty, border);
        }

        private static void ChangeGlowBorder(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddGlow(d as Border);
        }

        private static void AddGlow(Border border)
        {
            if (border == null)
                return;

            var glow = new DropShadowEffect { ShadowDepth = 0.0d, BlurRadius = GLOW_BORDER_MARGIN, Color = (Color)border.GetValue(VS2012.GlowColorProperty) };
            border.Effect = glow;
            border.SizeChanged += HandleGlowBorder;
        }

        private static void HandleGlowBorder(object sender, SizeChangedEventArgs e)
        {
            var border = sender as Border;
            var window = border.Parent as Window ?? border.TemplatedParent as Window;
            if (window == null)
                return;

            if (window.WindowState == WindowState.Maximized)
            {
                // Need to keep the margin or else we loose pixels from the windows caption buttons etc
                border.Margin = new Thickness(GLOW_BORDER_MARGIN);
                // border.BorderThickness = new Thickness(0);
                // force a redraw
                window.InvalidateVisual();
                return;
            }

            // Snapped windows are a little more awkward to detect
            // todo: Need to get current screen height not primary
            var workingArea = WpfScreen.GetScreenFrom(window).WorkingArea;
            if (window.Top == workingArea.Top
                && window.Height == workingArea.Height)
            {
                border.Margin = new Thickness(
                    window.Left == workingArea.Left ? 0 : GLOW_BORDER_MARGIN,
                    0,
                    window.Left + window.Width == workingArea.Right ? 0 : GLOW_BORDER_MARGIN,
                    0);
                border.BorderThickness = new Thickness(
                    window.Left == workingArea.Left ? 0 : 1,
                    0,
                    window.Left + window.Width == workingArea.Right ? 0 : 1,
                    0);
                return;
            }
            border.Margin = new Thickness(GLOW_BORDER_MARGIN);
            border.BorderThickness = new Thickness(1);
        }

        /// <summary>The ChromeContent AttachedProperty.</summary>
        public static readonly DependencyProperty ChromeContentProperty =
             DependencyProperty.RegisterAttached(
                 "ChromeContent",
                 typeof(UIElement),
                 typeof(VS2012));

        public static UIElement GetChromeContent(UIElement window)
        {
            return (UIElement)window.GetValue(ChromeContentProperty);
        }

        public static void SetChromeContent(UIElement window, UIElement content)
        {
            window.SetValue(ChromeContentProperty, content);
        }

    }
}
