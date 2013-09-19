namespace WinChrome.Extensions
{
    using System.Windows.Media;

    /// <summary>The <see cref="Color"/> extension.</summary>
    public static class ColorExtension
    {
        /// <summary>Merges two <see cref="Color"/>s.</summary>
        /// <param name="color">The first <see cref="Color"/>.</param>
        /// <param name="other">The other <see cref="Color"/>.</param>
        /// <param name="amount">The amount of the first <see cref="Color"/> to use, range 0.0 to 1.0. 
        /// The <paramref name="other"/> colour will have an opposing amount.</param>
        /// <returns>The <see cref="Color"/>.</returns>
        public static Color Merge(this Color color, Color other, double amount)
        {
            var inverseAmount = 1 - amount;
            return Color.FromArgb(
                color.A,
                (byte)((color.R * inverseAmount) + (other.R * amount)),
                (byte)((color.G * inverseAmount) + (other.G * amount)),
                (byte)((color.B * inverseAmount) + (other.B * amount)));
        }

        /// <summary>Converts the <see cref="Color"/> to a grey scale by levelling the RGB values.</summary>
        /// <param name="color">The <see cref="Color"/>.</param>
        /// <returns>A greyed out <see cref="Color"/>.</returns>
        public static Color GrayScale(this Color color)
        {
            var gray = (byte)((color.R + color.G + color.B) / 3);
            return Color.FromArgb(color.A, gray, gray, gray);
        }

        /// <summary>Merges two <see cref="Color"/>s by mixing equal amounts of each.</summary>
        /// <param name="color">The <see cref="Color"/>.</param>
        /// <param name="other">The other.</param>
        /// <returns>The resulting mixed <see cref="Color"/>.</returns>
        public static Color Merge(this Color color, Color other)
        {
            return color.Merge(other, 0.5);
        }

        /// <summary>Fades a <see cref="Color"/> by merging it with an amount of its <see cref="GrayScale"/>.</summary>
        /// <param name="color">The <see cref="Color"/>.</param>
        /// <param name="amount">The amount from 0.0 to 1.0.</param>
        /// <returns>The resulting <see cref="Color"/>.</returns>
        public static Color Fade(this Color color, double amount)
        {
            return color.Merge(color.GrayScale(), amount);
        }

        /// <summary>Fades a <see cref="Color"/> by merging it with an equal amount of its <see cref="GrayScale"/>.</summary>
        /// <param name="color">The <see cref="Color"/>.</param>
        /// <returns>The resulting <see cref="Color"/>.</returns>
        public static Color Fade(this Color color)
        {
            return color.Fade(0.5);
        }

        /// <summary>Lightens a <see cref="Color"/> by mixing it with <see cref="Colors.White"/>.</summary>
        /// <param name="color">The <see cref="Color"/>.</param>
        /// <returns>The resulting <see cref="Color"/>.</returns>
        public static Color Lighten(this Color color)
        {
            return color.Lighten(0.25d);
        }

        /// <summary>Lightens a <see cref="Color"/> by mixing it with an amount of <see cref="Colors.White"/>.</summary>
        /// <param name="color">The <see cref="Color"/>.</param>
        /// <param name="amount">The amount of <see cref="Colors.White"/> to mix.</param>
        /// <returns>The resulting <see cref="Color"/>.</returns>
        public static Color Lighten(this Color color, double amount)
        {
            return color.Merge(Colors.White, amount);
        }

        /// <summary>Creates a simple vertical light gradient brush.</summary>
        /// <param name="baseColour">The base colour.</param>
        /// <returns>The <see cref="LinearGradientBrush"/>.</returns>
        public static LinearGradientBrush VerticalSimpleLightGradientBrush(this Color baseColour)
        {
            return new LinearGradientBrush(baseColour, baseColour.Fade(), 90.0d);
        }

        /// <summary>Creates a vertical gradient brush.</summary>
        /// <param name="baseColour">The base colour.</param>
        /// <returns>The <see cref="LinearGradientBrush"/>.</returns>
        public static LinearGradientBrush VerticalGradientBrush(this Color baseColour)
        {
            var colors = new GradientStopCollection(new[]
                {
                    new GradientStop(baseColour.Lighten(0.5d), 0.0d),
                    new GradientStop(baseColour.Lighten(0.25d), 0.4d),
                    new GradientStop(baseColour, 0.6d),
                    new GradientStop(baseColour.Fade(0.25d), 1.0d)
                });

            return new LinearGradientBrush(colors, 90.0d);
        }
    }
}
