using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contexts
{
    internal interface IGoalContext
    {
        List<Goal> GetGoalsByUserId(int userId);
        Goal GetGoalById(int goalId);
        bool CreateGoal(Goal goal);
        bool EditGoal(Goal goal);
    }
}
