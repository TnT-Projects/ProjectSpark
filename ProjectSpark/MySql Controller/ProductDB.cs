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
        /*public static bool addProduct(int cad_id, string naam, float prijs, bool enable)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(null, connection);

            // Create and prepare an SQL statement.
            command.CommandText =
                "INSERT INTO Region (RegionID, RegionDescription) " +
                "VALUES (@id, @desc)";
            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int, 0);
            SqlParameter descParam =
                new SqlParameter("@desc", SqlDbType.Text, 100);
            idParam.Value = 20;
            descParam.Value = "First Region";
            command.Parameters.Add(idParam);
            command.Parameters.Add(descParam);

            // Call Prepare after setting the Commandtext and Parameters.
            command.Prepare();
            command.ExecuteNonQuery();

            // Change parameter values and call ExecuteNonQuery.
            command.Parameters[0].Value = 21;
            command.Parameters[1].Value = "Second Region";
            command.ExecuteNonQuery();
            return true;
        }*/
    }
}
