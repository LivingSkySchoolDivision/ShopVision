using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public class VehicleInspectionRepository
    {
        
        public List<VehicleInspection> GetForVehicle(Vehicle vehicle)
        {
            return GetForVehicle(vehicle.ID);
        }

        private VehicleInspection dataReaderToInspection(SqlDataReader dataReader)
        {
            return new VehicleInspection()
            {
                ID = Parsers.ParseInt(dataReader["inspectionid"].ToString().Trim()),
                VehicleID = Parsers.ParseInt(dataReader["vehicleid"].ToString().Trim()),
                Description = dataReader["InspectionDescription"].ToString().Trim(),
                EffectiveDate = Parsers.ParseDate(dataReader["InspectionEffectiveDate"].ToString().Trim()),
                ExpiryDate = Parsers.ParseDate(dataReader["InspectionExpiryDate"].ToString().Trim())
            };
        }

        public List<VehicleInspection> GetForVehicle(int id)
        {
            List<VehicleInspection> returnMe = new List<VehicleInspection>();

            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM VehicleInspections WHERE VehicleID=@VEHICLEID"
                };
                sqlCommand.Parameters.AddWithValue("VEHICLEID",id);
                sqlCommand.Connection.Open();
                SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        VehicleInspection inspection = dataReaderToInspection(dbDataReader);
                        if (inspection != null)
                        {
                            returnMe.Add(inspection);
                        }
                    }
                }

                sqlCommand.Connection.Close();
            }

            return returnMe;
        }
    }
}