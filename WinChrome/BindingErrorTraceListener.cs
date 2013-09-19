namespace WinChrome
{
    using System.Diagnostics;
    using System.Text;
    using System.Windows;

    /// <summary>The binding error trace listener.</summary>
    public class BindingErrorTraceListener : DefaultTraceListener
    {
        private static BindingErrorTraceListener _listener;

        private readonly StringBuilder _message = new StringBuilder();

        private BindingErrorTraceListener()
        {
        }

        /// <summary>The set trace.</summary>
        public static void SetTrace()
        {
            SetTrace(SourceLevels.Error, TraceOptions.None);
        }

        /// <summary>The set trace.</summary>
        /// <param name="level">The level.</param>
        /// <param name="options">The options.</param>
        public static void SetTrace(SourceLevels level, TraceOptions options)
        {
            if (_listener == null)
            {
                _listener = new BindingErrorTraceListener();
                PresentationTraceSources.DataBindingSource.Listeners.Add(_listener);
            }

            _listener.TraceOutputOptions = options;
            PresentationTraceSources.DataBindingSource.Switch.Level = level;
        }

        /// <summary>The close trace.</summary>
        public static void CloseTrace()
        {
            if (_listener == null)
            {
                return;
            }

            _listener.Flush();
            _listener.Close();
            PresentationTraceSources.DataBindingSource.Listeners.Remove(_listener);
            _listener = null;
        }

        /// <summary>Writes a message to the listener.</summary>
        /// <param name="message">The message.</param>
        public override void Write(string message)
        {
            this._message.Append(message);
        }

        /// <summary>The write line.</summary>
        /// <param name="message">The message.</param>
        public override void WriteLine(string message)
        {
            this._message.Append(message);

            var final = this._message.ToString();
            this._message.Length = 0;

            MessageBox.Show(
                final,
                "Binding Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}