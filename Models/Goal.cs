using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public DateTime? StartDT { get; set; }
        public DateTime EndDT { get; set; }
        public int Progress { get; set; }
        public GoalStatus Status { get; set; }
        public int Strikes { get; set; }
        public DateTime PostDT { get; set; }

        public Goal(int goalId, int userId, string title, string info, DateTime? startDT, DateTime endDT, int progress, GoalStatus status, int strikes, DateTime postDT)
        {
            GoalId = goalId;
            UserId = userId;
            Title = title;
            Info = info;
            StartDT = startDT;
            EndDT = endDT;
            Progress = progress;
            Status = status;
            Strikes = strikes;
            PostDT = postDT;
        }

        public Goal() { }
    }
}
