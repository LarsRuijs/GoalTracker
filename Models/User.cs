using System;
using System.Collections.Generic;
using System.Net.Mail;

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

        public User() { }
    }
}
