namespace WinChrome.Convertors
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>The day of week and time convertor.</summary>
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public class DayOfWeekAndTimeConvertor : IValueConverter
    {
        /// <summary>The convert.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string format = @"hh\:mm\:ss";
            if (string.IsNullOrEmpty(parameter as string) == false)
            {
                format = (string)parameter;
            }

            var t = (TimeSpan)value;
            return Enum.GetName(typeof(DayOfWeek), (DayOfWeek)t.Days) + " " + t.ToString(format);
        }

        /// <summary>The convert back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
