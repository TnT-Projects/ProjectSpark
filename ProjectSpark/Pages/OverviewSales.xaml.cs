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

            
            Button button = new Button();
            button.Content = "Tafel 1";
            button.Margin= new Thickness(15,15,0,0);
            button.Width=75;
            button.Height = 75;
            button.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            button.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            button.Background = (Brush)new BrushConverter().ConvertFrom("#134370");

            Button button2 = new Button();
            button2.Content = "Tafel 2";
            button2.Margin = new Thickness(30, 250, 0, 0);
            button2.Width = 75;
            button2.Height = 75;
            button2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            button2.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            Button button3 = new Button();
            button3.Content = "Tafel 3";
            button3.Margin = new Thickness(135, 200, 0, 0);
            button3.Width = 75;
            button3.Height = 75;
            button3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            button3.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            Button button4 = new Button();
            button4.Content = "Tafel 4";
            button4.Margin = new Thickness(861, 228, 0, 0);
            button4.Width = 75;
            button4.Height = 75;
            button4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            button4.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            

            grd_GroundPlan.Children.Add(button);
            grd_GroundPlan.Children.Add(button2);
            grd_GroundPlan.Children.Add(button3);
            grd_GroundPlan.Children.Add(button4);
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            //Switcher.Switcher.Switch(new Sales());
        }
    }
}
