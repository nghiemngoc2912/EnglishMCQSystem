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
    /// Interaction logic for PgListOfUsers.xaml
    /// </summary>
    public partial class PgListOfUsers : Page
    {
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        public PgListOfUsers()
        {
            InitializeComponent();
            //display all Users
            var users = context.Users
                .Select(u => new
                {
                    ID=u.Id,
                    Name=u.Name,
                    Email=u.Email,
                    Username=u.Username,
                    Role=u.Role.Name
                })
                .ToList();
            dgUsers.ItemsSource = users;

        }
    }
}
