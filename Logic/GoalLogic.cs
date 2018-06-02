using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Contexts;
using Data;

namespace Logic
{
    public class GoalLogic
    {
        GoalRepository repo = new GoalRepository(StorageType.Database);

        public List<Goal> GetAllByUserId(int userId) => repo.GetAllByUserId(userId);

        public Goal GetSingle(int goalId) => repo.GetSingle(goalId);

        public bool Add(int userId, string title, string info, DateTime? startDT, DateTime endDT)
        {
            Goal goal = new Goal()
            {
                UserId = userId,
                Title = title,
                Info = info,
                StartDT = startDT,
                EndDT = endDT
            };

            // Checkt de goal voor ongewenste of inconsistente gegevens.
            if (!goal.CheckForInconsistenties())
                return false;

            bool response = repo.Add(goal);

            return response;
        }

        public bool Edit(Goal goal)
        {
            // Wanneer de goal succesvol is afgerond wordt de progress parameter automatisch 100%.
            if (goal.Status == GoalStatus.Finished && goal.Progress != 100)
                goal.Progress = 100;

            // Checkt de goal voor ongewenste of inconsistente gegevens.
            if (!goal.CheckForInconsistenties())
                return false;

            bool response = repo.Edit(goal);

            return response;
        }

        public bool FinishGoal(int goalId)
        {
            Goal goal = GetSingle(goalId);

            // Wanneer de goal succesvol is afgerond wordt de progress parameter automatisch 100%.
            if (goal.Status == GoalStatus.Finished && goal.Progress != 100)
                goal.Progress = 100;

            bool response = repo.Edit(goal);

            return response;
        }
    }
}
