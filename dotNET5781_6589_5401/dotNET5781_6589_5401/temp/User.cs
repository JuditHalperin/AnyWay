using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temp
{
    class User
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
        private bool administrativePrivileges; public bool AdministrativePrivileges
        {
            get { return administrativePrivileges; }
            set { administrativePrivileges = value; }
        }

        #endregion

        public User(string u, string p, bool a)
        {
            Username = u;
            Password = p;
            AdministrativePrivileges = a;
        }
    }
}
