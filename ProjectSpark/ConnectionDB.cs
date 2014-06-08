using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ProjectSpark
{
    class ConnectionDB
    {

        public static MySqlConnection getConnection(){
            string myConnectionString = "server=84.193.222.30;uid=TnT;pwd=Dmdnet22;database=Spark;";

             MySql.Data.MySqlClient.MySqlConnection conn = new MySqlConnection();
             conn.ConnectionString = myConnectionString;

             return conn;
            //catch (MySqlException ex)

       }
    }
}
