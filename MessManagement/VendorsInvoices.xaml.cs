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

        public VendorsInvoices()
        {
            InitializeComponent();
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
    }
    public class VendorInvoice
    {
        public uint SNo { get; set; }
        public uint InvNo { get; set; }
        public DateTime InvDate { get; set; }
        public string Item { get; set; }
        public int Purchase { get; set; }
        public int Discount { get; set; }
        public int NetAmount { get; set; }
    }
}
