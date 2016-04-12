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
    /// Interaction logic for LandingPageFrontend.xaml
    /// </summary>
    public partial class LandingPageFrontend : UserControl
    {
        public LandingPageFrontend()
        {
            InitializeComponent();
        }
        
        private void button_menuchoose_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MenuChoose());
        }

        private void button_gen_excel_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MemberDuesGenerate());
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Login());
        }

        private void button_weekmenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new WeeklyMenu());
        }
    }
}
