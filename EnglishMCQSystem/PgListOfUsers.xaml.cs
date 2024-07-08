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
            LoadData();
        }

        private void LoadData()
        {
            var users = context.Users
                .Select(u => new
                {
                    ID = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Username = u.Username,
                    Role = u.Role.Name,
                    IsActive = u.IsActive
                })
                .ToList();
            dgUsers.ItemsSource = users;
            ClearForm();
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtRole.Text = "";
            chkActive.IsChecked = false;
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //display user info on form
            if (dgUsers.SelectedItem != null)
            {
                dynamic user = dgUsers.SelectedItem;
                txtName.Text = user.Name;
                txtEmail.Text = user.Email;
                txtUsername.Text = user.Username;
                txtRole.Text = user.Role;
                chkActive.IsChecked = user.IsActive;
            }
        }

        private void chkActive_Click(object sender, RoutedEventArgs e)
        {

            //update user active status
            String username = txtUsername.Text;
            User user = context.Users.Where(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                if(MessageBox.Show("Are you sure you want to change the active status of this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    chkActive.IsChecked = user.IsActive;
                    return;
                }
                user.IsActive = chkActive.IsChecked.Value;
                context.Update(user);
                context.SaveChanges();
                LoadData();
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            String username = txtUsername.Text;
            User user = context.Users.Where(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                if(user.RoleId == 1)
                {
                    MessageBox.Show("Admin password can not be changed.");
                    return;
                }
                AdminChangePassword adminChangePassword = new AdminChangePassword(user);
                adminChangePassword.ShowDialog();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            String search = txtSearchName.Text;
            var users = context.Users
                .Where(u => u.Name.Contains(search)||u.Username.Contains(search)||u.Email.Contains(search))
                .Select(u => new
                {
                    ID = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Username = u.Username,
                    Role = u.Role.Name,
                    IsActive = u.IsActive
                })
                .ToList();
            dgUsers.ItemsSource = users;
            ClearForm();
        }
    }
}
