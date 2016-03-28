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
    /// Interaction logic for EditVendors.xaml
    /// </summary>
    public partial class EditEmployee : UserControl
    {
        List<EditEmployeeList> employeelist = new List<EditEmployeeList>();

        public EditEmployee()
        {
            InitializeComponent();
            LoadMemberList();
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            //Switcher.Switch(new ____());
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
            employeelist.Add(new EditEmployeeList() { SNo = 4, Name = "Terminator Adams", Bank = "ICICI", Wages = 300, Category = "UnSkilled" });
            gridemployee.Items.Refresh();
        }

        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            gridemployee.CommitEdit();
            try
            {
                var selectedIndex = gridemployee.SelectedIndex;
                employeelist.RemoveAt(selectedIndex);
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
            employeelist.Add(new EditEmployeeList() { SNo = 1, Name = "John Legends", Bank = "SBI", Wages=400, Category="Skilled" });
            employeelist.Add(new EditEmployeeList() { SNo = 2, Name = "Ranganathan Srinivasan", Bank = "SBI", Wages=400, Category = "Skilled" });
            employeelist.Add(new EditEmployeeList() { SNo = 3, Name = "Ranganathan Mutthunathan", Bank = "HDFC", Wages =500, Category = "Manager" });
            employeelist.Add(new EditEmployeeList() { SNo = 4, Name = "Terminator Adams", Bank = "ICICI", Wages=300, Category = "UnSkilled" });

            return 1;
        }

        private int SaveToDatabase()
        {
            //TO DO: Save only when changes noticed in the list (Add a check maybe)
            return 1;
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
