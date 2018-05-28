using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoalTracker.ViewModels
{
    public class FilterUsersViewModel
    {
        public List<User> Users { get; set; }

        public string FilterString { get; set; }

        public FilterUsersViewModel()
        {
            Users = new List<User>();
        }
    }
}
