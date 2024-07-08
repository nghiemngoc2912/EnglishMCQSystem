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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class PgProfile : Page
    {
        public PgProfile()
        {
            InitializeComponent();
            var user = SessionManager.Instance.CurrentUser;
            txtName.Text = user.Name;
            txtEmail.Text = user.Email;
            txtUsername.Text = user.Username;
        }

        
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            var user = SessionManager.Instance.CurrentUser;
            String name = txtName.Text;
            String email = txtEmail.Text;
            String emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            //check input valid
            if (name.Equals("") || email.Equals(""))
            {
                //input not empty
                MessageBox.Show("Please fill all the fields");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
            {
                //email not valid
                MessageBox.Show("Email not valid");
                return;
            }
            else if (context.Users.Where(u=>u.Email==email)!=null) {
                //check email exist
                MessageBox.Show("Email already exist");
                return;
            } else { 
                user.Name = name;
                user.Email = email;
                context.Users.Update(user);
                context.SaveChanges();
                MessageBox.Show("Update profile successfully!");
            }
            
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            changePassword.Show();
        }
    }
}
