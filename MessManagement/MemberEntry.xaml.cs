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
    /// Interaction logic for MemberEntry.xaml
    /// </summary>
    public partial class MemberEntry : UserControl
    {
        List<DayMenu> todayextralist = new List<DayMenu>();

        public MemberEntry()
        {
            InitializeComponent();
            LoadExtras();
        }
        private void LoadExtras()
        {
            todayextralist.Add(new DayMenu() { Name = "Rasgulla", Price = 10 });
            todayextralist.Add(new DayMenu() { Name = "Chicken Curry", Price = 65 });
            todayextralist.Add(new DayMenu() { Name = "Gajar Ka Halwa", Price = 15 });
            todayextralist.Add(new DayMenu() { Name = "Palak Paneer", Price = 22 });
            menutoday.ItemsSource = todayextralist;
        }
        
        private void button_strike(object sender, RoutedEventArgs e)
        {
            menutoday.CommitEdit();
            if (menutoday.SelectedItems.Count > 0)
            {
                try
                {
                    var rowIndex = menutoday.SelectedIndex;
                    var row = (DataGridRow)menutoday.ItemContainerGenerator.ContainerFromIndex(rowIndex);
                    Console.Write("\n\n" + row.GetType() + "\n\n");
                    row.Background = Brushes.Black;
                    
                }
                catch (Exception exception)
                {
                    Console.Write("\n\n" + exception.ToString() + "\n\n");
                }
                finally
                {
                    //menutoday.Items.Refresh();
                }
            }
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_latest_transactions_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
