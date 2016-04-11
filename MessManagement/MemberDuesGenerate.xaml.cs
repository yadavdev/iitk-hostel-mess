﻿using System;
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
            button_generate.IsEnabled = false;
            Dictionary<int, StudentData> tempdata = new Dictionary<int, StudentData >();
            try
            {
                string sql = "SELECT * from Student ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                Console.WriteLine(sql);
                MySqlDataReader dr = null;
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    MessageBox.Show("No data returned from database. Check Dates or Contact Administrator\n", "Excel no data", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                { //Creating Dictionary
                    while (dr.Read())
                    {
                        Console.WriteLine(dr["roll"] + " " + dr["name"] );
                        tempdata.Add(Convert.ToInt32(dr["roll"]),new StudentData());
                        tempdata[Convert.ToInt32(dr["roll"] )].Name = dr["name"].ToString();
                    }
                }
                if (dr != null)
                    dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Database queries Failed:\n" + ex.ToString());
            }


            try
            {
                FileInfo newFile = new FileInfo("messdues.xlsx");
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new FileInfo("messdues.xlsx");
                }
                DateTime startdateval = startdate.SelectedDate.Value.Date;
                DateTime enddateval = enddate.SelectedDate.Value.Date;
                Console.WriteLine("startdate: " + string.Format("{0:yyyy-MM-dd}", startdateval));
                Console.WriteLine("enddate: " + string.Format("{0:yyyy-MM-dd}", enddateval));
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
                    ws.Cells[2, 1].Value = "For Dates(both inclusive): " + startdateval.ToShortDateString() + " to " + enddateval.ToShortDateString();
                    ws.Cells[3, 1].Value = "Sno";
                    ws.Cells[3, 2].Value = "Rollno";
                    ws.Cells[3, 3].Value = "Name";
                    ws.Cells[3, 4].Value = "Mess Extras";
                    ws.Cells[3, 5].Value = "Total";
                    try
                    {
                        string sql = "SELECT * from Smt WHERE date<='"+ string.Format("{0:yyyy-MM-dd}", enddateval) + " 23:59:59:999' and date>='"+ string.Format("{0:yyyy-MM-dd}", startdateval) + "'";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        //cmd.Connection.Open();
                        //cmd.Parameters.AddWithValue("@startdate", string.Format("{0:yyyy-MM-dd}", startdateval));
                        //cmd.Parameters.AddWithValue("@enddate", string.Format("{0:yyyy-MM-dd}", enddateval));
                        Console.WriteLine(sql);
                        MySqlDataReader dr = null;
                        dr = cmd.ExecuteReader();
                        if(!dr.HasRows)
                        {
                            MessageBox.Show("No data returned from database. Check Dates or Contact Administrator\n", "Excel no data", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        { //Creating Dictionary
                            while (dr.Read())
                            {
                                Console.WriteLine(dr["roll"] + " " + dr["price"] );
                                tempdata[Convert.ToInt32(dr["roll"])].Price += Convert.ToInt32(dr["price"]);
                            }
                            int i = 4;
                            int sno = 1;
                            foreach (KeyValuePair<int,StudentData> mydata in tempdata)
                            {
                                ws.Cells[i, 1].Value = sno;
                                ws.Cells[i, 2].Value = mydata.Key;
                                ws.Cells[i, 3].Value = mydata.Value.Name;
                                ws.Cells[i, 4].Value = mydata.Value.Price;
                                ws.Cells[i, 5].Value = mydata.Value.Price;
                                sno += 1;
                                i += 1;
                            }
                            objExcelPackage.Save();
                            MessageBox.Show("Excel File generated from database!", "Generation Succed",MessageBoxButton.OK,MessageBoxImage.Information);

                            
                        }
                        if (dr != null)
                            dr.Close();

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error Database queries Failed:\n" + ex.ToString());
                    }
                   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Excel Generation Failed. Retry or Contact Administrator\n" + ex.ToString());
            }
            button_back.IsEnabled = true;
            button_generate.IsEnabled = true;
        }
    }
    public class StudentData
    {
        public string Name { get; set; }

        public int Price { get; set; }
    }

}
