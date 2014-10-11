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

        public static string SplitToNewLine(string value, int maxLength)
        {
            string[] names = value.Split(' ', '-');
            string newName = "";
            int count = 0;
            foreach (string name in names)
            {
                if (name.Length + newName.Length <= maxLength)
                {
                    newName += name + " ";
                    count++;
                }
            }



            return string.Join(Environment.NewLine, value.Split()
    .Select((word, index) => new { word, index })
    .GroupBy(x => x.index / count)
    .Select(grp => string.Join(" ", grp.Select(x => x.word)))); 
        }
    }
}
