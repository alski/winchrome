namespace WinChrome.Convertors
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Data;

    /// <summary>The is current journal entry converter.</summary>
    public class IsCurrentJournalEntryConverter : IValueConverter
    {
        /// <summary>The convert.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PropertyInfo info = value == null ? null : value.GetType().GetProperty("Name");
            return (info == null) ? "Bold" : "Normal";
        }

        /// <summary>The convert back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
