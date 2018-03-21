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
                IsImportant = Parsers.ParseBool(dataReader["HighImportance"].ToString())

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
    }
}