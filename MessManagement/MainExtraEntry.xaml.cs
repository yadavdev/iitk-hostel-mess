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
        int memberid = 0;
        string membername = "";
        public MainExtraEntry(int arg)
        {
            try
            {
                InitializeComponent();
                memberid = arg;
                LoadExtras();
                todayspecialextra.ItemsSource = today_special;
                fixedextra.ItemsSource = today_fixed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nClick Okay to go back", "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                Switcher.Switch(new MemberEntry());
            }

        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MemberEntry());
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {
            UpdateDatabase(); /*And Add to Latest Transaction*/
            Switcher.Switch(new MemberEntry());
        }
        private void LoadExtras()
        {
            try
            {
                //load from database where membername = memberid
                //set membername = datafromquery['name']
                label_name.Content = membername;
                label_rollno.Content = memberid;


                today_special.Add(new DailyMenuEntry() { Name = "Chicken Curry", Quantity = 0, Price = 86 });
                today_special.Add(new DailyMenuEntry() { Name = "Veg Curry", Quantity = 0, Price = 345 });
                today_special.Add(new DailyMenuEntry() { Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });
                today_special.Add(new DailyMenuEntry() { Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });
                today_special.Add(new DailyMenuEntry() { Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });

                today_fixed.Add(new DailyMenuEntry() { Name = "Chicken Curry", Quantity = 0, Price = 86 });
                today_fixed.Add(new DailyMenuEntry() { Name = "Veg Curry", Quantity = 0, Price = 345 });
                today_fixed.Add(new DailyMenuEntry() { Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });
                today_fixed.Add(new DailyMenuEntry() { Name = "Chicken Curry", Quantity = 0, Price = 86 });
                today_fixed.Add(new DailyMenuEntry() { Name = "Veg Curry", Quantity = 0, Price = 345 });
                today_fixed.Add(new DailyMenuEntry() { Name = "Hall2 Special Curry", Quantity = 0, Price = 104 });
            }
            catch(Exception ex){
                MessageBox.Show("Error: " + ex.Message + "\nClick okay to go back", "Database Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                Switcher.Switch(new MemberEntry());
            }
        }
        private void UpdateDatabase()
        {

        }
    }

    public class DailyMenuEntry
    {
        public string Name { get; set; }
        public uint Quantity { get; set; }
        public int Price { get; set; }
    }
}
