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
    /// Interaction logic for VendorsInvoices.xaml
    /// </summary>
    public partial class VendorsInvoices : UserControl
    {
        List<PurchaseInvoiceClass> invoiceslist = new List<PurchaseInvoiceClass>();
        List<VendorListClass> vendorlist = new List<VendorListClass>();
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=rootpa55word;";
        MySqlConnection conn = null;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public VendorsInvoices()
        {
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);
                InitializeComponent();
                date_adder.SelectedDate = DateTime.Today;
                LoadDefault();
            }
            catch (MySqlException ex)
            {
                log.Error("Error: " + ex.Message);
                Console.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error Connecting to Database");
                Switcher.Switch(new AdminPanel());
            }
        }
        
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            if (conn != null)
                conn.Close();
            Switcher.Switch(new AdminPanel());
        }

       private void button_add_Click(object sender, RoutedEventArgs e)
        {
            gridinvoice.CommitEdit();
            string invno = "", article = ""; double puramt = 0, disc = 0;
            try
            {

                if (textBox_inv.Text == "" || textBox_inv.Text == "Enter Inv.No")
                    MessageBox.Show("Enter valid Enter Invoice No.");
                else
                {

                    invno = textBox_article.Text;
                    article = textBox_article.Text;
                    if (article == "Enter Article" || article == "")
                        MessageBox.Show("Enter valid Article name");
                    else
                    {
                        double val;
                        if (Double.TryParse(textBox_purchaseAmt.Text, out val))
                            {
                            puramt = Double.Parse(textBox_purchaseAmt.Text);
                                if (Double.TryParse(textBox_Discount.Text, out val))
                                     {
                                         disc = Double.Parse(textBox_Discount.Text);
                                /*Enter in database*/
                                string date = string.Format("{0:yyyy-MM-dd}",date_adder.SelectedDate);
                                Console.WriteLine("Vendor id is:" + vendorlistbox.SelectedValue);
                                try
                                {
                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.Connection = conn;
                                    cmd.CommandText = "INSERT INTO vendorinvoices(sid,invno,item,date,puramt,discount) VALUES(@sid,@invno,@item,@date,@puramt,@discount)";
                                    cmd.Prepare();
                                    cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(vendorlistbox.SelectedValue));
                                    cmd.Parameters.AddWithValue("@date", date);
                                    cmd.Parameters.AddWithValue("@item", article);
                                    cmd.Parameters.AddWithValue("@invno", invno);
                                    cmd.Parameters.AddWithValue("@puramt",puramt );
                                    cmd.Parameters.AddWithValue("@discount",disc);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Vendor successfully added");
                                    textBox_inv.Text = "Enter Inv.No";
                                    textBox_article.Text = "Enter Article";
                                    textBox_purchaseAmt.Text = "Enter Purchase Amt";
                                    textBox_Discount.Text = "Enter Discount";
                                    
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine("Database query failed."+ex.ToString());
                                    MessageBox.Show("Database query failed. Try Again");
                                }

                            }
                            }
                        
                    }
                }
            }
            catch
            {
                MessageBox.Show("Invalid Purchase Amount or Discount!\n");
            }
            gridinvoice.Items.Refresh();
        }

        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            gridinvoice.CommitEdit();
            try
            {
                uint sno = ((PurchaseInvoiceClass)gridinvoice.SelectedItem).SNo;
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM  vendorinvoices where sno = @sno ";
                    cmd.Parameters.AddWithValue("@sno", sno);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    invoiceslist.Remove((PurchaseInvoiceClass)gridinvoice.SelectedItem);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Dasebase Deletion Failed"+ ex.ToString());
                    MessageBox.Show("Dasebase Deletion Failed,Please Try Again");
                }                
            }
            catch (Exception exception)
            {
                Console.Write("\n\n" + exception.ToString() + "\n\n");
            }
            gridinvoice.Items.Refresh();
        }

        private void LoadDefault()
        {
           
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from  supplier";
                cmd.Prepare();
                MySqlDataReader dr = null;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    vendorlist.Add(new VendorListClass() { VendorNo = Convert.ToUInt32(dr["sid"]), Name = dr["name"].ToString() });
                }
                if (dr != null)
                    dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error During Loading Vendors:\n" + ex.ToString());
            }
            //vendorlist.Add(new VendorListClass() { VendorNo = 1, Name = "Abeigil Enterprises" });
            //vendorlist.Add(new VendorListClass() { VendorNo = 2, Name = "Alif & Sons" });
            //vendorlist.Add(new VendorListClass() { VendorNo = 3, Name = "Mess Dairies" });
            gridinvoice.ItemsSource = invoiceslist;
            vendorlistbox.ItemsSource = vendorlist;
            vendorlistbox.DisplayMemberPath = "Name";
            vendorlistbox.SelectedValuePath = "VendorNo";
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gridinvoice.CommitEdit();
            try
            {
                invoiceslist.Clear();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select * from  vendorinvoices where sid=@sid";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@sid", vendorlistbox.SelectedValue);
                MySqlDataReader dr = null;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    invoiceslist.Add(new PurchaseInvoiceClass() { SNo = Convert.ToUInt32(dr["sno"]), sid = Convert.ToUInt32(dr["sid"]), InvNo = dr["invno"].ToString(), InvDate = Convert.ToDateTime(dr["date"]), Purchase = Convert.ToInt32(dr["puramt"]), Discount = Convert.ToInt32(dr["discount"]) });
                }
                if (dr != null)
                    dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error During Loading Vendors:\n" + ex.ToString());
            }
            gridinvoice.Items.Refresh();
        }

        private void button_excel_Click(object sender, RoutedEventArgs e)
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
    public class PurchaseInvoiceClass
    {
        public uint SNo { get; set; }
        public uint sid { get; set; }
        public string InvNo { get; set; }
        public DateTime InvDate { get; set; }
        public string Item { get; set; }
        public int Purchase { get; set; }
        public int Discount { get; set; }
        public int NetAmount { get; set; }
    }
    public class VendorListClass
    {
        public uint VendorNo { get; set; }
        public string Name { get; set; }
    }

}
