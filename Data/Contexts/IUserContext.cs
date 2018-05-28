using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contexts
{
    internal interface IUserContext
    {
        string CheckIfUserExists(string email, string username);
        string CheckByEmail(string email);
        string CheckByUsername(string username);
        User GetUserByLogin(string username, string password);
        bool Register(User user);
        List<User> GetUsers(string filter);
        User GetUser(int userId);
        bool EditUser(User user);
    }
}
