namespace WinChrome.Convertors
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>The highlight text convertor.</summary>
    [ValueConversion(typeof(string), typeof(Inline))]
    public class HighlightTextConvertor : IMultiValueConverter
    {
        /// <summary>Initialises a new instance of the <see cref="HighlightTextConvertor"/> class.</summary>
        public HighlightTextConvertor()
        {
            this.HighlightBrush = new SolidColorBrush(Color.FromArgb(127, Colors.Yellow.R, Colors.Yellow.G, Colors.Yellow.B));
        }

        private Brush HighlightBrush { get; set; }

        /// <summary>The convert.</summary>
        /// <param name="values">The values.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var valueString = values[0] as string;
            if (string.IsNullOrEmpty(valueString))
            {
                return null;
            }

            var regex = values[1] as Regex;
            if (regex == null)
            {
                return valueString;
            }

            var parts = regex.Split(valueString);
            var result = new Span();
            result.Inlines.AddRange(from part in parts
                                    select this.BuildInline(part, regex.IsMatch(part)));
            result.Style = parameter as System.Windows.Style;
            return result;
        }

        /// <summary>The convert back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetTypes">The target types.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Returns nothing.</returns>
        /// <exception cref="NotImplementedException">Always going to happen</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Inline BuildInline(string part, bool isHighlighted)
        {
            var result = new Run(part);
            if (isHighlighted)
            {
                result.Background = this.HighlightBrush;
            }

            return result;
        }
    }
}
