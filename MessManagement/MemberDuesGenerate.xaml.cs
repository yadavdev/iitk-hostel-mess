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
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.IO;
using MySql.Data.MySqlClient;

namespace MessManagement
{
    /// <summary>
    /// Interaction logic for MemberDuesGenerate.xaml
    /// </summary>
    public partial class MemberDuesGenerate : UserControl
    {
        private string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=gaurav;";
        private MySqlConnection conn = null;

        public MemberDuesGenerate()
        {
            InitializeComponent();
            startdate.SelectedDate = DateTime.Today;
            enddate.SelectedDate = DateTime.Today;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error: Connecting to the Database. Contact Administrator or Retry.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new LandingPageFrontend());
        }

        private void button_generate_Click(object sender, RoutedEventArgs e)
        {
            button_back.IsEnabled = false;
            try
            {
                FileInfo newFile = new FileInfo("messdues.xlsx");
                DateTime startdateval = startdate.SelectedDate.Value.Date;
                DateTime enddateval = startdate.SelectedDate.Value.Date;
                Console.WriteLine("startdate: " + startdateval.ToShortDateString());
                Console.WriteLine("startdate: " + enddateval.ToShortDateString());
                /*Check if date is set or not and all fields required set*/
                using (ExcelPackage objExcelPackage = new ExcelPackage(newFile))
                {
                    ExcelWorksheet ws = objExcelPackage.Workbook.Worksheets.Add("Dues");
                    ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
                    ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet
                                                          //Merging cells and create a center heading for out table
                    ws.Cells[1, 1].Value = "Mess Dues Students"; // Heading Name
                    ws.Cells[1, 1, 1, 6].Merge = true; //Merge columns start and end range
                    ws.Cells[1, 1, 1, 6].Style.Font.Bold = true; //Font should be bold
                    ws.Cells[1, 1, 1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Aligmnet is center
                    ws.Cells[2, 1].Value = "For Date" + startdateval.ToShortDateString() + " to " + enddateval.ToShortDateString();
                    ws.Cells[3, 1].Value = "Sno";
                    ws.Cells[3, 2].Value = "Rollno";
                    ws.Cells[3, 3].Value = "Name";
                    ws.Cells[3, 4].Value = "Mess Extras";
                    ws.Cells[3, 5].Value = "Total";
                    try
                    {
                        string sql = "SELECT * from snt WHERE date>='@startdate' and date<='@enddate 23:59:59.999'";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        //cmd.Connection.Open();
                        cmd.Parameters.AddWithValue("@startdate", startdateval.ToShortDateString());
                        cmd.Parameters.AddWithValue("@password", enddateval.ToShortDateString());
                        MySqlDataReader dr = null;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            if (dr.GetInt32(2) == 0)
                            {
                                Switcher.Switch(new LandingPageAdmin());
                            }
                            else
                            {
                                Switcher.Switch(new LandingPageFrontend());
                            }
                            if (conn != null)
                                conn.Close();
                            Console.WriteLine("Check after Switch 1,2,3");
                        }
                        else
                        {
                            MessageBox.Show("Id or Password Incorrect.\n", "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        if (dr != null)
                            dr.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error Database queries Failed:\n" + ex.ToString());
                    }
                    objExcelPackage.Save();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Excel Generation Failed. Retry or Contact Administrator\n" + ex.ToString());
            }
            button_back.IsEnabled = false;
        }
    }
}
