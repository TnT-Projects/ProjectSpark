using ProjectSpark.MySql_Controller_Klassen;
using ProjectSpark.MySql_Klassen;
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
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : UserControl
    {
        Product selectedProduct;
        public Products()
        {
            InitializeComponent();
            //Vul listbox met producten
            lbx_products.ItemsSource = ProductDB.getProducts();
            //Vul combobox met categorien
            cbb_categories.ItemsSource = CategorieDB.getCategories();
            //Disable controls untill they're filled
            tbx_productNaam.IsEnabled = false;
            tbx_productPrijs.IsEnabled = false;
            cbb_categories.IsEnabled = false;

        }

        private void lbx_products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbx_productNaam.IsEnabled = true;
            tbx_productPrijs.IsEnabled = true;
            cbb_categories.IsEnabled = true;

            selectedProduct = (Product)lbx_products.SelectedItem;

            tbx_productNaam.Text = selectedProduct.Prd_naam;

            tbx_productPrijs.Text = selectedProduct.Prd_prijs.ToString();

            foreach (Categorie item in cbb_categories.Items)
            {
                if (selectedProduct.Prd_cat_id.Equals(item.Cat_id))
                {
                    cbb_categories.SelectedItem = item;
                }
            }
        }

        private void btn_MainMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switcher.Switch(new Main());
        }
    }
}
