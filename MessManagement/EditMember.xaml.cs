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

        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            editmember.CommitEdit();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            editmember.CommitEdit();
        }

        private void button_remove_Click(object sender, RoutedEventArgs e)
        {
            editmember.CommitEdit();
        }

        private void LoadMemberList()
        {
            memberlist.Add(new EditMemberList() { RollNo = 13001, Name = "John Legends", Remarks = "Hall-2" });
            memberlist.Add(new EditMemberList() { RollNo = 13048, Name = "Ranganathan Srinivasan", Remarks = "Hall-3" });
            memberlist.Add(new EditMemberList() { RollNo = 13049, Name = "Ranganathan Mutthunathan", Remarks = "Hall-5" });
            memberlist.Add(new EditMemberList() { RollNo = 13050, Name = "Terminator Adams", Remarks = "Hall-10" });
            editmember.ItemsSource = memberlist;
        }

    }
    public class EditMemberList
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
    }
}
