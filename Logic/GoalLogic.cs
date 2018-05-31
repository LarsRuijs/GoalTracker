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
        GoalRepository repo = new GoalRepository(StorageType.Memory);

        public List<Goal> GetGoalsByUserId(int userId) => repo.GetGoalsByUserId(userId);

        public Goal GetGoalById(int goalId) => repo.GetGoalById(goalId);

        public bool CreateGoal(Goal goal)
        {
            // Checkt de goal voor ongewenste of inconsistente gegevens.
            if (!CheckForInconsistenties(goal))
                throw new Exception("The input is missing required data.");

            bool created = repo.CreateGoal(goal);

            return created;
        }

        public bool EditGoal(Goal goal)
        {
            // Checkt de goal voor ongewenste of inconsistente gegevens.
            if (!CheckForInconsistenties(goal))
                return false;

            // Wanneer de goal succesvol is afgerond wordt de progress parameter automatisch 100%.
            if (goal.Status == GoalStatus.Finished && goal.Progress != 100)
                goal.Progress = 100;

            bool edited = repo.EditGoal(goal);

            return edited;
        }

        private bool CheckForInconsistenties(Goal goal)
        {
            if (goal.Title == "")
                return false;

            if (goal.Progress < 0 || goal.Progress > 100)
                return false;

            if (goal.EndDT < DateTime.Now && goal.Status == GoalStatus.InProgress)
                return false;

            if (goal.StartDT.HasValue)
            {
                if (goal.StartDT > goal.EndDT)
                    return false;
            }

            return true;
        }
    }
}
