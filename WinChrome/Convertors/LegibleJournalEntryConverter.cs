namespace WinChrome.Convertors
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows.Data;

    /// <summary>The legible journal entry converter.</summary>
    public class LegibleJournalEntryConverter : IValueConverter
    {
        /// <summary>The convert.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PropertyInfo info = value != null ? value.GetType().GetProperty("Name") : null;
            if (info == null)
            {
                return "Current Page";
            }

            var changer = new Regex(
@"\(     #Start bracket
[^\)]*  #Up to closing bracket
\)          #closing bracket", 
                             RegexOptions.IgnorePatternWhitespace);
            return changer.Replace((string)info.GetValue(value, null), string.Empty);
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
