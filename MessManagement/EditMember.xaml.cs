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
    /// Interaction logic for EditMember.xaml
    /// </summary>
    public partial class EditMember : UserControl
    {
        List<EditMemberList> memberlist = new List<EditMemberList>();
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=gaurav;";
        MySqlConnection conn = null;

        public EditMember()
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


        

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            editmember.CommitEdit();
            int roll = 0; string name = ""; string remarks = "";
            //DO check that rollno doesn't already exist(Database will give error, catch and notify user or check beforehand)
            // Query Rollno to check existance
            try
            {
                if (int.Parse(textBox_roll.Text) > 0)
                    roll = int.Parse(textBox_roll.Text);

                if (textBox_name.Text == "" ||textBox_name.Text == "Enter Member Name")
                    MessageBox.Show("Enter valid Member name");
                else
                {
                   
                    name = textBox_name.Text;

                    remarks = textBox_remark.Text;
                    if (remarks == "Enter Remark")
                        remarks = "No Remarks";
                    /*Enter in database*/
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO student VALUES(@member,@rollno,@remarks)";
                        cmd.Prepare();
                        cmd.Parameters.AddWithValue("@member", name);
                        cmd.Parameters.AddWithValue("@rollno", roll);
                        cmd.Parameters.AddWithValue("@remarks", remarks);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Member successfully added");
                        memberlist.Add(new EditMemberList() { RollNo = roll, Name = name, Remarks = remarks });
                        
                    }
                    catch
                    {
                        Console.WriteLine("Database query failed.");
                        MessageBox.Show("Possibly Member alredy exists! or Try Again");
                    }
                }
            }


            catch
            {
                MessageBox.Show("Enter valid Roll No!");
            }
           
            
         editmember.Items.Refresh();
        }
    
        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            editmember.CommitEdit();
            try
            {

                // as EditMemberList;
                Console.WriteLine(((EditMemberList) editmember.SelectedItem).Name);
                int roll = ((EditMemberList)editmember.SelectedItem).RollNo;
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM student where roll = @rollno ";
                    cmd.Parameters.AddWithValue("@rollno", roll);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    memberlist.Remove((EditMemberList)editmember.SelectedItem);
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
                editmember.Items.Refresh();
            }
            editmember.Items.Refresh();
        }

        
        private void LoadMemberList()
        {
            int success = LoadFromDatabase();
            editmember.ItemsSource = memberlist; //Can't be changed in runtime. Change list instead and refresh
        }

        private int LoadFromDatabase()
        {

            try
            {
                string str = "SELECT * from student";
                //Console.WriteLine(str);
                MySqlDataReader dr = null;
                MySqlCommand cmd = new MySqlCommand(str, conn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    memberlist.Add(new EditMemberList() { RollNo = dr.GetInt32(1), Name = dr.GetString(0), Remarks = dr.GetString(2) });

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
        
    }
    public class EditMemberList
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
    }
}
