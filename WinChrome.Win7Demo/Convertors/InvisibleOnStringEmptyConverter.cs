namespace WinChrome.Convertors
{
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>The invisibility on string empty converter.</summary>
    [ValueConversion(typeof(String), typeof(Visibility))]
    public class InvisibleOnStringEmptyConverter : MarkupExtension, IValueConverter
    {
        /// <summary>Initialises a new instance of the <see cref="InvisibleOnStringEmptyConverter"/> class.</summary>
        public InvisibleOnStringEmptyConverter()
        {
            this.NullOrEmptyVisibility = Visibility.Collapsed;
        }

        /// <summary>Gets or sets the null or empty visibility.</summary>
        public Visibility NullOrEmptyVisibility { get; set; }

        /// <summary>The convert.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return this.NullOrEmptyVisibility;
            }

            var stringValue = value.ToString();
            return string.IsNullOrEmpty(stringValue) ? this.NullOrEmptyVisibility : Visibility.Visible;
        }

        /// <summary>The convert back.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The <see cref="object"/>.</returns>
        /// <exception cref="NotImplementedException">Isn't two way</exception>
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
