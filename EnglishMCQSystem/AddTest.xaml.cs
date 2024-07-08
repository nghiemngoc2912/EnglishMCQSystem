using EnglishMCQSystem.Models;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
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

namespace EnglishMCQSystem
{
    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : Window
    {
        int numOfQuestions = 0;
        public AddTest()
        {
            InitializeComponent();
            LoadQuestion();
        }
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        private void LoadQuestion()
        {
            var questions = context.Questions
                .Where(q=>q.IsActive)
                .ToList();
            lstQuestionBank.ItemsSource = questions;
            numOfQuestions = questions.Count;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions = new List<Question>();
            string name = txtName.Text;
            string difficultyLevel = txtDifficultyLevel.Text;
            var questionbank = context.Questions.ToList();
            foreach (var question in questionbank)
            {

                var checkbox = FindCheckbox(question.Id.ToString());
                if (checkbox?.IsChecked == true)
                {
                    questions.Add(question);
                }

            }
            if (questions.Count == 0)
            {
                MessageBox.Show("Please select at least one question");
                return;
            }
            var test = new Test
            {
                Name = name,
                DifficultyLevel = difficultyLevel,
                NumOfQuestions = questions.Count,
                IsActive = true,
                Questions = questions
            };
            context.Tests.Add(test);
            context.SaveChanges();
            MessageBox.Show("Test created successfully");
            foreach (Window window in Application.Current.Windows)
            {
                var page = FindPage<PgListOfTests>(window);
                if (page != null)
                {
                    page.LoadData();
                    break; // Assuming you only need to find one instance and call LoadData
                }
            }
            this.Close();
        }

        private CheckBox FindCheckbox(string id)
        {
            var checkboxes = FindVisualChildren<CheckBox>(this);

            // Iterate through each checkbox and check its Tag value
            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Tag != null && checkbox.Tag.ToString() == id)
                {
                    return checkbox;
                }
            }

            // Return null if no checkbox with the specified Tag was found
            return null;
        }
        private T FindPage<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childOfChild = FindPage<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
