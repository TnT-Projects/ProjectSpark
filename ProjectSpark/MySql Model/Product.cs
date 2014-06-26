using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpark.MySql_Klassen
{
    public class producten
    {
        int prd_id;
        int prd_cat_id;
        string prd_naam;
        float prd_prijs;
        bool prd_enable;

        public producten(int prd_id, int prd_cat_id, string prd_naam, float prd_prijs, bool prd_enable)
        {
            this.prd_id = prd_id;
            this.prd_cat_id = prd_cat_id;
            this.prd_naam = prd_naam;
            this.prd_prijs = prd_prijs;
            this.prd_enable = prd_enable;
        }

        public int Prd_id
        {
            get { return prd_id; }
            set { prd_id = value; }
        }

        public int Prd_cat_id
        {
            get { return prd_cat_id; }
            set { prd_cat_id = value; }
        }

        public string Prd_naam
        {
            get { return prd_naam; }
            set { prd_naam = value; }
        }

        public float Prd_prijs
        {
            get { return prd_prijs; }
            set { prd_prijs = value; }
        }

        public bool Prd_enable
        {
            get { return prd_enable; }
            set { prd_enable = value; }
        }

        public override string ToString()
        {
            return prd_naam.Length <= 14 ? prd_naam.PadRight(19) + string.Format("{0:c}", this.prd_prijs) : prd_naam.Substring(0, 14) + "...".PadRight(5) + string.Format("{0:c}", this.prd_prijs);
            //return this.prd_naam.PadRight(20) + string.Format("{0:c}", this.prd_prijs);
        }

    }
}
