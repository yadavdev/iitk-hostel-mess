using System;
using System.Collections.Generic;
using System.Timers;
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
using MySql.Data.MySqlClient;
namespace MessManagement
{
    /// <summary>
    /// Interaction logic for MemberEntry.xaml
    /// </summary>
    public partial class MemberEntry : UserControl
    {
        List<DayMenu> todayextralist = new List<DayMenu>();
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=gaurav;";
        MySqlConnection conn = null;
        public MemberEntry()
        {
            InitializeComponent();
            LoadExtras();
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
        }
        private void LoadExtras()
        {
            todayextralist.Add(new DayMenu() { Name = "Rasgulla", Price = 10 });
            todayextralist.Add(new DayMenu() { Name = "Chicken Curry", Price = 65 });
            todayextralist.Add(new DayMenu() { Name = "Gajar Ka Halwa", Price = 15 });
            todayextralist.Add(new DayMenu() { Name = "Palak Paneer", Price = 22 });
            menutoday.ItemsSource = todayextralist;
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {
            /*Logic to Store and Check Member Id from database*/
            try
            {
                int argtosend = int.Parse(id_field.Text);
                Console.WriteLine(argtosend.ToString());
                //check if in database
                try
                {
                    string str = "SELECT * from student where roll ="+ argtosend.ToString();
                    Console.WriteLine(str);
                    MySqlDataReader dr = null;
                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    dr = cmd.ExecuteReader();
                    if (!dr.Read())
                    {
                        label_error.Content = "Roll No not found";
                        if (dr != null)
                        {
                            dr.Close();
                        }
                    }
                    else
                    {
                        Switcher.Switch(new MainExtraEntry(argtosend));
                    }  
                }
                catch
                {
                    label_error.Content = "Database query failed.";
                }
            }
            catch
            {
                label_error.Content = "Parse Error: Input an integer.";
            }
        }

        private void button_latest_transactions_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new LatestMemberTransactions());
        }
        
    }
}
