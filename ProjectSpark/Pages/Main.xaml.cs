﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        List<string> buttonNames;

        public Main()
        {
            InitializeComponent();
            ((MainWindow)Switcher.Switcher.pageSwitcher).Title = "Hoofd Menu";
            buttonNames = new List<string>() { "Verkoop","Bestellingen","Promoties","Klanten","Produkten","Rapport","Inventaris","Instellingen","Log uit" };
            int counter = 0;
            for (int i = 0; i < grd_MainMenu.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < grd_MainMenu.ColumnDefinitions.Count; j++)
                {
                    Button btn = new Button();
                    btn.Content = buttonNames[counter];
                    //btn.Width = 300;
                    //btn.Height = 300;
                    btn.FontSize = 20;
                    btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    btn.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    btn.Margin = new Thickness(25);
                    btn.SetValue(Grid.RowProperty, i);
                    btn.SetValue(Grid.ColumnProperty, j);
                    btn.Click += btn_Click;
                    btn.Tag = counter;
                    Border myBorder1;
                    myBorder1 = new Border();
                    myBorder1.BorderBrush = Brushes.SlateBlue;
                    myBorder1.BorderThickness = new Thickness(5, 10, 15, 20);
                    myBorder1.Background = Brushes.AliceBlue;
                    myBorder1.Padding = new Thickness(5);
                    myBorder1.CornerRadius = new CornerRadius(15);

                    
                    //Image img = new Image();
                    //img.Source = new BitmapImage(new Uri(@"foo.png"));
                    //StackPanel stackPnl = new StackPanel();
                    //stackPnl.Orientation = Orientation.Horizontal;
                    //stackPnl.Margin = new Thickness(10);
                    //stackPnl.Children.Add(img);


                    grd_MainMenu.Children.Add(btn);
                    counter++;
                }
            }
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(((Button)sender).Tag))
                {
                    case 0:
                        //MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        Switcher.Switcher.Switch(new Sales());
                        break;
                    case 1:
                        //MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        Switcher.Switcher.Switch(new Window1());
                        break;
                    case 2:
                        MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        break;
                    case 3:
                        MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        break;
                    case 4:
                        MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        break;
                    case 5:
                        MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        break;
                    case 6:
                        MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        break;
                    case 7:
                        MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        break;
                    case 8:
                        MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        break;
                    case 9:
                        MessageBox.Show(Convert.ToInt32(((Button)sender).Tag).ToString());
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("lol " + ex);
            }
        }
    }
}