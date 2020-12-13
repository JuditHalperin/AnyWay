using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temp
{
    static public class Users
    {
        static ObservableCollection<User> users = new ObservableCollection<User>();

        static Users()
        {
            users.Add(new User("Asnat", "123456", false));
            users.Add(new User("Judit", "654321", true));
        }

        static public bool containUser(string username)
        {
            foreach (User item in users)
                if (item.Username == username)
                    return true;
            return false;
        }

        static public bool signIn(string username, string password, bool administrativePrivileges)
        {
            foreach (User item in users)
                if (item.Username == username && item.Password == password && item.AdministrativePrivileges == administrativePrivileges)
                    return true;
            return false;
        }

        static public void newAccount(string username, string password, bool administrativePrivileges)
        {
            if (!containUser(username))
                users.Add(new User(username, password, administrativePrivileges));
        }
    }
}
