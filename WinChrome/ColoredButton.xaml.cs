using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinChrome
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ColoredButton
    {
        /// <summary>The glow <see cref="Brush"/><see cref="DependencyProperty"/>.</summary>
        public static readonly DependencyProperty MouseOverBackgroundProperty =
             DependencyProperty.Register(
                 "MouseOverBackground",
                 typeof(Brush),
                 typeof(ColoredButton),
                 new PropertyMetadata(Brushes.White));

        public static Brush GetMouseOverBackground(UIElement window)
        {
            return (Brush)window.GetValue(MouseOverBackgroundProperty);
        }

        public static void SetMouseOverBackground(UIElement window, Brush glow)
        {
            window.SetValue(MouseOverBackgroundProperty, glow);
        }

        /// <summary>The glow <see cref="Brush"/><see cref="DependencyProperty"/>.</summary>
        public static readonly DependencyProperty MouseOverForegroundProperty =
             DependencyProperty.Register(
                 "MouseOverForeground",
                 typeof(Brush),
                 typeof(ColoredButton),
                 new PropertyMetadata(SystemColors.HotTrackBrush));

        public static Brush GetMouseOverForeground(UIElement window)
        {
            return (Brush)window.GetValue(MouseOverForegroundProperty);
        }

        public static void SetMouseOverForeground(UIElement window, Brush glow)
        {
            window.SetValue(MouseOverForegroundProperty, glow);
        }

        /// <summary>The glow <see cref="Brush"/><see cref="DependencyProperty"/>.</summary>
        public static readonly DependencyProperty PressedBackgroundProperty =
             DependencyProperty.Register(
                 "PressedBackground",
                 typeof(Brush),
                 typeof(ColoredButton),
                 new PropertyMetadata(SystemColors.HotTrackBrush));

        public static Brush GetPressedBackground(UIElement window)
        {
            return (Brush)window.GetValue(PressedBackgroundProperty);
        }

        public static void SetPressedBackground(UIElement window, Brush glow)
        {
            window.SetValue(PressedBackgroundProperty, glow);
        }

        /// <summary>The glow <see cref="Brush"/><see cref="DependencyProperty"/>.</summary>
        public static readonly DependencyProperty PressedForegroundProperty =
             DependencyProperty.Register(
                 "PressedForeground",
                 typeof(Brush),
                 typeof(ColoredButton),
                 new PropertyMetadata(Brushes.White));

        public static Brush GetPressedForeground(UIElement window)
        {
            return (Brush)window.GetValue(PressedForegroundProperty);
        }

        public static void SetPressedForeground(UIElement window, Brush glow)
        {
            window.SetValue(PressedForegroundProperty, glow);
        }

        public ColoredButton()
        {
            InitializeComponent();
        }
    }
}
