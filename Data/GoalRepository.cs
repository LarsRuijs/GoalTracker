using Data.Contexts;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class GoalRepository
    {
        private IGoalContext context;

        public GoalRepository(StorageType st)
        {
            switch (st)
            {
                case StorageType.Memory: context = new GoalMemory(); break;
                case StorageType.Database: context = new GoalSql(); break;
            }
        }

        public List<Goal> GetGoalsByUserId(int userId) => context.GetGoalsByUserId(userId);

        public Goal GetGoalById(int goalId) => context.GetGoalById(goalId);

        public bool CreateGoal(Goal goal) => context.CreateGoal(goal);

        public bool EditGoal(Goal goal) => context.EditGoal(goal);
    }
}
