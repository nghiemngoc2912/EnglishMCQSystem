using EnglishMCQSystem.Models;
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

namespace EnglishMCQSystem
{
    /// <summary>
    /// Interaction logic for PgHome.xaml
    /// </summary>
    public partial class PgHome : Page
    {
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        public PgHome()
        {
            InitializeComponent();
            var data = context.Tests
                .Where(t => t.IsActive)
                .Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    Level = t.DifficultyLevel,
                    NumberOfQuestions = t.NumOfQuestions
                })
                .ToList();
            dgTests.ItemsSource = null;
            dgTests.ItemsSource = data;
        }

        private void dgTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedTest = (dynamic)dgTests.SelectedItem;
                if (selectedTest != null)
                {
                    int id = selectedTest.Id;
                    var test = context.Tests
                        .Where(t => t.Id == id)
                        .FirstOrDefault();
                    if (test != null)
                    {
                        var testWindow = new TestWindow(test);
                        Window parentWindow = Window.GetWindow(this);
                        if (parentWindow != null)
                        {
                            parentWindow.Close();
                        }
                        testWindow.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
