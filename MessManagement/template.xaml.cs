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
    /// Interaction logic for template.xaml
    /// </summary>
    public partial class template : UserControl
    {
        public template()
        {
            InitializeComponent();
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
}
