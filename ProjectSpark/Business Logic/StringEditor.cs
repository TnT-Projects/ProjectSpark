using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpark.Business_Logic
{
    class StringEditor
    {
        public static string Truncate(string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + ".";
        }

        public static string ConvertToCurrency(double value)
        {
            return string.Format("{0:c}", value);
        }
    }
}
