using MySql.Data.MySqlClient;
using ProjectSpark.MySql_Klassen;
using System;
using System.Collections.Generic;

namespace ProjectSpark.MySql_Controller_Klassen
{
    class productenDB
    {
        public static List<producten> getProducts()
        {
            return returnProducts("SELECT * FROM tbl_producten");
        }

        public static List<producten> getProductsByCategories(int prd_cat_id)
        {
            return returnProducts("SELECT * FROM tbl_producten WHERE prd_cat_id=" + prd_cat_id);
        }

        private static List<producten> returnProducts(string stm)
        {
            List<producten> lijst = new List<producten>();
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd;
            MySqlDataReader rdr;
            try
            {
                conn = ConnectionDB.getConnection();
                conn.Open();
                cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = (int)rdr["prd_id"];
                    int cat_id = (int)rdr["prd_cat_id"];
                    string naam = (string)rdr["prd_naam"];
                    float prijs = (float)rdr["prd_prijs"];
                    bool enable = (bool)rdr["prd_enable"];
                    lijst.Add(new producten(id, cat_id, naam, prijs, enable));
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
