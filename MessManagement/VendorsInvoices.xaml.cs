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
        Dictionary<uint, List<PurchaseInvoiceClass>> invoiceslist = new Dictionary<uint, List<PurchaseInvoiceClass>>();
        List<VendorListClass> vendorlist = new List<VendorListClass>();
        public VendorsInvoices()
        {
            InitializeComponent();
            LoadDefault();
        }
        
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            //Switcher.Switch(new ____());
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            gridinvoice.CommitEdit();
            int success = SaveToDatabase();
            if (success == 1)
            {
                //Show A POPUP THAT SAVED SUCCESSFULLY
                //LOAD FROM DATABASE AND ASK TO CHECK THAT CHANGES ARE REFLECTED
                int loadsuccess = LoadFromDatabase();
                gridinvoice.Items.Refresh();
            }
            else
            {
                //SHOW A POPUP/IN A LABEL THAT SAVE FAILED and WHAT TO DO NEXT
            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            gridinvoice.CommitEdit();
            //DO check that rollno doesn't already exist(Database will give error, catch and notify user or check beforehand)
            // Query Rollno to check existance
            invoiceslist[0].Add(new PurchaseInvoiceClass() { SNo = 21, InvNo = 43242, Purchase = 433, Item = "Milk", Discount = 43, NetAmount = 489, InvDate = DateTime.Now });
            gridinvoice.Items.Refresh();
        }

        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            gridinvoice.CommitEdit();
            try
            {
                var selectedIndex = gridinvoice.SelectedIndex;
                invoiceslist[0].RemoveAt(selectedIndex);
            }
            catch (Exception exception)
            {
                Console.Write("\n\n" + exception.ToString() + "\n\n");
            }
            finally
            {
                gridinvoice.Items.Refresh();
            }
        }

        private void button_reload_Click(object sender, RoutedEventArgs e)
        {
            gridinvoice.CommitEdit();
            int loadsuccess = LoadFromDatabase();
            gridinvoice.Items.Refresh();
        }

        private int LoadFromDatabase()
        {
            invoiceslist[0].Add(new PurchaseInvoiceClass() { SNo = 21, InvNo = 43242, Purchase = 433, Item = "Milk", Discount = 43, NetAmount = 489, InvDate = DateTime.Now });
            return 1;
        }

        private int SaveToDatabase()
        {
            //TO DO: Save only when changes noticed in the list (Add a check maybe)
            return 1;
        }

        private void LoadDefault()
        {
            invoiceslist.Add(0, new List<PurchaseInvoiceClass>());

            vendorlist.Add(new VendorListClass() { VendorNo = 1, Name = "Abeigil Enterprises" });
            vendorlist.Add(new VendorListClass() { VendorNo = 2, Name = "Alif & Sons" });
            vendorlist.Add(new VendorListClass() { VendorNo = 3, Name = "Mess Dairies" });
            int success = LoadFromDatabase();
            gridinvoice.ItemsSource = invoiceslist[0];
            //vendorcombobox.ItemsSource = vendorlist;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_excel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    public class PurchaseInvoiceClass
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
        public uint VendorNo { get; set; }
        public string Name { get; set; }
    }

}
