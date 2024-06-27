using EnglishMCQSystem.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for PgHistory.xaml
    /// </summary>
    public partial class PgHistory : Page
    {
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        public PgHistory()
        {
            InitializeComponent();
            SessionManager session = SessionManager.Instance;
            var user = session.CurrentUser;
            var data=context.UserTests
                .Where(ut => ut.UserId == user.Id)
                .Select(ut => new
                {
                    Id = ut.Id,
                    TestName = ut.Test.Name,
                    Score = ut.Score,
                    Date=ut.TestDate
                })
                .ToList();
            dgUserTests.ItemsSource= null;
            dgUserTests.ItemsSource = data;
        }

        private void dgUserTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTest = (dynamic)dgUserTests.SelectedItem;
            if (selectedTest != null)
            {
                int id = selectedTest.Id;
                var userTest = context.UserTests
                    .Include(ut=>ut.Test)
                    .FirstOrDefault(ut => ut.Id == id);
                if (userTest != null)
                {
                    UserTestAnswersWindow userTestAnswersWindow = new UserTestAnswersWindow(userTest);
                    userTestAnswersWindow.ShowDialog();
                }
            }
        }
    }
}
