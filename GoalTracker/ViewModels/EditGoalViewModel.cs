using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoalTracker.ViewModels
{
    public class EditGoalViewModel
    {
        [Required]
        public int GoalId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Info { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartDT { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDT { get; set; }
        [Required]
        public int Progress { get; set; }
        [Required]
        public GoalStatus Status { get; set; }
        [Required]
        public int Strikes { get; set; }

        public EditGoalViewModel() { }
    }
}
