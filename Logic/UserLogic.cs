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
        UserRepository repo = new UserRepository(StorageType.Database);

        public List<User> GetAllByFilter(string filter) => repo.GetAllByFilter(filter);

        public User GetSingle(int userId) => repo.GetSingle(userId);

        public bool Edit(User user) => repo.Edit(user);

        public string Register(string username, string password, string email)
        {
            string eMessage = "";

            eMessage = CheckRegisterInformation(username, password, email);

            // Checkt of dat een error is ontstaan. 
            if (eMessage != "")
                return eMessage;

            // Checkt of al een user bestaat met de ingevoerde email adres of username.
            eMessage = repo.CheckIfUserExists(email, username);

            // Checkt of dat een error is ontstaan. 
            if (eMessage != "")
                return eMessage;

            var user = new User()
            {
                Username = username,
                Password = password,
                Email = email
            };

            // Registreert de user
            bool response = repo.Register(user);

            if (!response)
                return "Registry failed.";
            else
                return "";
        }

        public User Login(string username, string password)
        {
            if (CheckLoginInformation(username, password))
            {
                return new User();
            }

            var input = new User()
            {
                Username = username,
                Password = password
            };

            User user = repo.GetUserByLogin(input);

            return user;
        }

        private bool CheckLoginInformation(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                return false;

            return true;
        }

        private string CheckRegisterInformation(string username, string password, string email)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(email))
                return "Not all required fields are filled in.";

            if (!CheckEmailAdress(email))
                return "The inserted email adress is invalid.";

            return "";
        }

        private bool CheckEmailAdress(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
