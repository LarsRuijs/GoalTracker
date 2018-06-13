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

        /// <summary>
        /// Gets a filtered list of users.
        /// </summary>
        /// <param name="filter">Applies to username and email.</param>
        /// <returns></returns>
        public List<User> GetAllByFilter(string filter) => repo.GetAllByFilter(filter);
        
        /// <summary>
        /// Gets a single user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetSingle(int userId) => repo.GetSingle(userId);

        /// <summary>
        /// Edits a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Edit(User user) => repo.Edit(user);

        /// <summary>
        /// Registers and checks a newly submitted user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a user based on username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string username, string password)
        {
            if (!CheckLoginInformation(username, password))
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

        /// <summary>
        /// Checks if the login input is applyable.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool CheckLoginInformation(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the registry input is applyable.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private string CheckRegisterInformation(string username, string password, string email)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(email))
                return "Not all required fields are filled in.";

            if (!CheckEmailAdress(email))
                return "The inserted email adress is invalid.";

            return "";
        }

        /// <summary>
        /// Checks if the inserted email adress has the actual syntax of an email adress.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
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
