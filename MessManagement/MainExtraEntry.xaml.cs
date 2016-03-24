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
    /// Interaction logic for MainExtraEntry.xaml
    /// </summary>
    public partial class MainExtraEntry : UserControl
    {
        List<DailyMenuEntry> today_fixed = new List<DailyMenuEntry>();
        List<DailyMenuEntry> today_special = new List<DailyMenuEntry>();
        public MainExtraEntry()
        {
            InitializeComponent();
            LoadExtras();
            todayspecialextra.ItemsSource = today_special;
            fixedextra.ItemsSource = today_fixed;
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MemberEntry());
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadExtras()
        {
            today_special.Add(new DailyMenuEntry() { Name = "Chicken Curry", Quantity = 0, Price = 86 });
            today_special.Add(new DailyMenuEntry() { Name = "Veg Curry", Quantity = 0, Price = 345 });
            today_special.Add(new DailyMenuEntry() {  Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });
            today_special.Add(new DailyMenuEntry() { Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });
            today_special.Add(new DailyMenuEntry() { Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });

            today_fixed.Add(new DailyMenuEntry() { Name = "Chicken Curry", Quantity = 0, Price = 86 });
            today_fixed.Add(new DailyMenuEntry() { Name = "Veg Curry", Quantity = 0, Price = 345 });
            today_fixed.Add(new DailyMenuEntry() { Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });
            today_fixed.Add(new DailyMenuEntry() { Name = "Chicken Curry", Quantity = 0, Price = 86 });
            today_fixed.Add(new DailyMenuEntry() { Name = "Veg Curry", Quantity = 0, Price = 345 });
            today_fixed.Add(new DailyMenuEntry() { Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });
        }
    }

    public class DailyMenuEntry
    {
        public string Name { get; set; }
        public uint Quantity { get; set; }
        public int Price { get; set; }
    }
}
