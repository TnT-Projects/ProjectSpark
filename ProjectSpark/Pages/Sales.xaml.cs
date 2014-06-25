using ProjectSpark.MySql_Controller_Klassen;
using ProjectSpark.MySql_Klassen;
using ProjectSpark.Switcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public partial class Sales : UserControl, ISwitchable
    {
        Scanner scanner;
        List<producten> selectedProducts;
        Dictionary<producten, int> counts;
        public Sales()
        {
            InitializeComponent();
            ((MainWindow)Switcher.Switcher.pageSwitcher).Title = "Verkoop";
            scanner = new Scanner();
            scanner.scanEvent += scanner_scanEvent;
            selectedProducts = new List<producten>();
            counts = selectedProducts.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            //lbx_Products.Items.Clear();
            lbx_Products.ItemsSource = counts;

            //Aanmaken van tabs uit de database
            foreach (categorie item in categorieDB.getCategories())
            {
                TabItem test = new TabItem();
                test.Header = item.Cat_naam;
                test.Tag = item.Cat_id;
                tbc_Products.Items.Add(test);
            }

            //Opvullen van tabs met producten uit de database
            foreach (TabItem item in tbc_Products.Items)
            {
                WrapPanel wrpPanel = new WrapPanel();
                foreach (producten product in productenDB.getProductsByCategories((int)item.Tag))
                {
                    Button button = new Button();
                    {
                        button.Content = product.Prd_naam;
                        button.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                        button.Width = 125;
                        button.Height = 125;
                        button.Margin = new Thickness(5);
                        button.Tag = product;
                        button.Background = Brushes.GhostWhite;
                        button.Click += button_Click;
                        wrpPanel.Children.Add(button);
                    }
                }
                item.Content = wrpPanel;
            }

            foreach (TabItem tab in tbc_Products.Items)
            {
                tab.Height = 50;
                tab.MinWidth = 100;
            }

        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            producten product = (producten)((Button)sender).Tag;

            selectedProducts.Add(product);
            lbl_SelectedProduct.Content = product.Prd_naam;
            UpdateProductListbox();

            foreach (KeyValuePair<producten, int> item in lbx_Products.Items)
            {
                if (((producten)item.Key).Prd_naam.Equals(product.Prd_naam))
                {
                    lbx_Products.SelectedItem = item;
                }
            }
        }

        private void UpdateProductListbox()
        {
            //var count = selectedProducts.GroupBy(x => x).Select(x => new {Name = x.Key, Total = x.Count() }).ToList();
            counts = selectedProducts.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            //lbx_Products.Items.Clear();
            lbx_Products.ItemsSource = counts;// selectedProducts.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count()); ;
        }

        void scanner_scanEvent(string EAN)
        {
            //SCAN EVENT, LOOK IN DATABSE FOR PRODUCT
            this.Dispatcher.Invoke((Action)(() =>
            {
                //BARSCANNER LOGIC HERE
            }));
        }

        private void btn_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            //scanner.scanEvent -= scanner_scanEvent; ON LEAVE PAGE EVENT
            scanner.scanEvent -= scanner_scanEvent;
            Switcher.Switcher.Switch(new Main());
        }

        private void btn_removeProduct_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_Products.SelectedItem != null)
            {
                int selectedIndex = lbx_Products.SelectedIndex;

                lbl_SelectedProduct.Content = "- " + ((producten)(((KeyValuePair<producten, int>)lbx_Products.SelectedItem).Key)).Prd_naam;

                // Figure out a way to remove LAST added product of that name
                selectedProducts.RemoveAt(GetLastIndexOfProduct((producten)(((KeyValuePair<producten, int>)lbx_Products.SelectedItem).Key))); //((tbl_producten)(((KeyValuePair<tbl_producten, int>)lbx_Products.SelectedItem).Key));
                UpdateProductListbox();
                if (selectedIndex > (lbx_Products.Items.Count - 1))
                {
                    selectedIndex--;
                }
                lbx_Products.SelectedIndex = selectedIndex;
                
            }
            else
            {
                lbl_SelectedProduct.Content = "Geen product geselecteerd!";
                //MessageBox.Show("Er dient eerst een product geselecteerd te worden, alvorens te verwijderen.");
            }

            
        }

        private int GetLastIndexOfProduct(producten product)
        {
            for (int i = selectedProducts.Count - 1; i > -1; i--)
            {
                if (selectedProducts[i].Prd_naam.Equals(product.Prd_naam))
                {
                    return i;
                }

            }
            
            return 0;
        }

        private void btn_Pay_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switcher.Switch(new Payment(selectedProducts), this);
        }

        public void UtilizeState(object state)
        {
            //Opvullen State
            this.selectedProducts = ((Sales)state).selectedProducts;
            this.UpdateProductListbox();
        }
    }
}
