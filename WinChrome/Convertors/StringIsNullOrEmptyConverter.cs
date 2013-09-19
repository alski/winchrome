namespace WinChrome.Convertors
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>The string is null or empty converter.</summary>
    /// <returns>Returns True if the string is null or empty</returns>
    public class StringIsNullOrEmptyConverter : IValueConverter
    {
        /// <summary>The convert.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            return string.IsNullOrEmpty(text);
        }

        /// <summary>The don't convert back throw an exception method.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        /// <exception cref="NotImplementedException">Always get one of these</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
