using EnglishMCQSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UserTestAnswersWindow.xaml
    /// </summary>
    public partial class UserTestAnswersWindow : Window
    {
        public UserTestAnswersWindow()
        {
            InitializeComponent();
        }
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        public ObservableCollection<dynamic> Questions { get; set; }
        UserTest currentUserTest;
        public UserTestAnswersWindow(UserTest userTest)
        {
            InitializeComponent();
            currentUserTest = userTest;
            txtName.Text = userTest.Test.Name;
            var userTestAnswers = context.UserTestAnswers
                .Where(uta => uta.UserTestId == userTest.Id)
                .Select(uta => new
                {
                    Question = uta.Question.Text,
                    UserAnswer = uta.UserAnswer,
                    IsCorrect = uta.UserAnswer == uta.Question.CorrectAnswer
                })
                .ToList();
            dgUserTestAnswer.ItemsSource = null;
            dgUserTestAnswer.ItemsSource = userTestAnswers;
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            
            TestWindow testWindow = new TestWindow(currentUserTest.Test);
            testWindow.Show();
            //close other windows
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() != typeof(TestWindow))
                {
                    window.Close();
                }
            }
        }
    }
}
