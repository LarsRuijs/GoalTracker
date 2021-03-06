﻿using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Data.Contexts
{
    public class GoalSql : IGoalContext
    {
        SqlConnection conn = new ConnString(ConnectionType.Remote).Value;

        public List<Goal> GetAllByUserId(int userId)
        {
            // Command definition en settings
            var cmd = new SqlCommand("GetGoals", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

            var goalList = new List<Goal>();

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var goal = new Goal()
                    {
                        GoalId = (int)reader["GoalId"],
                        UserId = (int)reader["UserId"],
                        Title = (string)reader["Title"],
                        Info = (string)reader["Info"],
                        StartDT = (reader["StartDT"] == DBNull.Value) ? null : (DateTime?)reader["StartDT"],
                        EndDT = (DateTime)reader["EndDT"],
                        Progress = (int)reader["Progress"],
                        Status = (GoalStatus)reader["Status"],
                        Strikes = (int)reader["Strikes"],
                        PostDT = (DateTime)reader["PostDT"]
                    };

                    goalList.Add(goal);
                }
            }

            conn.Close();

            return goalList;
        }

        public Goal GetSingle(int goalId)
        {
            // Command definition en settings
            var cmd = new SqlCommand("GetGoal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@goalId", SqlDbType.Int).Value = goalId;

            var goalToReturn = new Goal();

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var goal = new Goal()
                    {
                        GoalId = (int)reader["GoalId"],
                        UserId = (int)reader["UserId"],
                        Title = (string)reader["Title"],
                        Info = (string)reader["Info"],
                        StartDT = (reader["StartDT"] == DBNull.Value) ? null : (DateTime?)reader["StartDT"],
                        EndDT = (DateTime)reader["EndDT"],
                        Progress = (int)reader["Progress"],
                        Status = (GoalStatus)reader["Status"],
                        Strikes = (int)reader["Strikes"],
                        PostDT = (DateTime)reader["PostDT"]
                    };

                    goalToReturn = goal;
                }
            }

            conn.Close();

            return goalToReturn;
        }

        public bool Add(Goal goal)
        {
            // Command definition en settings
            var cmd = new SqlCommand("CreateGoal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = goal.UserId;
            cmd.Parameters.Add("@title", SqlDbType.NVarChar, 150).Value = goal.Title;
            cmd.Parameters.Add("@info", SqlDbType.NVarChar, 8000).Value = goal.Info;
            cmd.Parameters.Add("@startDT", SqlDbType.DateTime).Value = goal.StartDT;
            cmd.Parameters.Add("@endDT", SqlDbType.DateTime).Value = goal.EndDT;
            cmd.Parameters.Add("@progress", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@status", SqlDbType.Int).Value = (int)GoalStatus.InProgress;

            try
            {
                conn.Open();

                int rowsUpdated = cmd.ExecuteNonQuery();

                conn.Close();

                return rowsUpdated > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(Goal goal)
        {
            // Command definition en settings
            var cmd = new SqlCommand("EditGoal", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@goalId", SqlDbType.Int).Value = goal.GoalId;
            cmd.Parameters.Add("@title", SqlDbType.NVarChar, 150).Value = goal.Title;
            cmd.Parameters.Add("@info", SqlDbType.NVarChar, 8000).Value = goal.Info;
            cmd.Parameters.Add("@startDT", SqlDbType.DateTime).Value = goal.StartDT;
            cmd.Parameters.Add("@endDT", SqlDbType.DateTime).Value = goal.EndDT;
            cmd.Parameters.Add("@progress", SqlDbType.Int).Value = goal.Progress;
            cmd.Parameters.Add("@status", SqlDbType.Int).Value = (int)goal.Status;
            cmd.Parameters.Add("@strikes", SqlDbType.Int).Value = goal.Strikes;

            try
            {
                conn.Open();

                int rowsUpdated = cmd.ExecuteNonQuery();

                conn.Close();

                return rowsUpdated > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
