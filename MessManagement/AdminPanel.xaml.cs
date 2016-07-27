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
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : UserControl
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new LandingPageAdmin());
        }
        private void button_memberedit_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new EditMember());
        }
        private void button_employee_edit_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new EditEmployee());
        }
        private void button_vendoradd_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new EditVendors());
        }
        private void button_invoices_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new VendorsInvoices());
        }
        private void License(object sender, RoutedEventArgs e)
        {
            MenuBarFunctions.License(sender, e);
        }
        private void Contributors(object sender, RoutedEventArgs e)
        {
            MenuBarFunctions.Contributors(sender, e);
        }
    }
}
