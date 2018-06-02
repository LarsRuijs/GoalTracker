using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace Data.Contexts
{
    public class DiscussionSql : IDiscussionContext
    {
        SqlConnection conn = new SqlConnection(@"Data Source=mssql.fhict.local;Initial Catalog=dbi393093;Persist Security Info=True;User ID=dbi393093;Password=welkom");

        public bool Add(Discussion discussion)
        {
            // Command definition en settings
            var cmd = new SqlCommand("CreateDiscussion", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = discussion.Submitter.UserId;
            cmd.Parameters.Add("@title", SqlDbType.NVarChar, 100).Value = discussion.Title;
            cmd.Parameters.Add("@description", SqlDbType.NVarChar, -1).Value = discussion.Title;

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

        public bool Edit(Discussion discussion)
        {
            // Command definition en settings
            var cmd = new SqlCommand("EditDiscussion", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@discussionId", SqlDbType.Int).Value = discussion.DiscussionId;
            cmd.Parameters.Add("@title", SqlDbType.NVarChar, 100).Value = discussion.Title;
            cmd.Parameters.Add("@description", SqlDbType.NVarChar, -1).Value = discussion.Title;
            cmd.Parameters.Add("@locked", SqlDbType.Bit).Value = discussion.Locked;

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

        public bool Delete(int discussionId)
        {
            // Command definition en settings
            var cmd = new SqlCommand("DeleteDiscussion", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@discussionId", SqlDbType.Int).Value = discussionId;

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

        public List<Discussion> GetAll(string filterInput = "")
        {
            // Command definition en settings
            var cmd = new SqlCommand("GetDiscussions", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@input", SqlDbType.NVarChar, -1).Value = filterInput;

            var discussionList = new List<Discussion>();

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // User apart aanmaken om in de discussion te stoppen
                    var submitter = new User()
                    {
                        UserId = (int)reader["UserId"],
                        Email = (string)reader["Email"],
                        Username = (string)reader["Username"],
                        Role = (UserRole)reader["Role"],
                        Banning = VarHandler.NullableDateTimeHandler(reader["Banning"])
                    };

                    var discussion = new Discussion()
                    {
                        DiscussionId = (int)reader["DiscussionId"],
                        Title = (string)reader["Title"],
                        Content = (string)reader["Description"],
                        PostDT = (DateTime)reader["PostDT"],
                        Likes = (int)reader["Likes"],
                        Locked = (bool)reader["Locked"],
                        Submitter = submitter
                    };

                    discussionList.Add(discussion);
                }
            }

            conn.Close();

            return discussionList;
        }

        public Discussion GetSingle(int discussionId)
        {
            // Command definition en settings
            var cmd = new SqlCommand("GetDiscussion", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@discussionId", SqlDbType.Int).Value = discussionId;

            var discussion = new Discussion();

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // User apart aanmaken om in de discussion te stoppen
                    var submitter = new User()
                    {
                        UserId = (int)reader["UserId"],
                        Email = (string)reader["Email"],
                        Username = (string)reader["Username"],
                        Role = (UserRole)reader["Role"],
                        Banning = VarHandler.NullableDateTimeHandler(reader["Banning"])
                    };

                    discussion = new Discussion()
                    {
                        DiscussionId = (int)reader["DiscussionId"],
                        Title = (string)reader["Title"],
                        Content = (string)reader["Description"],
                        PostDT = (DateTime)reader["PostDT"],
                        Likes = reader["Likes"] == null ? 0 : (int)reader["Likes"],
                        Locked = (bool)reader["Locked"],
                        Submitter = submitter
                    };                    
                }
            }

            conn.Close();

            return discussion;
        }

        public bool LikeUnlike(int userId, int discussionId)
        {
            // Command definition en settings
            var cmd = new SqlCommand("LikeUnlikeDiscussion", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@discussionId", SqlDbType.Int).Value = discussionId;

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
