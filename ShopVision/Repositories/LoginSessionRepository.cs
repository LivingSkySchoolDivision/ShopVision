﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public class LoginSessionRepository
    {
        private LoginSession dataReaderToLoginSession(SqlDataReader dataReader)
        {
            return new LoginSession()
            {
                Username = dataReader["username"].ToString(),
                IPAddress = dataReader["ip"].ToString(),
                Thumbprint = dataReader["thumbprint"].ToString(),
                UserAgent = dataReader["useragent"].ToString(),
                SessionStarts = DateTime.Parse(dataReader["sessionstarts"].ToString()),
                SessionEnds = DateTime.Parse(dataReader["sessionends"].ToString())
            };
        }



        public List<LoginSession> LoadAll()
        {
            List<LoginSession> returnMe = new List<LoginSession>();

            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM sessions;"
                };
                sqlCommand.Connection.Open();
                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        returnMe.Add(dataReaderToLoginSession(dataReader));
                    }
                }
                sqlCommand.Connection.Close();
            }

            return returnMe;

        }

        /// <summary>
        /// Loads the specified session, if it exists and if it is valid
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="hash"></param>
        /// <param name="ip"></param>
        /// <param name="useragent"></param>
        /// <returns></returns>
        public LoginSession LoadIfValid(string hash, string ip, string useragent)
        {
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText =
                        "SELECT * FROM sessions WHERE thumbprint=@Hash AND ip=@IP AND useragent=@UA AND sessionstarts < {fn NOW()} AND sessionends > {fn NOW()};"
                };
                sqlCommand.Parameters.AddWithValue("@Hash", hash);
                sqlCommand.Parameters.AddWithValue("@IP", ip);
                sqlCommand.Parameters.AddWithValue("@UA", useragent);
                sqlCommand.Connection.Open();
                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        return dataReaderToLoginSession(dataReader);
                    }
                }

                sqlCommand.Connection.Close();
            }
            return null;
        }

        /// <summary>
        /// Deletes a session from the session table
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool Expire(string hash)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
                {
                    SqlCommand sqlCommand = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.Text,
                        CommandText = "DELETE FROM sessions WHERE thumbprint=@Hash;"
                    };
                    sqlCommand.Parameters.AddWithValue("@Hash", hash);
                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Loads all sessions who's dates and times are currently within the active period
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public List<LoginSession> LoadActive()
        {
            List<LoginSession> returnMe = new List<LoginSession>();

            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText =
                        "SELECT * FROM sessions WHERE sessionstarts < {fn NOW()} AND sessionends > {fn NOW()};";

                    sqlCommand.Connection.Open();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            returnMe.Add(dataReaderToLoginSession(dataReader));
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }

            return returnMe;
        }

        /// <summary>
        /// Generates a new random string to use as a session ID
        /// </summary>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string CreateSessionID(string seed)
        {
            return Crypto.MD5("LSKY" + DateTime.Now.ToString("ffffff") + seed);
        }

        /// <summary>
        /// Creates a new session and returns the session ID. This assumes that the username and password were valid.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="remoteIP"></param>
        /// <param name="useragent"></param>
        /// <returns></returns>
        public string CreateSession(string username, string remoteIP, string useragent)
        {
            // Generate a session ID
            string newSessionID = CreateSessionID(username + remoteIP + useragent);
            // Determine a timespan for this session based on the current time of day
            // If logging in during the work day, make a session last 8 hours
            // If logging in after hours, make the session only last 2 hours

            TimeSpan sessionDuration = new TimeSpan(0, 30, 0);

            // Create a session in the database 
            // Also while we are querying the database, clear out expired sessions that are lingering, and clear any existing sessions for
            // this user, limiting the site to one session per user (per site code)
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "DELETE FROM sessions WHERE sessionends < {fn NOW()};DELETE FROM sessions WHERE username=@USERNAME;INSERT INTO sessions(thumbprint,username,ip,useragent,sessionstarts,sessionends) VALUES(@ID, @USERNAME, @IP, @USERAGENT, @SESSIONSTART, @SESSIONEND);";
                    sqlCommand.Parameters.AddWithValue("@ID", newSessionID);
                    sqlCommand.Parameters.AddWithValue("@USERNAME", username);
                    sqlCommand.Parameters.AddWithValue("@IP", remoteIP);
                    sqlCommand.Parameters.AddWithValue("@USERAGENT", useragent);
                    sqlCommand.Parameters.AddWithValue("@SESSIONSTART", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@SESSIONEND", DateTime.Now.Add(sessionDuration));

                    sqlCommand.Connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Connection.Close();

                    return newSessionID;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Purges expires sessions from the database
        /// </summary>
        /// <param name="connection"></param>
        public void PurgeAllExpiredSessions()
        {
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "DELETE FROM sessions WHERE sessionends < {fn NOW()};"
                };
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
        }

        /// <summary>
        /// Retreives the session ID from the users's cookies
        /// </summary>
        /// <returns></returns>
        public string GetSessionIDFromCookies(HttpRequest request)
        {
            HttpCookie sessionCookie = request.Cookies[Settings.CookieName];
            if (sessionCookie != null)
            {
                return sessionCookie.Value.Trim();
            }
            else
            {
                return string.Empty.Trim();
            }
        }


    }
}