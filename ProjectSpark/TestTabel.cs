using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpark
{
    public class TestTabel
    {
        public TestTabel(int id, string Naam)
        {
            this.Naam = Naam;
            this.id = id;
        }

        public int id { get; set; }
        public string Naam { get; set; }
        public override string ToString()
        {
            return Naam;
        }
    }
}
