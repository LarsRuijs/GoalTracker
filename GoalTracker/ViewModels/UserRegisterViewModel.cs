using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoalTracker.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The two passwords do not match.")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

        public UserRegisterViewModel() { }
    }
}
