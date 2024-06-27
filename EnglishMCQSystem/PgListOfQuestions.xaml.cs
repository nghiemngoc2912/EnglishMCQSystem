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
            //display all Questions
            var questions = context.Questions.ToList();
            dgQuestions.ItemsSource = null;
            dgQuestions.ItemsSource = questions;
        }

        private void dgQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //display selected question
            var selectedQuestion = (Question)dgQuestions.SelectedItem;
            if(selectedQuestion== null)
            {
                return;
            }
            txtID.Text = selectedQuestion.Id.ToString();
            txtText.Text = selectedQuestion.Text;
            cboCorrectAnswer.Text = selectedQuestion.CorrectAnswer;
        }

    }
}
