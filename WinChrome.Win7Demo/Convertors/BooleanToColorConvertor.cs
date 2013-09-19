namespace WinChrome.Convertors
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>The boolean to <see cref="Color"/>convertor.</summary>
    [ValueConversion(typeof(bool), typeof(Color))]
    public class BooleanToColorConvertor : IValueConverter
    {
        /// <summary>Initialises a new instance of the <see cref="BooleanToColorConvertor"/> class.</summary>
        public BooleanToColorConvertor()
        {
            this.TrueColor = new SolidColorBrush(Colors.Green);
            this.FalseColor = new SolidColorBrush(Colors.Red);
        }

        /// <summary>Gets or sets the true <see cref="Color"/>.</summary>
        public Brush TrueColor { get; set; }

        /// <summary>Gets or sets the false <see cref="Color"/>.</summary>
        public Brush FalseColor { get; set; }

        /// <summary>The convert.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return this.TrueColor;
            }

            return this.FalseColor;
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
