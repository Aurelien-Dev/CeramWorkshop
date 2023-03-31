using Domain.Models.WorkshopDomaine;
using System.Globalization;

namespace Client.Utils.Extension
{
    /// <summary>
    /// Represents the number of decimal places to use for monetary values.
    /// </summary>
    public enum DecimalPrecision
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
    }

    /// <summary>
    /// Provides extension methods for formatting monetary values as strings.
    /// </summary>
    public static class StringMonetaryExtension
    {
        /// <summary>
        /// Converts the specified double value to a monetary string using the specified culture and precision.
        /// </summary>
        /// <param name="str">The double value to convert.</param>
        /// <param name="culture">The culture to use for formatting.</param>
        /// <param name="precision">The number of decimal places to use for the monetary value.</param>
        /// <returns>The monetary value as a formatted string.</returns>
        public static string ToStringMonetary(this double str, CultureInfo culture, DecimalPrecision precision = DecimalPrecision.Two)
        {
            return str.ToString($"C{(int)precision}", culture);
        }

        /// <summary>
        /// Converts the specified nullable double value to a monetary string using the specified culture and precision.
        /// If the value is null, returns a string with two dashes instead of the monetary value.
        /// </summary>
        /// <param name="str">The nullable double value to convert.</param>
        /// <param name="culture">The culture to use for formatting.</param>
        /// <param name="precision">The number of decimal places to use for the monetary value.</param>
        /// <returns>The monetary value as a formatted string, or a string with two dashes if the value is null.</returns>
        public static string ToStringMonetary(this double? str, CultureInfo culture, DecimalPrecision precision = DecimalPrecision.Two)
        {
            if (str == null) return 0f.ToString($"C{(int)precision}", culture).Replace("0", "--");

            return ToStringMonetary(str.Value, culture, precision);
        }
    }

}
