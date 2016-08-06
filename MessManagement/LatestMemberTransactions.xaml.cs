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
using MySql.Data.MySqlClient;
using log4net;

namespace MessManagement
{
    /// <summary>
    /// Interaction logic for LatestMemberTransactions.xaml
    /// </summary>
    public partial class LatestMemberTransactions : UserControl
    {
        List<MemberTransaction> trans_list = new List<MemberTransaction>();
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=rootpa55word;";
        MySqlConnection conn = null;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LatestMemberTransactions()
        {
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);

            }
            catch (MySqlException ex)
            {
                log.Error("Error: " + ex.Message);
                MessageBox.Show("Dasebase Connection Failed, Go back and Try Again");
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            InitializeComponent();
            LoadTrans();
        }

        private void button_remove_trans_Click(object sender, RoutedEventArgs e)
        {
            button_back.IsEnabled = false;
            latestmembertransactions.CommitEdit();
            try
            {
                Console.WriteLine("Transaction Removed.");
                trans_list[latestmembertransactions.SelectedIndex].Price = 0;
                //trans_list[latestmembertransactions.SelectedIndex].Quantity = 0;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "update smt set price = 0 where sid ="+((MemberTransaction)latestmembertransactions.SelectedItem).SNo;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Reset Transaction Successful");

            }
            catch(Exception exception)
            {
                Console.WriteLine("Error in removing Latest Transaction.");
                Console.WriteLine(exception);

            }
            latestmembertransactions.Items.Refresh();
            button_back.IsEnabled = true;
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            UpdateDatabase();
            if (conn != null)
                conn.Close();
            Switcher.Switch(new MemberEntry());
        }

        private void LoadTrans()
        {
            LoadFromDatabase();
            latestmembertransactions.ItemsSource = trans_list;
        }

        private void LoadFromDatabase()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from smt ORDER By date DESC LIMIT 3";
                cmd.Prepare();
                MySqlDataReader dr = null;
                dr = cmd.ExecuteReader();            
                if (!dr.HasRows)
                {
                    MessageBox.Show("No data returned from database. Contact Administrator\n", "No data", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                { //Creating Dictionary
                    while (dr.Read())
                    {
                        Console.WriteLine(dr["roll"] + " " + dr["item"]);
                        trans_list.Add(new MemberTransaction() {SNo = Convert.ToInt32(dr["sid"]),Roll = Convert.ToInt32(dr["roll"]), Name = dr["item"].ToString(), Quantity = Convert.ToInt32(dr["quantity"]), Price = Convert.ToInt32(dr["price"]) });
                    }
                }
                if (dr != null)
                    dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Database queries Failed:\n" + ex.ToString());
            }
            //trans_list.Add(new MemberTransaction() { SNo = 1, Roll = 11001, Name = "Chicken Curry", Quantity = 1, Price = 86 });
            //trans_list.Add(new MemberTransaction() { SNo = 2, Roll = 14073, Name = "Veg Curry", Quantity = 4, Price = 345 });
            //trans_list.Add(new MemberTransaction() { SNo = 3, Roll = 11342, Name = "Hall2 Special Curry", Quantity = 2, Price = 104 });
        }

        private void UpdateDatabase()
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

    public class MemberTransaction
    {
        public int SNo { get; set; }
        public int Roll { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

}