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

        public List<Goal> GetAllByUserId(int userId) => context.GetAllByUserId(userId);

        public Goal GetSingle(int goalId) => context.GetSingle(goalId);

        public bool Add(Goal goal) => context.Add(goal);

        public bool Edit(Goal goal) => context.Edit(goal);
    }
}
