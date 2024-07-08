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
    /// Interaction logic for PgListOfTests.xaml
    /// </summary>
    public partial class PgListOfTests : Page
    {
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        public PgListOfTests()
        {
            InitializeComponent();
            var data= context.Tests
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.DifficultyLevel,
                    t.NumOfQuestions
                }).ToList();
            dgTests.ItemsSource = null;
            dgTests.ItemsSource = data;
            dgTestQuestions.Visibility = Visibility.Hidden;
        }

        private void dgTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var selectedTest = (dynamic)dgTests.SelectedItem;
            if (selectedTest == null)
            {
                return;
            }
            double verticalOffset = scrollViewerTestQuestions.VerticalOffset;
            double horizontalOffset = scrollViewerTestQuestions.HorizontalOffset;
            int id = selectedTest.Id;
            txtName.Text = selectedTest.Name.ToString();
            txtDifficultyLevel.Text = "Difficulty Level: " + selectedTest.DifficultyLevel.ToString();
            txtNumOfQuestions.Text = "("+ selectedTest.NumOfQuestions.ToString()+" questions)";
            var questions = context.Tests
                .Where(t => t.Id == id)
                .SelectMany(t => t.Questions)
                .Select(q => new
                {
                    q.Id,
                    q.Text,
                    q.CorrectAnswer
                }).ToList();
            dgTestQuestions.Visibility = Visibility.Visible;
            dgTestQuestions.ItemsSource = null;
            dgTestQuestions.ItemsSource = questions;
            scrollViewerTestQuestions.ScrollToVerticalOffset(verticalOffset);
            scrollViewerTestQuestions.ScrollToHorizontalOffset(horizontalOffset);
        }
    }
}
