using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public class VehicleRepository
    {
        VehicleInspectionRepository inspectionRepo = new VehicleInspectionRepository();


        private Vehicle dataReaderToVehicle(SqlDataReader dataReader)
        {
            return new Vehicle()
            {
                ID = Parsers.ParseInt(dataReader["ID"].ToString().Trim()),
                Name = dataReader["VehicleName"].ToString().Trim(),
                IsInService = Parsers.ParseBool(dataReader["IsInService"].ToString().Trim()),
                Plate = dataReader["Plate"].ToString().Trim(),
                VIN = dataReader["VIN"].ToString().Trim(),
                Inspections = inspectionRepo.GetForVehicle(Parsers.ParseInt(dataReader["ID"].ToString().Trim()))
            };
        }


        public List<Vehicle> GetAll()
        {
            List<Vehicle> returnMe = new List<Vehicle>();

            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM Vehicles"
                };
                sqlCommand.Connection.Open();
                SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        Vehicle v = dataReaderToVehicle(dbDataReader);

                        if (v != null)
                        {
                            returnMe.Add(v);
                        }
                    }
                }

                sqlCommand.Connection.Close();
            }

            return returnMe;
        }

        public Vehicle Get(string vehicleID)
        {
            return Get(Parsers.ParseInt(vehicleID));
        }

        public Vehicle Get(int vehicleID)
        {            
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM Vehicles WHERE ID=@VID"
                };
                sqlCommand.Parameters.AddWithValue("VID", vehicleID);
                sqlCommand.Connection.Open();
                SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        Vehicle v = dataReaderToVehicle(dbDataReader);

                        if (v != null)
                        {
                            return v;
                        }
                    }
                }

                sqlCommand.Connection.Close();
            }

            return null;
        }

    }
}