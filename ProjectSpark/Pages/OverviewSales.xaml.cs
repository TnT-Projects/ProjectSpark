using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace ProjectSpark.Pages
{
    /// <summary>
    /// Interaction logic for OverviewSales.xaml
    /// </summary>
    public partial class OverviewSales : UserControl
    {
        

        public OverviewSales()
        {
            InitializeComponent();
            //Maak verbinding met server
            Switcher.Switcher.pageSwitcher.serverConnection.Connect();
            //Vraag tafels aan server.  R:T staat voor Request:Tables
            Switcher.Switcher.pageSwitcher.serverConnection.SendMessageToServer("RQ:T");
            //Listen to add tables event
            Switcher.Switcher.pageSwitcher.serverConnection.serverTableMessage += serverConnection_serverTableMessage;

        }

        void serverConnection_serverTableMessage(string id, string name, string left, string top, string right, string bottom)
        {
            //Generate Tables passed by the server
            this.Dispatcher.Invoke((Action)(() =>
            {
                Button button = new Button();
                button.Content = name;
                button.Margin = new Thickness(Convert.ToDouble(left), Convert.ToDouble(top), Convert.ToDouble(right), Convert.ToDouble(bottom));
                button.Width = 75;
                button.Height = 75;
                button.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                button.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                button.Background = (Brush)new BrushConverter().ConvertFrom("#134370");
                grd_GroundPlan.Children.Add(button);
            }));
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Switcher.Switcher.Switch(new Sales());
        }
    }
}
