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

namespace MessManagement
{
    /// <summary>
    /// Interaction logic for VendorsInvoices.xaml
    /// </summary>
    public partial class VendorsInvoices : UserControl
    {
        Dictionary<uint, List<PurchaseInvoice>> invoiceslist = new Dictionary<uint, List<PurchaseInvoice>>();
        List<String> vendorlist = new List<string>();
        public VendorsInvoices()
        {
            InitializeComponent();
            LoadDefault();
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_reload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {

        }
        private void button_remove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadDefault()
        {


            invoiceslist.Add(0, new List<PurchaseInvoice>());
            invoiceslist[0].Add(new PurchaseInvoice() { SNo = 21, InvNo = 43242, Purchase = 433, Item = "Milk", Discount = 43, NetAmount = 489, InvDate = DateTime.Now });

            gridinvoice.ItemsSource = invoiceslist[0];
            vendorlistBox.ItemsSource = vendorlist;
        }

        private void vendorlistBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    public class PurchaseInvoice
    {
        public uint SNo { get; set; }
        public uint InvNo { get; set; }
        public DateTime InvDate { get; set; }
        public string Item { get; set; }
        public int Purchase { get; set; }
        public int Discount { get; set; }
        public int NetAmount { get; set; }
    }
    public class VendorListClass
    {
        public uint SNo { get; set; }
        public String Name { get; set; }
    }
}
