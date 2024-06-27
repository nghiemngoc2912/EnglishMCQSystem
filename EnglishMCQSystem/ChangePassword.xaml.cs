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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
            var user = SessionManager.Instance.CurrentUser;
            txtUsername.Text = user.Username;
        }
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if(oldPassword.Password.Equals("") || newPassword.Password.Equals("") || confirmPassword.Password.Equals(""))
            {
                MessageBox.Show("Please fill all the fields");
            }
            else if (newPassword.Password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters");
            }
            else if(newPassword.Password != confirmPassword.Password)
            {
                MessageBox.Show("New password and confirm password must be the same");
            }
            else
            {
                var user = SessionManager.Instance.CurrentUser;
                if(user.Password != oldPassword.Password)
                {
                    MessageBox.Show("Old password is incorrect");
                }
                else
                {
                    if (oldPassword.Password.Equals(newPassword.Password))
                    {
                        MessageBox.Show("New password must be different from old password");
                    }
                    else {
                        user.Password = newPassword.Password;
                        context.Users.Update(user);
                        context.SaveChanges();
                        MessageBox.Show("Password changed successfully");
                        this.Close();
                    }
                    
                }
            }
        }
    }
}
