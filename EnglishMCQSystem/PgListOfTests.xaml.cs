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
            LoadData();
            dgTestQuestions.Visibility = Visibility.Hidden;
        }

        public void LoadData()
        {
            var data = context.Tests
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.DifficultyLevel,
                    t.NumOfQuestions,
                    t.IsActive
                }).ToList();
            dgTests.ItemsSource = null;
            dgTests.ItemsSource = data;
            ClearForm();
            
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtDifficultyLevel.Text = "";
            txtNumOfQuestions.Text = "";
            dgTestQuestions.ItemsSource = null;
            dgTestQuestions.Visibility = Visibility.Hidden;
        }

        private void dgTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var selectedTest = (dynamic)dgTests.SelectedItem;
            if (selectedTest == null)
            {
                return;
            }
            LoadQuestion(selectedTest);
        }

        private void LoadQuestion(dynamic selectedTest)
        {
            int id = selectedTest.Id;
            txtName.Text = selectedTest.Name.ToString();
            txtDifficultyLevel.Text = "Difficulty Level: " + selectedTest.DifficultyLevel.ToString();
            txtNumOfQuestions.Text = "(" + selectedTest.NumOfQuestions.ToString() + " questions)";
            chkIsActive.IsChecked = selectedTest.IsActive;
            chkIsActive.Visibility = Visibility.Visible;
            var questions = context.Tests
                .Where(t => t.Id == id)
                .SelectMany(t => t.Questions)
                .Select(q => new
                {
                    q.Id,
                    q.Text,
                    q.CorrectAnswer,
                    q.IsActive
                }).ToList();
            dgTestQuestions.Visibility = Visibility.Visible;
            dgTestQuestions.ItemsSource = null;
            dgTestQuestions.ItemsSource = questions;
        }

        private void chkIsActive_Click(object sender, RoutedEventArgs e)
        {
            var selectedTest = (dynamic)dgTests.SelectedItem;
            if (selectedTest == null)
            {
                return;
            }
            int id = selectedTest.Id;
            var test = context.Tests.Find(id);
            if (test == null)
            {
                return;
            }
            if(MessageBox.Show("Are you sure you want to change the active status of this test?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                chkIsActive.IsChecked = test.IsActive;
                return;
            }
            test.IsActive = chkIsActive.IsChecked.Value;
            context.Update(test);
            context.SaveChanges();
            LoadData();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedTest = (dynamic)dgTests.SelectedItem;
            if (selectedTest == null)
            {
                return;
            }
            int id = selectedTest.Id;
            var test = context.Tests.Find(id);
            test.Name = txtName.Text;
            test.IsActive = chkIsActive.IsChecked.Value;
            test.DifficultyLevel = txtDifficultyLevel.Text;
            context.Update(test);
            context.SaveChanges();
            LoadData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTest addTest = new AddTest();
            addTest.ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearchName.Text;
            var data = context.Tests
                .Where(t => t.Name.Contains(search))
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.DifficultyLevel,
                    t.NumOfQuestions,
                    t.IsActive
                }).ToList();
            dgTests.ItemsSource = null;
            dgTests.ItemsSource = data;
            ClearForm();
        }
    }
}
