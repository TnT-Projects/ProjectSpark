using MySql.Data.MySqlClient;
using ProjectSpark.MySql_Klassen;
using System;
using System.Collections.Generic;

namespace ProjectSpark.MySql_Controller_Klassen
{
    class ProductDB
    {
        public static List<Product> getProducts()
        {
            return returnProducts("SELECT * FROM tbl_producten");
        }

        public static List<Product> getProductsByCategories(int prd_cat_id)
        {
            return returnProducts("SELECT * FROM tbl_producten WHERE prd_cat_id=" + prd_cat_id);
        }

        private static List<Product> returnProducts(string stm)
        {
            List<Product> lijst = new List<Product>();
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
                    lijst.Add(new Product(id, cat_id, naam, prijs, enable));
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
        
        public static bool addProduct(int cat_id, string naam, float prijs, bool enable)
        {
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn = ConnectionDB.getConnection();
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO tbl_producten (prd_id, prd_cat_id, prd_naam, prd_prijs, prd_enable)" +
                    " VALUES (NULL, @cat_id, @naam, @prijs, @enable)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@cat_id", cat_id);
                cmd.Parameters.AddWithValue("@naam", naam);
                cmd.Parameters.AddWithValue("@prijs", prijs);
                cmd.Parameters.AddWithValue("@enable", enable);

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
        public static bool editProduct(int product_id, int cat_id, string naam, float prijs, bool enable)
        {
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                conn = ConnectionDB.getConnection();
                conn.Open();
                cmd.Connection = conn;
                
                cmd.CommandText = "UPDATE tbl_producten" +
                    " SET prd_cat_id = @cat_id, prd_naam = @naam, prd_prijs = @prijs, prd_enable = @enable WHERE prd_id = @product_id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@cat_id", cat_id);
                cmd.Parameters.AddWithValue("@naam", naam);
                cmd.Parameters.AddWithValue("@prijs", prijs);
                cmd.Parameters.AddWithValue("@enable", enable);
                cmd.Parameters.AddWithValue("@product_id", product_id);

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
