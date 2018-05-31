using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Data.Contexts
{
    class UserMemory : IUserContext
    {
        List<User> users = new List<User>();

        public UserMemory()
        {
            users.Add(new User() { UserId = 1, Email = "user@gmail.com", Username = "user", Password = "test", Role = UserRole.User });
            users.Add(new User() { UserId = 7, Email = "admin@outlook.com", Username = "admin", Password = "1234", Role = UserRole.Admin });
        }

        public string CheckIfUserExists(string email, string username)
        {
            var user = new User();

            user = users.FirstOrDefault(u => u.Username == username);

            if (String.IsNullOrEmpty(username))
                return "This username is already in use.";

            user = users.FirstOrDefault(u => u.Email == email);

            if (String.IsNullOrEmpty(email))
                return "This email address is already in use.";

            return "";
        }

        public bool EditUser(User user)
        {
            try
            {
                users.RemoveAll(u => u.UserId == user.UserId);
            }
            catch
            {
                return false;
            }

            users.Add(user);

            return true;
        }

        public User GetUser(int userId) => users.FirstOrDefault(u => u.UserId == userId);

        public User GetUserByLogin(string username, string password)
            => users.FirstOrDefault(u => (u.Username == username || u.Email == username) && u.Password == password);

        public List<User> GetUsers(string filter)
        {
            var usersToReturn = new List<User>();

            foreach (var user in users.Where(u => u.Email.Contains(filter) || u.Username.Contains(filter)))
            {
                usersToReturn.Add(user);
            }

            return usersToReturn;
        }

        Random rnd = new Random();

        public bool Register(User user)
        {
            users.Add(user);

            var unique = false;

            while (!unique)
            {
                user.UserId = rnd.Next(1, 100);

                if (users.Exists(u => u.UserId == user.UserId))
                {
                    unique = true;
                }
            }

            return true;
        }
    }
}
