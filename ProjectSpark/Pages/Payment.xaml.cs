using ProjectSpark.MySql_Klassen;
using ProjectSpark.Switcher;
using System;
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
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : UserControl, ISwitchable
    {
        List<tbl_producten> products;
        List<tbl_producten> subProducts;
        object previousState;

        public Payment(List<tbl_producten> products)
        {
            InitializeComponent();
            this.products = products;
            this.subProducts = new List<tbl_producten>();
            UpdateProductListbox();
        }

        //Update listbox view
        private void UpdateProductListbox()
        {
            Dictionary<tbl_producten, int> counts = products.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            lbx_Products.ItemsSource = counts;
        }

        //Update listbox view
        private void UpdateSubProductListbox()
        {
            Dictionary<tbl_producten, int> counts = subProducts.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            lbx_SubProducts.ItemsSource = counts;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switcher.Switch(new Sales(), previousState);
        }

        public void UtilizeState(object state)
        {
            previousState = state;
        }

        //Remove selecteditem objects from list
        private void removeProduct(List<tbl_producten> list, ListBox listBox, tbl_producten product)
        {
            list.RemoveAt(GetLastIndexOfProduct(list, (tbl_producten)(((KeyValuePair<tbl_producten, int>)listBox.SelectedItem).Key)));
        }

        //Find last index of a product in a list
        private int GetLastIndexOfProduct(List<tbl_producten> list, tbl_producten product)
        {
            for (int i = list.Count - 1; i > -1; i--)
            {
                if (list[i].Prd_id.Equals(product.Prd_id))
                {
                    return i;
                }
            }
            return 0;
        }

        private void lbx_Products_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                subProducts.Add((tbl_producten)(((KeyValuePair<tbl_producten, int>)lbx_Products.SelectedItem).Key));
                removeProduct(products, lbx_Products, (tbl_producten)(((KeyValuePair<tbl_producten, int>)lbx_Products.SelectedItem).Key));
                UpdateProductListbox();
                UpdateSubProductListbox();
            }
            catch
            {
                //Make sure app doesnt crash when pressed inside listbox
            }
        }

        private void lbx_SubProducts_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                products.Add((tbl_producten)(((KeyValuePair<tbl_producten, int>)lbx_SubProducts.SelectedItem).Key));
                removeProduct(subProducts, lbx_SubProducts, (tbl_producten)(((KeyValuePair<tbl_producten, int>)lbx_SubProducts.SelectedItem).Key));
                UpdateProductListbox();
                UpdateSubProductListbox();
            }
            catch
            {
                //Make sure app doesnt crash when pressed inside listbox
            }
        }

        private void btn_Pay_Click(object sender, RoutedEventArgs e)
        {
            double totaal = 0;
            if (subProducts.Count > 0)
            {
                foreach (tbl_producten item in subProducts)
                {
                    totaal += item.Prd_prijs;
                }
                MessageBox.Show("AFGEREKENT! TOTAAL PRIJS: " + totaal);
                subProducts.Clear();
                UpdateSubProductListbox();
            }
            else
            {
                foreach (tbl_producten item in products)
                {
                    totaal += item.Prd_prijs;
                }
                MessageBox.Show("AFGEREKENT! TOTAAL PRIJS: " + totaal);
                products.Clear();
                UpdateProductListbox();
                Switcher.Switcher.Switch(new Sales());
            }
            
            
        }
    }
}
