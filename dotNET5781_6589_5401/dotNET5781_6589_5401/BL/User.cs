using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class User
    {
        #region
        private string username; public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string password; public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private bool isManager; public bool IsManager
        {
            get { return isManager; }
            set { isManager = value; }
        }

        #endregion

        public User(string u, string p, bool i)
        {
            Username = u;
            Password = p;
            IsManager = i;
        }
    }
}
