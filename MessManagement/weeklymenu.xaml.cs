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
        /*Event Handers for Monday*/
        private void button_add00_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu00, "0", "0");
        }
        private void button_add01_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu01, "0", "1");
        }
        private void button_add02_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu02, "0", "2");
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
        /*Event Handers for Tuesday*/
        private void button_add10_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu10, "1", "0");
        }
        private void button_add11_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu11, "1", "1");
        }
        private void button_add12_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu12, "1", "2");
        }
        private void button_remove10_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu10, "1", "0");
        }
        private void button_remove11_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu11, "1", "1");
        }
        private void button_remove12_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu12, "1", "2");
        }
        /*Event Handers for Wednesday*/
        private void button_add20_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu20, "2", "0");
        }
        private void button_add21_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu21, "2", "1");
        }
        private void button_add22_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu22, "2", "2");
        }
        private void button_remove20_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu20, "2", "0");
        }
        private void button_remove21_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu21, "2", "1");
        }
        private void button_remove22_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu22, "2", "2");
        }
        /*Event Handers for Thursday*/
        private void button_add30_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu30, "3", "0");
        }
        private void button_add31_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu31, "3", "1");
        }
        private void button_add32_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu32, "3", "2");
        }
        private void button_remove30_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu30, "3", "0");
        }
        private void button_remove31_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu31, "3", "1");
        }
        private void button_remove32_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu32, "3", "2");
        }
        /*Event Handers for Friday*/
        private void button_add40_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu40, "4", "0");
        }
        private void button_add41_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu41, "4", "1");
        }
        private void button_add42_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu42, "4", "2");
        }
        private void button_remove40_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu40, "4", "0");
        }
        private void button_remove41_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu41, "4", "1");
        }
        private void button_remove42_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu42, "4", "2");
        }
        /*Event Handers for Saturday*/
        private void button_add50_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu50, "5", "0");
        }
        private void button_add51_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu51, "5", "1");
        }
        private void button_add52_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu52, "5", "2");
        }
        private void button_remove50_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu50, "5", "0");
        }
        private void button_remove51_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu51, "5", "1");
        }
        private void button_remove52_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu52, "5", "2");
        }
        /*Event Handers for Sunday*/
        private void button_add60_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu60, "6", "0");
        }
        private void button_add61_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu61, "6", "1");
        }
        private void button_add62_Click(object sender, RoutedEventArgs e)
        {
            AddExtra(ref Menu62, "6", "2");
        }
        private void button_remove60_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu60, "6", "0");
        }
        private void button_remove61_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu61, "6", "1");
        }
        private void button_remove62_Click(object sender, RoutedEventArgs e)
        {
            RemoveExtra(ref Menu62, "6", "2");
        }

        /*Add an extra row to the list of the given day and meal*/
        private void AddExtra(ref DataGrid dg, string day, string meal)
        {
            dg.CommitEdit();
            if (weeklymenu[day][meal].Count < MaxExtras)
            {
                weeklymenu[day][meal].Add(new DayMenu() { Name = "New Item", Price = 0 });

            }
            dg.Items.Refresh();
        }

        /*Remove an extra row from the list of the given day and meal*/
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