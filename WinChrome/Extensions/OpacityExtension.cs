namespace WinChrome.Extensions
{
    using System;
    using System.Windows.Markup;
    using System.Windows.Media;

    /// <summary>The opacity extension.</summary>
    [MarkupExtensionReturnType(typeof(Color))]
    public class OpacityExtension : MarkupExtension
    {
        /// <summary>Initialises a new instance of the <see cref="OpacityExtension"/> class.</summary>
        /// <param name="opacity">The opacity.</param>
        /// <param name="color">The <see cref="Color"/>.</param>
        public OpacityExtension(double opacity, Color color)
        {
            this.Color = color;
            this.Opacity = opacity;
        }

        /// <summary>Gets or sets the <see cref="Color"/>.</summary>
        // ReSharper disable MemberCanBePrivate.Global
        public Color Color { get; set; }
        // ReSharper restore MemberCanBePrivate.Global
        ////

        /// <summary>Gets or sets the opacity.</summary>
        /// <remarks> Range is from from 0.0 to 1.0. Defaults to 0 or Transparent</remarks>
        // ReSharper disable MemberCanBePrivate.Global
        public double Opacity { get; set; }
        // ReSharper restore MemberCanBePrivate.Global
        ////

        /// <summary>Provides the converted <see cref="Color"/>.</summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Color.FromArgb((byte)(255 * this.Opacity), this.Color.R, this.Color.G, this.Color.B);
        }
    }
}
