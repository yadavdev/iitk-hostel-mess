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

namespace MessManagement
{
    /// <summary>
    /// Interaction logic for EditVendors.xaml
    /// </summary>
    public partial class EditVendors : UserControl
    {
        List<EditVendorList> vendorlist = new List<EditVendorList>();
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=rootpa55word;";
        MySqlConnection conn = null;

        public EditVendors()
        {
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
            InitializeComponent();
            LoadVendorList();

        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
           gridvendor.CommitEdit();
           string name = ""; string article = "";
            //DO check that rollno doesn't already exist(Database will give error, catch and notify user or check beforehand)
            // Query Rollno to check existance
            try
            {
               
                if (textBox_name.Text == "" || textBox_name.Text == "Enter Vendor Name")
                    MessageBox.Show("Enter valid Vendor name");
                else
                {

                    name = textBox_name.Text;
                    article = textBox_article.Text;
                    if (article == "Enter Article" || article == "")
                        MessageBox.Show("Enter valid Article name");
                    else
                    {
                        /*Enter in database*/
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "INSERT INTO supplier(name,article) VALUES(@name,@article)";
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@article", article);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Vendor successfully added");
                            textBox_article.Text = "Enter Article";
                            textBox_name.Text = "Enter Vendor Name";
                            //vendorlist.Add(new EditVendorList() { Name = name, Article = article });
                            LoadFromDatabase();
                        }
                        catch
                        {
                            Console.WriteLine("Database query failed.");
                            MessageBox.Show("Possibly Member alredy exists! or Try Again");
                        }
                    }
                }
            }


            catch
            {
                MessageBox.Show("Enter valid Roll No!");
            }


            gridvendor.Items.Refresh();
        }

        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            gridvendor.CommitEdit();
            try
            {
                uint sno = ((EditVendorList)gridvendor.SelectedItem).SNo;
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM supplier where sid = @sno ";
                    cmd.Parameters.AddWithValue("@sno", sno);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    vendorlist.Remove((EditVendorList)gridvendor.SelectedItem);
                }
                catch
                {
                    Console.WriteLine("Dasebase Deletion Failed");
                    MessageBox.Show("Dasebase Deletion Failed,Please Try Again");
                }


            }
            catch (Exception exception)
            {
                Console.Write("\n\n" + exception.ToString() + "\n\n");
            }
            finally
            {
                gridvendor.Items.Refresh();
            }
        }
        private void LoadVendorList()
        {
            int success = LoadFromDatabase();
            gridvendor.ItemsSource = vendorlist; //Can't be changed in runtime. Change list instead and refresh
        }

        private int LoadFromDatabase()
        {
            uint Sno = 0; vendorlist.Clear();
            try
            {
                string str = "SELECT * from supplier";
                //Console.WriteLine(str);
                MySqlDataReader dr = null;
                MySqlCommand cmd = new MySqlCommand(str, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Sno++;
                    vendorlist.Add(new EditVendorList() {SNo = dr.GetUInt32(0),Name = dr.GetString(1),Article = dr.GetString(2) });

                }
                if (dr != null)
                    dr.Close();
            }
            catch
            {
                MessageBox.Show("Database query failed");
            }
           
            return 1;
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            if (conn != null)
                conn.Close();
            Switcher.Switch(new AdminPanel());
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
    public class EditVendorList
    {
        public uint SNo { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        }
}
