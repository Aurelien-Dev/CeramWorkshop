using Domain.Models.WorkshopDomaine;
using System.Globalization;

namespace Client.Utils.Extension
{
    public static class StringMonetaryExtension
    {
        public static string ToStringMonetary(this double str, CultureInfo culture)
        {
            return str.ToString("C2", culture);
        }
    }
}
