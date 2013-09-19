namespace WinChrome.Convertors
{
    using System;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>Converter that shows bytes as KB/MB/GB etc.</summary>
    [ValueConversion(typeof(long), typeof(string))]
    public class BytesConverter : MarkupExtension, IValueConverter
    {
        /// <summary>The convert.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            long foundSize;
            if (!long.TryParse(value.ToString(), out foundSize))
            {
                return "Unknown";
            }

            foreach (var s in new[] { " bytes", "KB", "MB", "GB", "TB" })
            {
                if (foundSize < 1024)
                {
                    return foundSize + s;
                }

                foundSize = foundSize / 1024;
            }

            return foundSize + "PB"; // really?
        }

        /// <summary>The convert back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        /// <exception cref="NotImplementedException">Oh look I'm not implemented</exception>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>The provide value.</summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
