﻿using Data.Contexts;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Data.Contexts
{
    internal class UserSql : IUserContext
    {
        SqlConnection conn = new ConnString(ConnectionType.Remote).Value;

        public string CheckIfUserExists(string email, string username)
        {
            string eMessage = CheckByEmail(email);

            // Return de error message gelijk wanneer die gevuld is.
            if (eMessage != "")
                return eMessage;

            eMessage = CheckByUsername(username);

            // Return de error message sowieso.
            return eMessage;
        }

        private string CheckByEmail(string email)
        {
            // Command definition en settings
            var cmd = new SqlCommand("CheckIfUserExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = email;
            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100).Value = "";

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                conn.Close();

                return "This email address is already in use.";
            }

            conn.Close();

            return "";
        }

        private string CheckByUsername(string username)
        {
            // Command definition en settings
            var cmd = new SqlCommand("CheckIfUserExists", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = "";
            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100).Value = username;

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                conn.Close();

                return "This username is already in use.";
            }

            conn.Close();

            return "";
        }

        public User GetUserByLogin(User input)
        {
            // Command definition en settings
            var cmd = new SqlCommand("Login", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            // Command parameters
            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100).Value = input.Username;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = input.Password;

            var userToReturn = new User();

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var user = new User()
                    {
                        UserId = (int)reader["UserId"],
                        Email = (string)reader["Email"],
                        Username = (string)reader["Username"],
                        Role = (UserRole)reader["Role"],
                        Banning = (reader["Banning"] == DBNull.Value) ? null : (DateTime?)reader["Banning"]
                    };

                    userToReturn = user;
                }
            }

            conn.Close();

            return userToReturn;
        }

        public bool Register(User user)
        {
            // Command definition en settings
            var cmd = new SqlCommand("Register", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = user.Email;
            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100).Value = user.Username;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = user.Password;
            cmd.Parameters.Add("@role", SqlDbType.Int).Value = (int)user.Role;

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

        public List<User> GetAllByFilter(string filter)
        {
            // Command definition en settings
            var cmd = new SqlCommand("GetUsers", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@filter", SqlDbType.NVarChar, -1).Value = filter;

            var users = new List<User>();

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var user = new User()
                    {
                        UserId = (int)reader["UserId"],
                        Email = (string)reader["Email"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        Role = (UserRole)reader["Role"],
                        Banning = (reader["Banning"] == DBNull.Value) ? null : (DateTime?)reader["Banning"]
                    };

                    users.Add(user);
                }
            }

            conn.Close();

            return users;
        }

        public User GetSingle(int userId)
        {
            // Command definition en settings
            var cmd = new SqlCommand("GetUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

            var user = new User();

            conn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user = new User()
                    {
                        UserId = (int)reader["UserId"],
                        Email = (string)reader["Email"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        Role = (UserRole)reader["Role"],
                        Banning = (reader["Banning"] == DBNull.Value) ? null : (DateTime?)reader["Banning"]
                    };
                }
            }

            conn.Close();

            return user;
        }

        public bool Edit(User user)
        {
            // Command definition en settings
            var cmd = new SqlCommand("EditUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Command parameters
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = user.UserId;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = user.Email;
            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 100).Value = user.Username;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = user.Password;
            cmd.Parameters.Add("@role", SqlDbType.Int).Value = (int)user.Role;
            cmd.Parameters.Add("@banning", SqlDbType.DateTime).Value = user.Banning;

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
