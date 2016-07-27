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
    /// Interaction logic for dropdown.xaml
    /// </summary>
    public partial class dropdown : UserControl
    {
        List<DayChoose> todaylist = new List<DayChoose>();
        public dropdown()
        {
            InitializeComponent();
            todaylist.Add(new DayChoose() { dayname = "Monday" });
            todaylist.Add(new DayChoose() { dayname = "Tuesday" });
            todaylist.Add(new DayChoose() { dayname = "Wednesday" });
            todaylist.Add(new DayChoose() { dayname = "Thursday" });
            todaylist.Add(new DayChoose() { dayname = "Friday" });
            todaylist.Add(new DayChoose() { dayname = "Saturday" });
            todaylist.Add(new DayChoose() { dayname = "Sunday" });
            comboboxlist_day.ItemsSource = todaylist;
            comboboxlist_day.DisplayMemberPath = "dayname";
            comboboxlist_day.SelectedValuePath = "dayname";
            comboboxlist_day.SelectedValue = "Monday";
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
