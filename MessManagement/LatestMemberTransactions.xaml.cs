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

namespace MessManagement
{
    /// <summary>
    /// Interaction logic for LatestMemberTransactions.xaml
    /// </summary>
    public partial class LatestMemberTransactions : UserControl
    {
        List<MemberTransaction> trans_list = new List<MemberTransaction>();

        public LatestMemberTransactions()
        {
            InitializeComponent();
            LoadTrans();
        }

        private void button_remove_trans_Click(object sender, RoutedEventArgs e)
        {
            latestmembertransactions.CommitEdit();
            try
            {
                Console.WriteLine("Transaction Removed.");
                trans_list[latestmembertransactions.SelectedIndex].Price = 0;
                trans_list[latestmembertransactions.SelectedIndex].Quantity = 0;
            }
            catch(Exception exception)
            {
                Console.WriteLine("Error in removing Latest Transaction.");
                Console.WriteLine(exception);

            }
            latestmembertransactions.Items.Refresh();
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            UpdateDatabase();
            Switcher.Switch(new MemberEntry());
        }

        private void LoadTrans()
        {
            LoadFromDatabase();
            latestmembertransactions.ItemsSource = trans_list;
        }

        private void LoadFromDatabase()
        {
            trans_list.Add(new MemberTransaction() { SNo = 1, RollNo = 11001, Name = "Chicken Curry", Quantity = 1, Price = 86 });
            trans_list.Add(new MemberTransaction() { SNo = 2, RollNo = 14073, Name = "Veg Curry", Quantity = 4, Price = 345 });
            trans_list.Add(new MemberTransaction() { SNo = 3, RollNo = 11342, Name = "Hall2 Special Curry", Quantity = 2, Price = 104 });
        }

        private void UpdateDatabase()
        {

        }

    }

    public class MemberTransaction
    {
        public int SNo { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

}