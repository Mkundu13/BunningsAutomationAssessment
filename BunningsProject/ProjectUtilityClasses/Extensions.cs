using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunningsProject.ProjectUtilityClasses
{
    public static class Extensions
    {
        public static string ToDateTimeInStringFormat(this string originalString, string format = "dd/MM/yyyy")
        {
            DateTime dt;

            try
            {
                dt = Convert.ToDateTime(originalString);
            }
            catch
            {
                dt = originalString.ToDateTimeInFormat(format);
            }

            return string.Format("{0: " + format + "}", dt).Trim();
        }

        public static DateTime ToDateTimeInFormat(this string stringdate, string Format)
        {
            return DateTime.ParseExact(stringdate, Format, null);
        }
    }
}
