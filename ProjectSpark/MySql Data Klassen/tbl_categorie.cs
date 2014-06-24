using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpark.MySql_Klassen
{
    class tbl_categorie
    {
        int cat_id;
        string cat_naam;


        public int Cat_id
        {
            get { return cat_id; }
            set { cat_id = value; }
        }
        
        public string Cat_naam
        {
            get { return cat_naam; }
            set { cat_naam = value; }
        }

        public tbl_categorie(int id, string cat_naam)
        {
            this.cat_id = id;
            this.cat_naam = cat_naam;
        }

        public override string ToString()
        {
            return cat_naam.ToString();
        }
    }
}
