using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoalTracker.ViewModels
{
    public class UserGoalsViewModel
    {
        public string Username { get; set; }

        public List<Goal> Goals { get; set; }
    }
}
