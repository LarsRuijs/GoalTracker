using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoalTracker.ViewModels
{
    public class SingleDiscussionViewModel
    {
        public Discussion Discussion { get; set; }

        [Required]
        public int DiscussionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Entry { get; set; }

        public SingleDiscussionViewModel() { }
    }
}
