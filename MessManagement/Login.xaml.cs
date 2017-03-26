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
using System.Configuration;

namespace MessManagement
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private string cs = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        private MySqlConnection conn = null;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Login()
        {
            InitializeComponent();
            try
            {
                /* Configure log4net to use the application base directory to store logs */
                log4net.GlobalContext.Properties["ApplicationDataBaseDirectory"] = ConfigurationManager.AppSettings["ApplicationDataBaseDirectory"];
                log4net.Config.XmlConfigurator.Configure();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Setting the log directory. Contact Administrator.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                /* Set a default directory */
                log4net.GlobalContext.Properties["ApplicationDataBaseDirectory"] = @"C:\MessManagement";
                log4net.Config.XmlConfigurator.Configure();
                log.Error("Error: " + ex.Message);
            }

            try
            {
                /* Try MySql Connection and check if successful */
                conn = new MySqlConnection(cs);
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);

            }
            catch (MySqlException ex)
            {
                log.Error("Error: " + ex.Message);
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
                    //cmd.Connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@password", password);
                    MySqlDataReader dr = null;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.GetInt32(2) == 0)
                        {
                            if (dr != null)
                                dr.Close();
                            if (conn != null)
                                conn.Close();
                            Switcher.Switch(new LandingPageAdmin());
                        }
                        else
                        {
                            if (dr != null)
                                dr.Close();
                            if (conn != null)
                                conn.Close();
                            Switcher.Switch(new LandingPageFrontend());
                        }
                        if (conn != null)
                            conn.Close();
                        Console.WriteLine("Check after Switch 1,2,3");
                    }
                    else
                    {
                        MessageBox.Show("Id or Password Incorrect.\n" , "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    if (dr != null)
                        dr.Close();
                }

                
            }
            catch(MySqlException ex)
            {
                log.Error("Error: " + ex.Message);
                MessageBox.Show("Error: During Database Connection. Contact Administrator or Retry.\n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
}
