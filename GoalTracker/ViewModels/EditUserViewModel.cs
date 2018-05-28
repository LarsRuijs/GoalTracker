using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoalTracker.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public DateTime? Banning { get; set; }

        public EditUserViewModel() { }
    }
}
