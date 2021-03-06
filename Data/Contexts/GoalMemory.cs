﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace Data.Contexts
{
    class GoalMemory : IGoalContext
    {
        List<Goal> goals = new List<Goal>();

        public GoalMemory()
        {
            goals.Add(new Goal() { GoalId = 2, UserId = 1, Title = "Dit is mijn goal", Info = "Dit is mijn goal omschrijving.", StartDT = Convert.ToDateTime("2018-03-25"), EndDT = Convert.ToDateTime("2018-06-15"), PostDT = Convert.ToDateTime("2018-03-25 12:23:00"), Status = GoalStatus.InProgress, Progress = 34, Strikes = 1 });
            goals.Add(new Goal() { GoalId = 2, UserId = 1, Title = "Ik ga mijn project af krijgen", EndDT = Convert.ToDateTime("2018-07-29"), PostDT = Convert.ToDateTime("2018-03-31 22:44:00"), Status = GoalStatus.InProgress, Progress = 50 });
        }

        Random rnd = new Random();

        public bool Add(Goal goal)
        {
            goal = CreateNecessaryGoalData(goal);

            goals.Add(goal);

            return true;
        }

        private Goal CreateNecessaryGoalData(Goal goal)
        {
            if (goal.GoalId < 0)
            {
                var unique = false;

                while (!unique)
                {
                    goal.GoalId = rnd.Next(1, 100);

                    if (goals.Exists(g => g.GoalId != goal.GoalId))
                    {
                        unique = true;
                    }
                }
            }

            goal.PostDT = DateTime.Now;

            return goal;
        }

        public bool Edit(Goal goal)
        {
            var goalToEdit = goals.Find(g => g.GoalId == goal.GoalId);

            goalToEdit.Title = goal.Title;
            goalToEdit.Info = goal.Info;
            goalToEdit.StartDT = goal.StartDT;
            goalToEdit.EndDT = goal.EndDT;
            goalToEdit.Status = goal.Status;
            goalToEdit.Progress = goal.Progress;
            goalToEdit.Strikes = goal.Strikes;

            return true;
        }

        public Goal GetSingle(int goalId) => goals.FirstOrDefault(g => g.GoalId == goalId);

        public List<Goal> GetAllByUserId(int userId)
        {
            var list = new List<Goal>();

            foreach (var goal in goals.Where(g => g.UserId == userId))
            {
                list.Add(goal);
            }

            list = list.OrderByDescending(g => g.EndDT).ToList();

            return list;
        }
    }
}
