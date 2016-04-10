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
using MySql.Data.MySqlClient;
namespace MessManagement
{
    /// <summary>
    /// Interaction logic for MenuChoose.xaml
    /// </summary>
    public partial class MenuChoose : UserControl
    {
        List<DayChoose> todaylist = new List<DayChoose>();
        List<DayChoose> todaylist2 = new List<DayChoose>();
        string cs =
            "SERVER=localhost;" +
            "DATABASE=mess_db;" +
            "UID=root;" +
            "PASSWORD=gaurav;";
        MySqlConnection conn = null;
        public MenuChoose()
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
            todaylist.Add(new DayChoose() { dayname = "Monday" });
            todaylist.Add(new DayChoose() { dayname = "Tuesday" });
            todaylist.Add(new DayChoose() { dayname = "Wednesday" });
            todaylist.Add(new DayChoose() { dayname = "Thursday" });
            todaylist.Add(new DayChoose() { dayname = "Friday" });
            todaylist.Add(new DayChoose() { dayname = "Saturday" });
            todaylist.Add(new DayChoose() { dayname = "Sunday" });
            todaylist2.Add(new DayChoose() { dayname = "Breakfast" });
            todaylist2.Add(new DayChoose() { dayname = "Lunch" });
            todaylist2.Add(new DayChoose() { dayname = "Dinner" });
            comboboxlist_day.ItemsSource = todaylist;
            tempmenu.ItemsSource = MenuTemp.tempmenu;
            comboboxlist_day.DisplayMemberPath = "dayname";
            comboboxlist_day.SelectedValuePath = "dayname";
            comboboxlist_day.SelectedValue = "Monday";
            comboboxlist_meal.ItemsSource = todaylist2;
            comboboxlist_meal.DisplayMemberPath = "dayname";
            comboboxlist_meal.SelectedValuePath = "dayname";
            //comboboxlist_meal.SelectedValue = "Breakfast";
            //LoadMenuFromDB();

        }

        private void ComboBox_day_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboboxlist_meal.SelectedIndex = -1;
        }
        private void ComboBox_meal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadMenuFromDB();

        }
        private void button_continue_Click(object sender, RoutedEventArgs e)
        {
            tempmenu.CommitEdit();
            Switcher.Switch(new MemberEntry());
        }
        private void button_remove_Click(object sender, RoutedEventArgs e)
        {

        }
        private void button_back_Click(object sender, RoutedEventArgs e)
        {

        }
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            tempmenu.CommitEdit();
            MenuTemp.tempmenu.Add(new DayMenu() { Name = "New Item", Price = 0 });
            tempmenu.Items.Refresh();
        }
        private void LoadMenuFromDB()
        {
            tempmenu.CommitEdit();
            
            int currday = comboboxlist_day.SelectedIndex;
            int currmeal = comboboxlist_meal.SelectedIndex;
            int db_day = currday * 10 + currmeal;
            string str = "SELECT * from menu where  day =" + db_day.ToString();
            Console.WriteLine(str);
            try
            {
                MySqlDataReader dr = null;
                MySqlCommand cmd = new MySqlCommand(str, conn);
                dr = cmd.ExecuteReader();
                MenuTemp.tempmenu.Clear();
                while (dr.Read())
                {   
                    Console.WriteLine(dr.GetString(0));
                    MenuTemp.tempmenu.Add(new DayMenu() { Name = dr.GetString(0), Price = Convert.ToInt32(dr.GetDouble(1)) });
                }
                if (dr != null)
                    dr.Close();
            }
            catch
            {
                Console.WriteLine("Database query failed.");
            }
            tempmenu.Items.Refresh();
        }
    }
    public class DayChoose
    {
        public string dayname { get; set; }
    }

}