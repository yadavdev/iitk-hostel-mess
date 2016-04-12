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
    public partial class EditEmployee : UserControl
    {
        List<EditEmployeeList> employeelist = new List<EditEmployeeList>();
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=gaurav;";
        MySqlConnection conn = null;

        public EditEmployee()
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
            LoadMemberList();
        }
        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            gridemployee.CommitEdit();
            int success = SaveToDatabase();
            if (success == 1)
            {
                //Show A POPUP THAT SAVED SUCCESSFULLY
                //LOAD FROM DATABASE AND ASK TO CHECK THAT CHANGES ARE REFLECTED
                int loadsuccess = LoadFromDatabase();
                gridemployee.Items.Refresh();
            }
            else
            {
                //SHOW A POPUP/IN A LABEL THAT SAVE FAILED and WHAT TO DO NEXT
            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            gridemployee.CommitEdit();
            //Dissallow same SNo.
            string name="",category="",address="",accno="",bankName="";
            long wage = 0; long mobile=0;
            try
            {
                if (uint.Parse(textBox_wage.Text) > 0)
                    wage = uint.Parse(textBox_wage.Text);

                if (textBox_name.Text == "" || textBox_name.Text == "Enter Member Name")
                    MessageBox.Show("Enter valid Member name");
                else if (textBox_category.Text == "" || textBox_category.Text == "Enter Wage Category")
                    MessageBox.Show("Enter valid Category");
                else
                {
                    try
                    {
                        if (long.Parse(textBox_mobile.Text) > 0)
                        {
                            mobile = long.Parse(textBox_mobile.Text);
                            if (!(mobile >= 1000000000 && mobile <= 9999999999))
                            { MessageBox.Show("Invalid Mobile Number!, Please Check"); }
                            else
                            {
                                //Mobile no correct
                                name = textBox_name.Text;
                                category = textBox_category.Text;
                                address = textBox_Address.Text;
                                accno = textBox_Account.Text;
                                bankName = textBox_Bank.Text;
                                if (address == "Enter Address")
                                    address = "";
                                if (accno == "Enter Account No.")
                                    accno = "";
                                if (bankName == "Enter Bank Name")
                                    bankName = "";
                                /*Enter in database*/
                                try
                                {
                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.Connection = conn;
                                    cmd.CommandText = "INSERT INTO employees(name,addr,mobno,category,wage,accno,bankname) VALUES(@member,@addr,@mobno,@cat,@wage,@accno,@bank)";
                                    cmd.Prepare();
                                    cmd.Parameters.AddWithValue("@member", name);
                                    cmd.Parameters.AddWithValue("@addr", address);
                                    cmd.Parameters.AddWithValue("@mobno", mobile.ToString());
                                    cmd.Parameters.AddWithValue("@cat", category);
                                    cmd.Parameters.AddWithValue("@wage", wage);
                                    cmd.Parameters.AddWithValue("@accno", accno);
                                    cmd.Parameters.AddWithValue("@bank", bankName);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Member successfully added");
                                    textBox_name.Text = "Enter Member Name";
                                    textBox_wage.Text = "Enter Wage";
                                    textBox_mobile.Text = "Enter Mobile No.";
                                    textBox_category.Text = "Enter Wage Category";
                                    textBox_Bank.Text = "Enter Bank Name";
                                    textBox_Address.Text = "Enter Address";
                                    textBox_Account.Text = "Enter Account No.";
                                    //employeelist.Add(new EditEmployeeList() { Name = name, Bank = bankName, Wages = wage, Category = category });
                                    LoadFromDatabase();
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine("Database query failed." + ex.ToString());
                                    MessageBox.Show("Possibly Member alredy exists! or Try Again"+ ex.ToString());
                                }
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Mobile Number or Wage!, Please Check");
                    }
                }
                    

                    
                }
            


            catch
            {
                MessageBox.Show("Enter valid Wage Amount!");
            }


           // employeelist.Add(new EditEmployeeList() { SNo = 4, Name = "Terminator Adams", Bank = "ICICI", Wages = 300, Category = "UnSkilled" });
            gridemployee.Items.Refresh();
        }

        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            gridemployee.CommitEdit();
            try
            {
                uint sno = ((EditEmployeeList)gridemployee.SelectedItem).SNo;
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM employees where sno = @sno ";
                    cmd.Parameters.AddWithValue("@sno", sno);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    employeelist.Remove((EditEmployeeList)gridemployee.SelectedItem);
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
                gridemployee.Items.Refresh();
            }
        }

        private void button_reload_Click(object sender, RoutedEventArgs e)
        {
            gridemployee.CommitEdit();
            int loadsuccess = LoadFromDatabase();
            gridemployee.Items.Refresh();
        }

        private void LoadMemberList()
        {
            int success = LoadFromDatabase();
            gridemployee.ItemsSource = employeelist; //Can't be changed in runtime. Change list instead and refresh
        }

        private int LoadFromDatabase()
        {
            uint Sno = 0;
            employeelist.Clear();
            try
            {
                string str = "SELECT * from employees";
                //Console.WriteLine(str);
                MySqlDataReader dr = null;
                MySqlCommand cmd = new MySqlCommand(str, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Sno++;
                    employeelist.Add(new EditEmployeeList() { SNo = dr.GetUInt32(0), Name = dr.GetString(1), Bank = dr.GetString(7), Wages = dr.GetUInt32(5), Category = dr.GetString(4) });

                }
                if (dr != null)
                    dr.Close();
            }
            catch
            {
                MessageBox.Show("Database query failed");
            }
            //employeelist.Add(new EditEmployeeList() { SNo = 1, Name = "John Legends", Bank = "SBI", Wages=400, Category="Skilled" });
            
            return 1;
        }

        private int SaveToDatabase()
        {
            //TO DO: Save only when changes noticed in the list (Add a check maybe)
            return 1;
        }

        private void button_back_Click_1(object sender, RoutedEventArgs e)
        {
            //Switcher.Switch(new ____());
            if (conn != null)
                conn.Close();
            Switcher.Switch(new AdminPanel());
        }
    }
    public class EditEmployeeList
    {
        public uint SNo { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public uint Wages { get; set; }
        public string Bank { get; set; }
        public ulong AccNo  { get; set; }
    }
}
