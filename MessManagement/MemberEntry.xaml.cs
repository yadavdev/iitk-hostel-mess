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
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=rootpa55word;";
        MySqlConnection conn = null;
        public MemberEntry()
        {
            InitializeComponent();
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);
                LoadExtras();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Dasebase Connection Failed, Go back and Try Again or Contact Administrator");

            }

        }
        private void LoadExtras()
        {
            menutoday.ItemsSource = MenuTemp.tempmenu;
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {
            /*Logic to Store and Check Member Id from database*/
            try
            {
                int rolltosend = int.Parse(id_field.Text);
                Console.WriteLine(rolltosend.ToString());
                //check if in database
                try
                {
                    string str = "SELECT * from student where roll ="+ rolltosend.ToString();
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
                        Switcher.Switch(new MainExtraEntry(rolltosend, dr["name"].ToString() ));
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
