using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Data.Contexts
{
    class CommentSql : ICommentContext
    {
        SqlConnection conn = new SqlConnection(@"Data Source=mssql.fhict.local;Initial Catalog=dbi393093;Persist Security Info=True;User ID=dbi393093;Password=welkom");

        public bool Add(Comment comment)
        {
            // Command definition en settings
            var cmd = new SqlCommand("CreateComment", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = comment.Submitter.UserId;
            cmd.Parameters.Add("@discussionId", SqlDbType.Int).Value = comment.DiscussionId;
            cmd.Parameters.Add("@entry", SqlDbType.NVarChar, -1).Value = comment.Content;

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

        public List<Comment> GetComments(int discussionId)
        {
            // Command definition en settings
            var cmd = new SqlCommand("GetComments", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@discussionId", SqlDbType.Int).Value = discussionId;

            var comments = new List<Comment>();

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var submitter = new User()
                    {
                        UserId = (int)reader["UserId"],
                        Email = (string)reader["Email"],
                        Username = (string)reader["Username"],
                        Role = (UserRole)reader["Role"],
                        Banning = VarHandler.NullableDateTimeHandler(reader["Banning"])
                    };

                    var comment = new Comment()
                    {
                        CommentId = (int)reader["CommentId"],
                        Content = (string)reader["Entry"],
                        PostDT = (DateTime)reader["PostDT"],
                        Likes = (int)reader["Likes"],
                        Submitter = submitter
                    };

                    comments.Add(comment);
                }
            }

            conn.Close();

            return comments;
        }

        public bool LikeUnlike(int userId, int commentId)
        {
            // Command definition en settings
            var cmd = new SqlCommand("LikeUnlikeComment", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@commentId", SqlDbType.Int).Value = commentId;

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
