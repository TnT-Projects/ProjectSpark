using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProjectSpark
{
    class TestTabelDB
    {
        public static List<TestTabel> getTestData(){
            List<TestTabel> lijst = new List<TestTabel>();
            MySqlConnection conn = ConnectionDB.getConnection();
            MySqlCommand cmd;
            MySqlDataReader rdr;
            string stm = "Select * FROM TestTabel";
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd = new MySqlCommand(stm, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int id = (int)rdr["id"];
                    string Naam = (string)rdr["Naam"];
                    lijst.Add(new TestTabel(id, Naam));
                }
            }
            catch (MySqlException ex){
                Console.WriteLine("Fout bij het verbinden met de database: " + ex.Message);
            }
            finally{
                conn.Close();
            }
            return lijst;
        }
    }
}
