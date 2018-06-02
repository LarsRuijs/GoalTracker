using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contexts
{
    internal interface IGoalContext
    {
        List<Goal> GetAllByUserId(int userId);
        Goal GetSingle(int goalId);
        bool Add(Goal goal);
        bool Edit(Goal goal);
    }
}
