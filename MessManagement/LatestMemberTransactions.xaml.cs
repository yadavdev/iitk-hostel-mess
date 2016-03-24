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

namespace MessManagement
{
    /// <summary>
    /// Interaction logic for LatestMemberTransactions.xaml
    /// </summary>
    public partial class LatestMemberTransactions : UserControl
    {
        public LatestMemberTransactions()
        {
            InitializeComponent();
        }
        private void button_remove_trans_Click(object sender, RoutedEventArgs e)
        {
            Console.Write("\n\nTransaction Removed.\n\n");
        }
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MemberEntry());
        }
    }
}