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
    /// Interaction logic for EditMember.xaml
    /// </summary>
    public partial class EditMember : UserControl
    {
        List<EditMemberList> memberlist = new List<EditMemberList>();

        public EditMember()
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
            editmember.CommitEdit();
            int success = SaveToDatabase();
            if (success == 1)
            {
                //Show A POPUP THAT SAVED SUCCESSFULLY
                //LOAD FROM DATABASE AND ASK TO CHECK THAT CHANGES ARE REFLECTED
                int loadsuccess = LoadFromDatabase();
                editmember.Items.Refresh();
            }
            else
            {
                //SHOW A POPUP/IN A LABEL THAT SAVE FAILED and WHAT TO DO NEXT
            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            editmember.CommitEdit();
        }

        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            editmember.CommitEdit();
        }

        private void button_reload_Click(object sender, RoutedEventArgs e)
        {
            editmember.CommitEdit();
            int loadsuccess = LoadFromDatabase();
            editmember.Items.Refresh();
        }

        private void LoadMemberList()
        {
            int success = LoadFromDatabase();
            editmember.ItemsSource = memberlist; //Can't be changed in runtime. Change list instead and refresh
        }

        private int LoadFromDatabase()
        {
            memberlist.Add(new EditMemberList() { RollNo = 13001, Name = "John Legends", Remarks = "Hall-2" });
            memberlist.Add(new EditMemberList() { RollNo = 13048, Name = "Ranganathan Srinivasan", Remarks = "Hall-3" });
            memberlist.Add(new EditMemberList() { RollNo = 13049, Name = "Ranganathan Mutthunathan", Remarks = "Hall-5" });
            memberlist.Add(new EditMemberList() { RollNo = 13050, Name = "Terminator Adams", Remarks = "Hall-10" });

            return 1;
        }

        private int SaveToDatabase()
        {
            //TO DO: Save only when changes noticed in the list (Add a check maybe)
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
