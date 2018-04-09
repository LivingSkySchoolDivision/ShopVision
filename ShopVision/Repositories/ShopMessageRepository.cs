using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public class ShopMessageRepository
    {
        private readonly string SQL = "SELECT * FROM ShopMessages";

        public ShopMessageRepository()
        {

        }


        private ShopMessage dataReaderToShopMessage(SqlDataReader dataReader)
        {
            return new ShopMessage()
            {
                ID = Parsers.ParseInt(dataReader["ID"].ToString()),
                Sender = dataReader["MsgSender"].ToString(),
                Content = dataReader["MsgContent"].ToString(),
                Start = Parsers.ParseDate(dataReader["MsgStart"].ToString()),
                End = Parsers.ParseDate(dataReader["MsgEnd"].ToString()),
                IsImportant = Parsers.ParseBool(dataReader["HighImportance"].ToString()),
                Created = Parsers.ParseDate(dataReader["MsgCreated"].ToString())

            };
        }

        public List<ShopMessage> GetAll()
        {

            List<ShopMessage> returnMe = new List<ShopMessage>();

            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = SQL
                };
                sqlCommand.Connection.Open();
                SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        ShopMessage msg = dataReaderToShopMessage(dbDataReader);

                        if (msg != null)
                        {
                            returnMe.Add(msg);
                        }
                    }
                }

                sqlCommand.Connection.Close();
            }

            return returnMe;
        }

        public List<ShopMessage> GetActive()
        {
            List<ShopMessage> returnMe = new List<ShopMessage>();

            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = SQL + " WHERE MsgStart < {fn NOW()} AND MsgEnd > {fn NOW()};"
                };
                sqlCommand.Connection.Open();
                SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        ShopMessage msg = dataReaderToShopMessage(dbDataReader);

                        if (msg != null)
                        {
                            returnMe.Add(msg);
                        }
                    }
                }

                sqlCommand.Connection.Close();
            }

            return returnMe;
        }

        public void Delete(ShopMessage message)
        {
            Delete(message.ID);
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "DELETE FROM ShopMessages WHERE ID=@MSGID"
                };
                sqlCommand.Parameters.AddWithValue("MSGID", id);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
        }


        public void Expire(ShopMessage message)
        {
            Expire(message.ID);
        }

        public void Expire(int id)
        {
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "UPDATE ShopMessages SET MsgEnd=@MSGEND WHERE ID=@MSGID"
                };
                sqlCommand.Parameters.AddWithValue("MSGID", id);                
                sqlCommand.Parameters.AddWithValue("MSGEND", DateTime.Now);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
        }

        public void Add(string MsgSender, string MsgContent, DateTime MsgStart, DateTime MsgEnd, bool IsImportant, string Icon)
        {
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "INSERT INTO ShopMessages(MsgSender, MsgContent, MsgStart, MsgEnd, HighImportance, MsgCreated, MsgIcon) VALUES(@MSGSENDER, @MSGCONTENT, @MSGSTART, @MSGEND, @MSGHIGH, @MSGCREATED, @MSGICON);"
                };
                sqlCommand.Parameters.AddWithValue("MSGSENDER", MsgSender);
                sqlCommand.Parameters.AddWithValue("MSGCONTENT", MsgContent);
                sqlCommand.Parameters.AddWithValue("MSGSTART", MsgStart);
                sqlCommand.Parameters.AddWithValue("MSGEND", MsgEnd);
                sqlCommand.Parameters.AddWithValue("MSGHIGH", IsImportant);
                sqlCommand.Parameters.AddWithValue("MSGCREATED", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("MSGICON", Icon);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }

        }
        
    }
}