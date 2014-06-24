using MySql.Data.MySqlClient;
using ProjectSpark.Pages;
using ProjectSpark.Switcher;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectSpark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ServerConnection serverConnection;
        public MySqlConnection conn;
        public MainWindow()
        {
            InitializeComponent();

            //Verbinding met database testen
            serverConnection = new ServerConnection();
            conn = ConnectionDB.getConnection();
            try
            {
                //conn.Open();
                string sql = "SELECT CURDATE();";
                MySqlCommand cmd = cmd = new MySqlCommand(sql, conn); //conn.CreateCommand();
                conn.Open();
                DateTime newProdID = (DateTime)cmd.ExecuteScalar();
                //MessageBox.Show(newProdID.ToString());//cmd = new MySqlCommand(sql, conn);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Foutcode: 8005250 Kon geen verbinding maken met de database, applicatie wordt nu beëindigd!\n" + ex.Message);
                this.Close();
            }
            finally
            {
                conn.Close();
            }
            
            Switcher.Switcher.pageSwitcher = this;
            Switcher.Switcher.Switch(new Main());
        }



        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
    }
}
