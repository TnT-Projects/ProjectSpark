using ProjectSpark.Business_Logic;
using ProjectSpark.MySql_Klassen;
using ProjectSpark.Switcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        List<Product> products;
        List<Product> subProducts;
        Thread insertThread;
        object previousState;

        public Payment(List<Product> products)
        {
            InitializeComponent();
            this.products = products;
            this.subProducts = new List<Product>();
            UpdateProductListbox();
        }

        //Update listbox view
        private void UpdateProductListbox()
        {
            Dictionary<Product, int> counts = products.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            lbx_Products.ItemsSource = counts;
        }

        //Update listbox view
        private void UpdateSubProductListbox()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Dictionary<Product, int> counts = subProducts.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
                lbx_SubProducts.ItemsSource = counts;
            }));
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
        private void removeProduct(List<Product> list, ListBox listBox, Product product)
        {
            list.RemoveAt(GetLastIndexOfProduct(list, (Product)(((KeyValuePair<Product, int>)listBox.SelectedItem).Key)));
        }

        //Find last index of a product in a list
        private int GetLastIndexOfProduct(List<Product> list, Product product)
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
                subProducts.Add((Product)(((KeyValuePair<Product, int>)lbx_Products.SelectedItem).Key));
                removeProduct(products, lbx_Products, (Product)(((KeyValuePair<Product, int>)lbx_Products.SelectedItem).Key));
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
                products.Add((Product)(((KeyValuePair<Product, int>)lbx_SubProducts.SelectedItem).Key));
                removeProduct(subProducts, lbx_SubProducts, (Product)(((KeyValuePair<Product, int>)lbx_SubProducts.SelectedItem).Key));
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
            if (subProducts.Count > 0)
            {
                insertThread = new Thread(new ThreadStart(PrintSubPayment));
                insertThread.SetApartmentState(ApartmentState.STA);
                insertThread.Start();
            }
            else
            {
                insertThread = new Thread(new ThreadStart(FinnishPayment));
                insertThread.SetApartmentState(ApartmentState.STA);
                insertThread.Start();

                Switcher.Switcher.Switch(new Sales());
                //UpdateProductListbox();
               
            }
        }

        private void PrintSubPayment()
        {
            Printing.Print(CreateFlowDocument(subProducts, true));
            subProducts.Clear();
            UpdateSubProductListbox();
        }

        private void FinnishPayment()
        {
            //MessageBox.Show(products.Count+"");
            Printing.Print(CreateFlowDocument(products, false));
            //products.Clear();
            // PUT INTO DB!! :D
        }

        private FlowDocument CreateFlowDocument(List<Product> list, bool subRekening)
        {
            // Create a FlowDocument
            FlowDocument doc = new FlowDocument();
            doc.MaxPageWidth = 300;
            // Create a Section
            Section sec = new Section();



            // Create Paragraphs
            Paragraph p1 = new Paragraph(); //Company logo or name
            p1.Inlines.Add(new Run(" "));
            p1.TextAlignment = TextAlignment.Center;
            Paragraph p2 = new Paragraph(); //
            Paragraph p3 = new Paragraph(); //Products
            Paragraph p4 = new Paragraph(); //
            Paragraph p5 = new Paragraph(); //
            Paragraph p6 = new Paragraph(); //

            ////Create Logo

            //if (File.Exists(Switcher.pageSwitcher.InfoContainer.Information.Logo))
            //{
            //    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            //    BitmapImage bimg = new BitmapImage();
            //    bimg.BeginInit();
            //    bimg.UriSource = new Uri(Switcher.pageSwitcher.InfoContainer.Information.Logo, UriKind.RelativeOrAbsolute);
            //    bimg.EndInit();
            //    image.Source = bimg;
            //    image.MaxHeight = 155;
            //    image.MaxWidth = 200;
            //    image.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;

            //    p1.Inlines.Add(new InlineUIContainer(image));
            //}
            //else
            //{
            //    p1.Inlines.Add(new Run(info.CompanyName));
            //}


            // insert text
            p6.FontSize = 12;
            p2.FontSize = 12;
            p3.FontSize = 12;
            p4.FontSize = 12;
            p5.FontSize = 12;
            p2.TextAlignment = TextAlignment.Center;
            //p2.Inlines.Add(new Run("------------------------------------------------------------\n"));
            //p2.Inlines.Add(new Run(info.Street + " " + info.Number + "\n" + info.ZipCode + " " + info.City + "\nTel.: " + info.Telephone + "  Fax: " + info.Fax + "\nBTW: " + info.Btw + "\nWebsite: " + info.WebSite));
            //if (info.OpeningsHours.Count() >= 5)
            //{
            //    p2.Inlines.Add(new Run("\n\n" + info.OpeningsHours));
            //}
            p2.Inlines.Add(new Run(DateTime.Now.ToString()));
            p2.Inlines.Add(new Run("\n----------------------------------------------------------"));
            //p3.Inlines.Add(new Run("Tafel:\t" + bill.Table + "\n\n"));
            if (subRekening)
            {
                p3.Inlines.Add(new Run("Subrekening\n"));
            }
            p3.Inlines.Add(new Run("#\tOmschrijving\t\t   Prijs\n"));
            p3.Inlines.Add(new Run("----------------------------------------------------------\n"));
            //(producten)(((KeyValuePair<producten, int>)lbx_SubProducts.SelectedItem).Key));
            double count = 0;
            Dictionary<Product, int> countedList = list.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            foreach (KeyValuePair<Product, int> item in countedList)
            {
                //MessageBox.Show(item.ToString());
                Product product = (Product)(item).Key;
                if (product.Prd_naam.ToString().Count() < 9)
                {
                    p3.Inlines.Add(new Run(item.Value + " \t" + product.Prd_naam.ToString() + "\t\t\t   " + StringEditor.ConvertToCurrency(product.Prd_prijs) + "\n"));
                }
                else
                {
                    p3.Inlines.Add(new Run(item.Value + " \t" + StringEditor.Truncate(product.Prd_naam.ToString(), 14) + "\t\t   " + StringEditor.ConvertToCurrency(product.Prd_prijs) + "\n"));
                }
                count += product.Prd_prijs;
            }
            p3.Inlines.Add(new Run("\t\t\t\t   --------\n"));
            p3.Inlines.Add(new Run("\tTotaal:\t\t\t   " + StringEditor.ConvertToCurrency(count)));// + PriceString.convertToPrice(bill.TotalPrice)));
            p3.Inlines.Add(new Run("\n----------------------------------------------------------"));
            //p6.Inlines.Add(new Run("Geholpen door " + bill.Waiter.ToString() + "\nOp: " + DateTime.Now));
            //System.Windows.Controls.Image barimage = new System.Windows.Controls.Image();
            //barimage.Source = Project_Cube.Classes.ImageConverter.ToWpfImage(CreateBarCode("09072013ABC0000001"));
            //barimage.MaxHeight = 155;));//
            //barimage.MaxWidth = 200;
            //barimage.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            ////p4.Inlines.Add(new Run(""));
            //p4.TextAlignment = TextAlignment.Center;
            //p4.Inlines.Add(new InlineUIContainer(barimage));
            //p5.TextAlignment = TextAlignment.Center;
            //p5.Inlines.Add(new Run("\n" + info.ThankMessage));

            // Add Paragraphs to Section
            sec.Blocks.Add(p1);

            sec.Blocks.Add(p3);
            sec.Blocks.Add(p2);
            sec.Blocks.Add(p6);
            sec.Blocks.Add(p5);

            sec.Blocks.Add(p4);

            // Add Section to FlowDocument
            doc.Blocks.Add(sec);

            return doc;
        }


    }
}
