using EnglishMCQSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishMCQSystem
{
    public class SessionManager
    {
        public static SessionManager instance;
        private User currentUser;
        public static SessionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionManager();
                }
                return instance;
            }
        }
        public User CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }
        public bool IsUserLoggedIn()
        {
            return currentUser != null;
        }
        public void Logout()
        {
            currentUser = null;
        }
    }
}
