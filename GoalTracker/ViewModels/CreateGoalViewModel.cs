using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoalTracker.ViewModels
{
    public class CreateGoalViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Info { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDT { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDT { get; set; }
    }
}
