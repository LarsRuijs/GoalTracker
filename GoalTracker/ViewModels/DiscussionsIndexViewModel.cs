using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoalTracker.ViewModels
{
    public class DiscussionsIndexViewModel
    {
        public List<Discussion> Discussions { get; set; }

        public string FilterInput { get; set; }

        public DiscussionsIndexViewModel() { }
    }
}
