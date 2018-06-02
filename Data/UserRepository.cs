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

        public User GetUserByLogin(User input) => context.GetUserByLogin(input);

        public bool Register(User user) => context.Register(user);

        public List<User> GetAllByFilter(string filter) => context.GetAllByFilter(filter);

        public User GetSingle(int userId) => context.GetSingle(userId);

        public bool Edit(User user) => context.Edit(user);
    }
}
