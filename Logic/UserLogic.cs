using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Data.Contexts;
using Models;

namespace Logic
{
    public class UserLogic
    {
        UserRepository repo = new UserRepository(StorageType.Memory);

        public List<User> GetUsers(string filter) => repo.GetUsers(filter);

        public User GetUser(int userId) => repo.GetUser(userId);

        public bool EditUser(User user) => repo.EditUser(user);

        public User Login(string username, string password)
        {
            if (username == "" || password == "")
                throw new Exception("Fill in both a Username and Password.");

            User user = repo.GetUserByLogin(username, password);
            
            return user;
        }

        public string Register(User user)
        {
            if (user.Username == "" || user.Password == "" || user.Email == "")
                return "Not all required input fields are filled in.";

            // Checkt of al een user bestaat met de ingevoerde email adres of username.
            string eMessage = repo.CheckIfUserExists(user.Email, user.Username);

            if (eMessage != "")
                return eMessage;

            // Registreert de user
            bool registered = repo.Register(user);

            if (!registered)
                return "Registry failed.";
            else
                return "";
        }
    }
}
