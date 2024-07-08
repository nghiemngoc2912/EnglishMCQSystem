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
    /// Interaction logic for PgListOfQuestions.xaml
    /// </summary>
    public partial class PgListOfQuestions : Page
    {
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        public PgListOfQuestions()
        {
            InitializeComponent();
            // Save the current scroll position
            double verticalOffset = scrollViewer.VerticalOffset;
            double horizontalOffset = scrollViewer.HorizontalOffset;
            //display all Questions
            
            LoadData();
            // Restore the scroll position
            scrollViewer.ScrollToVerticalOffset(verticalOffset);
            scrollViewer.ScrollToHorizontalOffset(horizontalOffset);
        }

        private void LoadData()
        {
            var questions = context.Questions
                .Select(q => new
                {
                    q.Id,
                    q.Text,
                    q.CorrectAnswer,
                    q.IsActive
                }).ToList();
            dgQuestions.ItemsSource = null;
            dgQuestions.ItemsSource = questions;
            ClearForm();
        }

        private void ClearForm()
        {
            txtID.Text = "";
            txtText.Text = "";
            cboCorrectAnswer.Text = "";
            chkActive.IsChecked = false;
        }

        private void dgQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //display selected question
            var selectedQuestion = (dynamic)dgQuestions.SelectedItem;
            if(selectedQuestion== null)
            {
                return;
            }
            txtID.Text = selectedQuestion.Id.ToString();
            txtText.Text = selectedQuestion.Text;
            cboCorrectAnswer.Text = selectedQuestion.CorrectAnswer;
            chkActive.IsChecked = selectedQuestion.IsActive;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            //check input update
            if (txtText.Text == "")
            {
                MessageBox.Show("Please enter the question text");
                return;
            }
            if (cboCorrectAnswer.Text == "")
            {
                MessageBox.Show("Please select the correct answer");
                return;
            }
            //update the selected question
            Question question = new Question();
            question.Text = txtText.Text;
            question.CorrectAnswer = cboCorrectAnswer.Text;
            context.Add(question);
            context.SaveChanges();
            MessageBox.Show("Question added successfully");
            LoadData();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            String id= txtID.Text;
            if (id == "")
            {
                MessageBox.Show("Please select a question to update");
                return;
            }
            //check input update
            if (txtText.Text == "")
            {
                MessageBox.Show("Please enter the question text");
                return;
            }
            if (cboCorrectAnswer.Text == "")
            {
                MessageBox.Show("Please select the correct answer");
                return;
            }
            //update the selected question
            Question question = context.Questions.Find(int.Parse(id));
            question.Text = txtText.Text;
            question.CorrectAnswer = cboCorrectAnswer.Text;
            question.IsActive = chkActive.IsChecked.Value;
            context.Update(question);
            context.SaveChanges();
            MessageBox.Show("Question updated successfully");
            LoadData();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var questions = context.Questions
                .Where(q => q.Text.Contains(txtSearchName.Text))
                .Select(q => new
                {
                    q.Id,
                    q.Text,
                    q.CorrectAnswer
                }).ToList();
            dgQuestions.ItemsSource = null;
            dgQuestions.ItemsSource = questions;
            ClearForm();
        }

        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuestion = (dynamic)dgQuestions.SelectedItem;
            if (selectedQuestion == null)
            {
                return;
            }
            Question question = context.Questions.Find(selectedQuestion.Id);
            if (question == null)
            {
                return;
            }
            if(MessageBox.Show("Are you sure you want to change the status of this question?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                chkActive.IsChecked = question.IsActive;
                return;
            }
            question.IsActive = chkActive.IsChecked.Value;
            context.Update(question);
            context.SaveChanges();
            LoadData();
        }
    }
}
