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
    /// Interaction logic for LandingPageAdmin.xaml
    /// </summary>
    public partial class LandingPageAdmin : UserControl
    {
        public LandingPageAdmin()
        {
            InitializeComponent();
        }

        private void button_student_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new LandingPageFrontend());
        }

        private void button_admin_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new AdminPanel());
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Login());
        }
    }
}
