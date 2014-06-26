using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ProjectSpark.MySql_Klassen
{
    class Categorie
    {
        int cat_id;
        string cat_naam;
        int cat_prioriteit;

        public Categorie(int id, string cat_naam, int cat_prioriteit)
        {
            this.cat_id = id;
            this.cat_naam = cat_naam;
            this.cat_prioriteit = cat_prioriteit;
        }

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
          
        public int Cat_prioriteit
        {
            get { return cat_prioriteit; }
            set { cat_prioriteit = value; }
        }
        
        public override string ToString()
        {
            return cat_naam.ToString();
        }
    }
}
