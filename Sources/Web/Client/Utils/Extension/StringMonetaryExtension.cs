using Domain.Models.WorkshopDomaine;
using System.Globalization;

namespace Client.Utils.Extension
{
    public enum DecimalPrecision
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
    }
    public static class StringMonetaryExtension
    {
        public static string ToStringMonetary(this double str, CultureInfo culture, DecimalPrecision precision = DecimalPrecision.Two)
        {
            return str.ToString($"C{(int)precision}", culture);
        }
    }
}
