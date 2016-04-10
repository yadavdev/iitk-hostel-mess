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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=gaurav;";
        MySqlConnection conn = null;
        public Login()
        {
            InitializeComponent();
            try
            {
                conn = new MySqlConnection(cs);
                //conn.Open();
                //Console.WriteLine("MySQL version : {0}", conn.ServerVersion);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error: Connecting to the Database. Contact Administrator or Retry.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string password = passwordBox_password.Password;
                string id = textBox_id.Text;
                if(password=="" || id == "" )
                {
                    MessageBox.Show("Add Id and Password to Login.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                }
                else
                {
                    string sql = "SELECT * from login WHERE id=@id and password=@password";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@password", password);
                    MySqlDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.GetInt32(2) == 1)
                        {
                            Switcher.Switch(new LandingPageAdmin());
                        }
                        else
                        {
                            Switcher.Switch(new LandingPageFrontend());
                        }
                        Console.WriteLine("Check after Switch 1,2,3");
                    }
                    else
                    {
                        MessageBox.Show("Id or Password Incorrect.\n" , "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    if (dr != null)
                        dr.Close();
                    if (conn != null)
                        cmd.Connection.Close();
                }

                
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Error: During Database Connection. Contact Administrator or Retry.\n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
