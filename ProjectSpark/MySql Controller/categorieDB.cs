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
            string stm = "SELECT * FROM tbl_categorie";
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

        public static bool addCategorie(string naam, int prioriteit)
        {
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn = ConnectionDB.getConnection();
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO tbl_categorie (cat_id, cat_naam, cat_prioriteit)" +
                    " VALUES (NULL, @naam, @prioriteit)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@naam", naam);
                cmd.Parameters.AddWithValue("@prioriteit", prioriteit);

                cmd.ExecuteNonQuery();
                return true;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Fout bij het invoegen van gegevens: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool editdCategorie(string naam, int prioriteit, int cat_id)
        {
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn = ConnectionDB.getConnection();
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "UPDATE tbl_categorie" +
                    " SET cat_naam = @naam, cat_prioriteit = @prioriteit WHERE cat_id = @cat_id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@naam", naam);
                cmd.Parameters.AddWithValue("@prioriteit", prioriteit);
                cmd.Parameters.AddWithValue("@cat_id", cat_id);

                cmd.ExecuteNonQuery();
                return true;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Fout bij het invoegen van gegevens: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
