using MySql.Data.MySqlClient;
using ProjectSpark.MySql_Klassen;
using System;
using System.Collections.Generic;

namespace ProjectSpark.MySql_Controller_Klassen
{
    class categorieDB
    {
        public static List<categorie> getCategories()
        {
            List<categorie> lijst = new List<categorie>();
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd;
            MySqlDataReader rdr;
            string stm = "Select * FROM tbl_categorie";
            try
            {
                conn = ConnectionDB.getConnection();
                conn.Open();
                cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = (int)rdr["cat_id"];
                    string naam = (string)rdr["cat_naam"];
                    lijst.Add(new categorie(id, naam));
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
