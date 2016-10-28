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
using System.IO;
using log4net;

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
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=rootpa55word;";
        MySqlConnection conn = null;
        MySqlTransaction tr = null;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MainExtraEntry(int roll, string name)
        {
            try
            {
                InitializeComponent();
                memberid = roll;
                membername = name;
                Directory.CreateDirectory(@"C:\MessManagement\Logs");
                if (membername == null || membername == "" || memberid <0)
                {
                    Switcher.Switch(new MemberEntry());
                    MessageBox.Show("Page Load Error: \nClick Okay to go back", "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                todayspecialextra.ItemsSource = today_special;
                fixedextra.ItemsSource = today_fixed;
                todayspecialextra.CellEditEnding += ExtraEntry_CellEditEnding;
                fixedextra.CellEditEnding += ExtraEntry_CellEditEnding;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Page Load Error: " + ex.Message + "\nClick Okay to go back", "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                Switcher.Switch(new MemberEntry());
            }
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);
                LoadExtras();
            }
            catch (MySqlException ex)
            {
                log.Error("Error: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message + "\nClick Ok to go back", "Database Connection failed.", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine("Error: {0}", ex.ToString());
                if (conn != null)
                    conn.Close();
                Switcher.Switch(new MemberEntry());

            }
        }

        private void ExtraEntry_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindPath = (column.Binding as Binding).Path.Path;
                    if (bindPath == "Quantity")
                    {
                        var element = e.EditingElement as TextBox; // element.Text has the new, user-entered value
                        int x;
                        if (!(int.TryParse(element.Text, out x) && x >= 0 && x <= 10)) //remove && x < 10 if don't require a limit
                        {
                            element.Text = "0";
                            // Not using message box will result in changing the value to 0 and prevents action of enter button
                            MessageBox.Show("Quantity should be 0 or a positive integer not greater than 10.");
                            Console.WriteLine("MessageBox Ended now!");
                        }
                    }
                }
            }
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            if (conn != null)
                conn.Close();
            Switcher.Switch(new MemberEntry());
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {
            button_enter.IsEnabled = false;
            button_back.IsEnabled = false;
            UpdateDatabase(); /*And Add to Latest Transaction*/
            if (conn != null)
                conn.Close();
            Switcher.Switch(new MemberEntry());
            
        }

        private void LoadExtras()
        {
            try
            {
                label_name.Content = membername;
                label_rollno.Content = memberid;
                Directory.CreateDirectory(@"C:\MessManagement\MemberImages");
                if (File.Exists("C:\\MessManagement\\MemberImages\\" + memberid.ToString() + "_0.jpg"))
                    image_id.Source = new BitmapImage(new Uri("C:\\MessManagement\\MemberImages\\"+memberid.ToString()+"_0.jpg", UriKind.Absolute));
                else
                    image_id.Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Images/member_256x256.png"));
                int db_day = 7 * 10 + MenuTemp.fixedmealtoday;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * from menu where  day =" + db_day.ToString();
                cmd.Prepare();
                MySqlDataReader dr = null;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine(dr["article"] + " " + dr["rate"]);
                    today_fixed.Add(new DailyMenuEntry() { Name = dr["article"].ToString(), Quantity = 0, Price = Convert.ToInt32(dr["rate"]) });
                }
                if (dr != null)
                    dr.Close();
                foreach (DayMenu curr in MenuTemp.tempmenu )
                {
                    today_special.Add(new DailyMenuEntry() { Name = curr.Name, Quantity = 0, Price = curr.Price });
                }
            }
            catch(Exception ex){
                if (conn != null)
                    conn.Close();
                log.Error("Error: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message + "\nTry Again. Click okay and go back", "Error Occured", MessageBoxButton.OK, MessageBoxImage.Error);
                Switcher.Switch(new MemberEntry());
            }
        }
        private void UpdateDatabase()
        {
            todayspecialextra.CommitEdit();
            fixedextra.CommitEdit();
            if (!datavalidiated())
            {
                // This should have been tackled in cellEditEnding. Do it here too, to be extra sure.
                MessageBox.Show("Data Validiation failed. Quantity should be 0 or a positive integer not greater than 10.");
                return;
            }
            try
            {
                tr = conn.BeginTransaction();
                log.Info(memberid.ToString() + ": Transaction Start");
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = tr;
                cmd.CommandText = "INSERT INTO smt (roll,item,quantity,price) VALUES(@roll,@item,@quantity,@price)";
                cmd.Prepare();
                foreach (DailyMenuEntry curr in today_special)
                {
                    if (curr.Quantity > 0)
                    {
                        cmd.Parameters.AddWithValue("@roll", memberid);
                        cmd.Parameters.AddWithValue("@item", curr.Name);
                        cmd.Parameters.AddWithValue("@quantity", curr.Quantity);
                        cmd.Parameters.AddWithValue("@price", curr.Price * curr.Quantity);
                        cmd.ExecuteNonQuery();
                        log.Info(memberid.ToString() +": INSERT INTO smt(roll, item, quantity, price) VALUES(" + memberid.ToString()+", '"+curr.Name+"', "+curr.Quantity.ToString()+", "+ (curr.Price * curr.Quantity).ToString() + ")");
                        cmd.Parameters.Clear();
                    }
                }
                foreach (DailyMenuEntry curr in today_fixed)
                {
                    if (curr.Quantity > 0)
                    {
                        cmd.Parameters.AddWithValue("@roll", memberid);
                        cmd.Parameters.AddWithValue("@item", curr.Name);
                        cmd.Parameters.AddWithValue("@quantity", curr.Quantity);
                        cmd.Parameters.AddWithValue("@price", curr.Price * curr.Quantity);
                        cmd.ExecuteNonQuery();
                        log.Info(memberid.ToString() + ": INSERT INTO smt(roll, item, quantity, price) VALUES(" + memberid.ToString() + ", '" + curr.Name + "', " + curr.Quantity.ToString() + ", " + (curr.Price * curr.Quantity).ToString() + ")");
                        cmd.Parameters.Clear();
                    }
                }
                tr.Commit();
                log.Info(memberid.ToString() + ": Transaction Complete: ");
            }
            catch ( Exception ex)
            {
                try
                {
                    tr.Rollback();
                    log.Info(memberid.ToString() + ": Transaction Failed and Rolled Back.");
                    log.Error(memberid.ToString() + ": Transaction Failed and Rolled Back.");
                    MessageBox.Show("Transaction didn't complete Successfully:\n" + ex.Message + "\nClick okay to go back", "Rolling Back Transaction", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                catch (MySqlException ex1)
                {
                    Console.WriteLine("Error: {0}", ex1.ToString());
                    log.Info(memberid.ToString() + ": Transaction Rolling Failed.");
                    log.Error(memberid.ToString() + ": Transaction Rolling Failed.");
                    MessageBox.Show("Transaction Roll Back Failed:\n" + ex1.Message + "\nCheck Latest Transaction Table or Contact Administrator", "Rolling Back Transaction", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
            finally
            {
                if (conn != null)
                    conn.Close();
                Switcher.Switch(new MemberEntry());
            }

        }
        private bool datavalidiated()
        {
            foreach (DailyMenuEntry curr in today_special)
            {
                if (curr.Quantity < 0 || curr.Quantity > 10)
                {
                    return false;
                }
            }
            foreach (DailyMenuEntry curr in today_fixed)
            {
                if (curr.Quantity < 0 || curr.Quantity > 10)
                {
                    return false;
                }
            }
            return true;
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell cell = dataGrid.Columns[2].GetCellContent(row).Parent as DataGridCell;
                cell.Focus();
                dataGrid.BeginEdit();
            }
            catch(Exception ex)
            {
                log.Error("Selection cell failed: " + ex.ToString());
            }
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

    public class DailyMenuEntry
    {
        public string Name { get; set; }
        public uint Quantity { get; set; }
        public int Price { get; set; }
    }
}
