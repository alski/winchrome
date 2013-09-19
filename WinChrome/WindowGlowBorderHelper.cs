using Microsoft.Windows.Shell;
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
    public class WindowGlowBorderHelper
    {
        private WeakReference _window;
        private WeakReference _border;
        private const double GLOW_BORDER_MARGIN = 0.0d;

        public WindowGlowBorderHelper(Window window, Border border)
        {
            _window = new WeakReference(window);
            _border = new WeakReference(border);

        }

        private void HandleGlowBorder(object sender, SizeChangedEventArgs e)
        {
            if (Window == null
                || Border == null)
                return;
      
        }

        public Window Window
        {
            get
            {
                if (_window.IsAlive)
                    return (Window)_window.Target;
                else
                    return null;
            }
        }

        public Border Border
        {
            get
            {
                if (_border.IsAlive)
                    return (Border)_border.Target;
                else
                    return null;
            }
        }
    }
}
