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
                Inspections = inspectionRepo.GetForVehicle(Parsers.ParseInt(dataReader["ID"].ToString().Trim())),
                Description = dataReader["VehicleDescription"].ToString().Trim()
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
                
        public void MakeVehicleActive(int vehicleID)
        {
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "UPDATE Vehicles SET IsInService=1 WHERE ID=@VID"
                };
                sqlCommand.Parameters.AddWithValue("VID", vehicleID);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
        }

        public void MakeVehicleInactive(int vehicleID)
        {
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "UPDATE Vehicles SET IsInService=0 WHERE ID=@VID"
                };
                sqlCommand.Parameters.AddWithValue("VID", vehicleID);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
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

        public Vehicle Add()
        {
            // Add a vehicle with a randomized name
            string name = "TEMP-" + Crypto.MD5(DateTime.Now.ToString("yyyyMMddHHmmssFFFFFFF"));

            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "INSERT INTO Vehicles(VehicleName, IsInservice, Plate, VIN, VehicleDescription) VALUES(@VNAME, @ISACTIVE, @PLATE, @VIN, @DESC)"
                };

                sqlCommand.Parameters.AddWithValue("VNAME", name);
                sqlCommand.Parameters.AddWithValue("ISACTIVE", true);
                sqlCommand.Parameters.AddWithValue("PLATE", "??? ???");
                sqlCommand.Parameters.AddWithValue("VIN", "????????????");
                sqlCommand.Parameters.AddWithValue("DESC", "");

                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }

            // Return the vehicle object so the caller can be redirected to an edit page
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM Vehicles WHERE VehicleName=@VNAME"
                };
                sqlCommand.Parameters.AddWithValue("VNAME", name);
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

        public void Update(Vehicle vehicle)
        {
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "UPDATE Vehicles SET VehicleName=@VNAME, IsInService=@ISACTIVE, Plate=@PLATE ,VIN=@VIN, VehicleDescription=@DESC WHERE ID=@VID"
                };

                sqlCommand.Parameters.AddWithValue("VID", vehicle.ID);
                sqlCommand.Parameters.AddWithValue("VNAME", vehicle.Name);
                sqlCommand.Parameters.AddWithValue("ISACTIVE", vehicle.IsInService);
                sqlCommand.Parameters.AddWithValue("PLATE", vehicle.Plate);
                sqlCommand.Parameters.AddWithValue("VIN", vehicle.VIN);
                sqlCommand.Parameters.AddWithValue("DESC", vehicle.Description);

                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }
        }

    }
}