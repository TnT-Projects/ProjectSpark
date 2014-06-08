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
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : UserControl
    {
        Scanner scanner;
        public Sales()
        {
            InitializeComponent();
            ((MainWindow)Switcher.Switcher.pageSwitcher).Title = "Verkoop";
            scanner = new Scanner();
            scanner.scanEvent += scanner_scanEvent;

            for (int i = 0; i < 15; i++)
            {
                TabItem test = new TabItem();
                test.Header = i.ToString() + " LOL";
                tbc_Products.Items.Add(test);
            }

            foreach (TabItem tab in tbc_Products.Items)
            {
                tab.Height = 50;
                tab.MinWidth = 100;
            }

        }

        void scanner_scanEvent(string EAN)
        {
            //SCAN EVENT, LOOK IN DATABSE FOR PRODUCT
            this.Dispatcher.Invoke((Action)(() =>
            {
                if (EAN.Equals("5449000000996"))
                {
                    MessageBox.Show("Cola");
                }
                else if (EAN.Equals("54491014"))
                {
                    lbx_Products.Items.Add(("Cola Mini").PadRight(40) + string.Format("{0:c}", 1.90));
                }
                else
                {
                    lbx_Products.Items.Add((EAN).PadRight(40) + string.Format("{0:c}", 5.90));
                }
            }));
        }

        private void btn_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            //scanner.scanEvent -= scanner_scanEvent; ON LEAVE PAGE EVENT
            scanner.scanEvent -= scanner_scanEvent;
            Switcher.Switcher.Switch(new Main());
        }



        
    }
}