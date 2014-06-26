using MySql.Data.MySqlClient;
using ProjectSpark.MySql_Klassen;
using System;
using System.Collections.Generic;

namespace ProjectSpark.MySql_Controller_Klassen
{
    class CategorieDB
    {
        public static List<Categorie> getCategories()
        {
            List<Categorie> lijst = new List<Categorie>();
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
                    int prioriteit = (int)rdr["cat_prioriteit"];
                    lijst.Add(new Categorie(id, naam, prioriteit));
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
