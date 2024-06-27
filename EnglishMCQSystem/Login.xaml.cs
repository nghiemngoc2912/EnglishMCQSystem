using EnglishMCQSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        EnglishMcqsystemContext ems = new EnglishMcqsystemContext();

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var user = ems.Users.Where(u => u.Username == txtUsername.Text && u.Password == txtPassword.Password).FirstOrDefault();
            

            if (user != null)
            {
                SessionManager.Instance.CurrentUser = user;
                //save into session
                if(user.RoleId == 1)
                {
                    AdminDashboard admin = new AdminDashboard();
                    admin.Show();
                    this.Close();
                }
                else
                {
                    Home home = new Home();
                    home.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Login Fail");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
