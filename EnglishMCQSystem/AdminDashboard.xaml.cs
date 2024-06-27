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
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EnglishMCQSystem
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {
            InitializeComponent();

        }
        private void Navigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox.SelectedItem is ListBoxItem selectedItem)
            {
                switch (selectedItem.Content)
                {
                    case "Dashboard":
                        ContentFrame.Navigate(new PgDashboard());
                        break;
                    case "List of Users":
                        ContentFrame.Navigate(new PgListOfUsers());
                        break;
                    case "List of Questions":
                        ContentFrame.Navigate(new PgListOfQuestions());
                        break;
                    case "List of Tests":
                        ContentFrame.Navigate(new PgListOfTests());
                        break;
                }
            }
        }
    }
}
