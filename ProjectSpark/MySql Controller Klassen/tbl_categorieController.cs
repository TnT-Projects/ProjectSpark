using MySql.Data.MySqlClient;
using ProjectSpark.MySql_Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpark.MySql_Controller_Klassen
{
    class tbl_categorieController
    {
        public static List<tbl_categorie> getCategories()
        {
            List<tbl_categorie> lijst = new List<tbl_categorie>();
            MySqlCommand cmd;
            MySqlDataReader rdr;
            string stm = "Select * FROM tbl_categorie";
            try
            {
                Switcher.Switcher.pageSwitcher.conn.Open();
                cmd = Switcher.Switcher.pageSwitcher.conn.CreateCommand();
                cmd = new MySqlCommand(stm, Switcher.Switcher.pageSwitcher.conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = (int)rdr["cat_id"];
                    string Naam = (string)rdr["cat_naam"];
                    lijst.Add(new tbl_categorie(id, Naam));
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Fout bij het verbinden met de database: " + ex.Message);
            }
            finally
            {
                Switcher.Switcher.pageSwitcher.conn.Close();
            }
            return lijst;
        }
    }
}
