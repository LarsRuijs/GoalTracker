using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contexts
{
    internal interface IUserContext
    {
        string CheckIfUserExists(string email, string username);
        User GetUserByLogin(User input);
        bool Register(User user);
        List<User> GetAllByFilter(string filter);
        User GetSingle(int userId);
        bool Edit(User user);
    }
}
