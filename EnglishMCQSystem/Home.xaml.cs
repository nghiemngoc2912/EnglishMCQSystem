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
using System.Windows.Shapes;

namespace EnglishMCQSystem
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            ContentFrame.Navigate(new PgHome());
        }
        private void Navigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox.SelectedItem is ListBoxItem selectedItem)
            {
                switch (selectedItem.Content)
                {
                    case "Home":
                        ContentFrame.Navigate(new PgHome());
                        break;
                    case "Profile":
                        ContentFrame.Navigate(new PgProfile());
                        break;
                    case "History":
                        ContentFrame.Navigate(new PgHistory());
                        break;
                }
            }
        }
    }
}
