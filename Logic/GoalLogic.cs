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

        /// <summary>
        /// Gets all goals of a single user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Goal> GetAllByUserId(int userId) => repo.GetAllByUserId(userId);

        /// <summary>
        /// Gets a single discussion.
        /// </summary>
        /// <param name="goalId"></param>
        /// <returns></returns>
        public Goal GetSingle(int goalId) => repo.GetSingle(goalId);

        /// <summary>
        /// Adds and checks the newly submitted goal.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="title"></param>
        /// <param name="info"></param>
        /// <param name="startDT"></param>
        /// <param name="endDT"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Edits a goal.
        /// </summary>
        /// <param name="goal"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Finishes a goal. 
        /// </summary>
        /// <param name="goalId"></param>
        /// <returns></returns>
        public bool FinishGoal(int goalId)
        {
            Goal goal = GetSingle(goalId);

            goal.Status = GoalStatus.Finished;

            // Wanneer de goal succesvol is afgerond wordt de progress parameter automatisch 100%.
            if (goal.Progress != 100)
                goal.Progress = 100;

            bool response = repo.Edit(goal);

            return response;
        }
    }
}
