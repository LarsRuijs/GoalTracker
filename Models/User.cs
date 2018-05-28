using System;
using System.Collections.Generic;

namespace Models
{
    public class User
    {       
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime? Banning { get; set; }
        public List<Goal> Goals { get; set; }

        public User(int userId, string email, string username, string password, UserRole role, DateTime? banning)
        {
            UserId = userId;
            Email = email;
            Username = username;
            Password = password;
            Role = role;
            Banning = banning;
        }

        public User(int userId, string email, string username, UserRole role, DateTime? banning)
        {
            UserId = userId;
            Email = email;
            Username = username;
            Role = role;
            Banning = banning;
        }

        public User() { }
    }
}
