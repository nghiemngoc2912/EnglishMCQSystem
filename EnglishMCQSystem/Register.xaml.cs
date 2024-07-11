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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            String name=txtName.Text;
            String email=txtEmail.Text;
            String username=txtUsername.Text;
            String password=txtPassword.Password;
            String confirmPassword=txtConfirmPassword.Password;
            String emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";   
            //check input valid
            if (name.Equals("") || email.Equals("") || password.Equals("") || confirmPassword.Equals(""))
            {
                //input not empty
                MessageBox.Show("Please fill all the fields");
                return;
            }else if (!System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
            {
                //email not valid
                MessageBox.Show("Email not valid");
                return;
            }else if (password.Length < 8)
            {
                //password length less than 8
                MessageBox.Show("Password length must be at least 8 characters");
                return;
            }
            else if (!password.Equals(confirmPassword))
            {
                //password and confirm password do not match
                MessageBox.Show("Password and Confirm Password do not match");
                return;
            }
            //check in db if username or email already exists
            else if (context.Users.Any(u => u.Username == username))
            {
                //username already exists
                MessageBox.Show("Username already exists");
                return;
            }else if (context.Users.Any(u => u.Email == email))
            {
                //email already exists
                MessageBox.Show("Email already exists");
                return;
            }
            else
            {
                //create new user
                User user = new User();
                user.Name = name;
                user.Email = email.ToLower();
                user.Username = username;
                user.Password = password;
                user.RoleId = 2;
                user.IsActive = true;
                context.Users.Add(user);
                context.SaveChanges();
                MessageBox.Show("User registered successfully");
                Login login = new Login();
                login.Show();
                this.Close();
            }
              
        }
    }
}
