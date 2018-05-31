using Data.Contexts;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class UserRepository
    {
        private IUserContext context;

        public UserRepository(StorageType st)
        {
            switch (st)
            {
                case StorageType.Memory: context = new UserMemory(); break;
                case StorageType.Database: context = new UserSql(); break;
            }
        }

        public string CheckIfUserExists(string email, string username) => context.CheckIfUserExists(email, username);

        public User GetUserByLogin(string username, string password) => context.GetUserByLogin(username, password);

        public bool Register(User user) => context.Register(user);

        public List<User> GetUsers(string filter) => context.GetUsers(filter);

        public User GetUser(int userId) => context.GetUser(userId);

        public bool EditUser(User user) => context.EditUser(user);
    }
}
