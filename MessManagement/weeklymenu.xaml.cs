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
    /// Interaction logic for weeklymenu.xaml
    /// </summary>
    public partial class WeeklyMenu : UserControl
    {
        int MaxExtras = 4;
        Dictionary<String, Dictionary<String, List<DayMenu>>> weeklymenu = new Dictionary<String, Dictionary<String, List<DayMenu>>>();

        public WeeklyMenu()
        {
            InitializeComponent();
            LoadMenus();

        }

        private void button_add00_Click(object sender, RoutedEventArgs e)
        {
            Menu00.CommitEdit();
            if (weeklymenu["0"]["0"].Count < MaxExtras)
            {
                weeklymenu["0"]["0"].Add(new DayMenu() { Name = "New Item", Price = 0 });
                Menu00.Items.Refresh();

            }
            else
                Menu00.Items.Refresh();
        }

        private void button_add01_Click(object sender, RoutedEventArgs e)
        {
            Menu01.CommitEdit();
            if (weeklymenu["0"]["1"].Count < MaxExtras)
            {
                weeklymenu["0"]["1"].Add(new DayMenu() { Name = "New Item", Price = 0 });
                Menu01.Items.Refresh();

            }
            else
                Menu01.Items.Refresh();
        }

        private void button_add02_Click(object sender, RoutedEventArgs e)
        {
            Menu02.CommitEdit();
            if (weeklymenu["0"]["2"].Count < MaxExtras)
            {
                weeklymenu["0"]["2"].Add(new DayMenu() { Name = "New Item", Price = 0 });
                Menu02.Items.Refresh();

            }
            else
                Menu02.Items.Refresh();
        }
        private void button_remove00_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu00, "0", "0");
        }
        private void button_remove01_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu01, "0", "1");
        }
        private void button_remove02_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu02, "0", "2");
        }

        private void RemoveExtra(ref DataGrid dg, string day, string meal)
        {
            dg.CommitEdit();
            if (dg.SelectedItems.Count > 0)
            {
                try
                {
                    var selectedIndex = dg.SelectedIndex;
                    weeklymenu[day][meal].RemoveAt(selectedIndex);
                }
                catch (Exception exception)
                {
                    Console.Write("\n\n" + exception.ToString() + "\n\n");
                }
                finally
                {
                    dg.Items.Refresh();
                }
            }
        }

        private void LoadMenus()
        {
            weeklymenu.Add("0", new Dictionary<String, List<DayMenu>>());
            weeklymenu["0"].Add("0", new List<DayMenu>());
            weeklymenu["0"].Add("1", new List<DayMenu>());
            weeklymenu["0"].Add("2", new List<DayMenu>());

            weeklymenu["0"]["0"].Add(new DayMenu() { Name = "Rasgulla", Price = 14 });
            weeklymenu["0"]["1"].Add(new DayMenu() { Name = "GulabJamun", Price = 20 });
            weeklymenu["0"]["2"].Add(new DayMenu() { Name = "ChikenCurry", Price = 25 });
            Menu00.ItemsSource = weeklymenu["0"]["0"];
            Menu01.ItemsSource = weeklymenu["0"]["1"];
            Menu02.ItemsSource = weeklymenu["0"]["2"];
        }


    }

    public class DayMenu
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
    }
}