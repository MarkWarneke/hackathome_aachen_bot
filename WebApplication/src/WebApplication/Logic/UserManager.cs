using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Logic
{
    public class UserManager
    {

        User user = MockUser.Instance.User;

        public UserManager()
        {
            
        }

        public User findById(int id)
        {
            if (user.ID == id.ToString())
                return user;

            return null;
        }

        public User updateUser(User user)
        {
            MockUser.Instance.User = user;
            return user;
        }

        public User getUser()
        {
            return MockUser.Instance.User;
        }
    }
}
