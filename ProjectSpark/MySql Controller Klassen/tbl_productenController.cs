using MySql.Data.MySqlClient;
using ProjectSpark.MySql_Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSpark.MySql_Controller_Klassen
{
    class tbl_productenController
    {

        public static List<tbl_producten> getProducts()
        {
            List<tbl_producten> lijst = new List<tbl_producten>();
            //MySqlConnection conn = Switcher.Switcher.pageSwitcher.conn;
            MySqlCommand cmd;
            MySqlDataReader rdr;
            string stm = "Select * FROM tbl_producten";
            try
            {
                Switcher.Switcher.pageSwitcher.conn.Open();
                //cmd = conn.CreateCommand();
                cmd = new MySqlCommand(stm, Switcher.Switcher.pageSwitcher.conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = (int)rdr["prd_id"];
                    int cat_id = (int)rdr["prd_cat_id"];
                    string Naam = (string)rdr["prd_naam"];
                    float prijs = (float)rdr["prd_prijs"];
                    lijst.Add(new tbl_producten(id, cat_id, Naam, prijs));
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


        public static List<tbl_producten> getProductsByCategories(int prd_cat_id)
        {
            List<tbl_producten> lijst = new List<tbl_producten>();
            MySqlConnection conn = ConnectionDB.getConnection();
            MySqlCommand cmd;
            MySqlDataReader rdr;
            string stm = "SELECT * FROM tbl_producten WHERE prd_cat_id=" + prd_cat_id;
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = (int)rdr["prd_id"];
                    int cat_id = (int)rdr["prd_cat_id"];
                    string Naam = (string)rdr["prd_naam"];
                    float prijs = (float)rdr["prd_prijs"];
                    lijst.Add(new tbl_producten(id, cat_id, Naam, prijs));
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Fout bij het verbinden met de database: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return lijst;
        }

    }
}
