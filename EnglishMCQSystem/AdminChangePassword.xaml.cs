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
    /// Interaction logic for AdminChangePassword.xaml
    /// </summary>
    public partial class AdminChangePassword : Window
    {
        public AdminChangePassword()
        {
            InitializeComponent();
        }
        User userChangePassword = new User();
        public AdminChangePassword(User user)
        {
            InitializeComponent();
            txtUsername.Text = user.Username;
            userChangePassword = user;
        }

        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (newPassword.Password.Equals("") || confirmPassword.Password.Equals(""))
            {
                MessageBox.Show("Please fill all the fields");
            }
            else if (newPassword.Password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters");
            }
            else if (newPassword.Password != confirmPassword.Password)
            {
                MessageBox.Show("New password and confirm password must be the same");
            }
            else
            {
                userChangePassword.Password = newPassword.Password;
                context.Users.Update(userChangePassword);
                context.SaveChanges();
                MessageBox.Show("Password changed successfully");
                this.Close();
            }
        }
    }
}
