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
using System.Windows.Shapes;

namespace EnglishMCQSystem
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private Test currentTest;
        private List<Question> questions;

        public TestWindow()
        {
            InitializeComponent();
        }
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        UserTest currentUserTest;
        public TestWindow(Test test)
        {
            InitializeComponent();
            currentTest = test;
            txtName.Text = test.Name;
            var questions = context.Tests
                            .Where(t => t.Id == test.Id)
                            .SelectMany(t => t.Questions)
                            .ToList();
            lboxQuestions.ItemsSource = questions;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var userTest = new UserTest
            {
                UserId = SessionManager.Instance.CurrentUser.Id,
                TestId= currentTest.Id,
                TestDate = DateTime.Now
            };
            context.UserTests.Add(userTest);
            context.SaveChanges();
            var data = lboxQuestions.ItemsSource as List<Question>;
            double score = 0;
            foreach (var question in data)
            {
                // Tìm RadioButton được chọn trong question do
                var selectedRadioButton = FindCheckedRadioButton(question.Id.ToString());
                var userAnswer = selectedRadioButton?.Content.ToString();
                var userTestAnswer = new UserTestAnswer
                {
                    UserTestId = userTest.Id,
                    QuestionId = question.Id,
                    UserAnswer = userAnswer // Lưu giá trị của RadioButton được chọn
                };
                if (question.CorrectAnswer == userAnswer)
                {
                    score += 10.0/(double)currentTest.NumOfQuestions;
                }
                // Lưu thay đổi vào cơ sở dữ liệu
                context.UserTestAnswers.Add(userTestAnswer);
            }
            // Lưu điểm số vào cơ sở dữ liệu
            userTest.Score = score;
            context.SaveChanges();
            MessageBox.Show("Your score is: " + score);
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private RadioButton FindCheckedRadioButton(string groupName)
        {
            return FindVisualChildren<RadioButton>(this)
                .FirstOrDefault(rb => rb.GroupName == groupName && rb.IsChecked == true);
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
