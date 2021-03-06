﻿using System;
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

        public VehicleInspection Add(int VehicleID, string Description, DateTime EffectiveDate, DateTime ExpiryDate)
        {
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "INSERT INTO VehicleInspections(VehicleID, InspectionDescription, InspectionEffectiveDate, InspectionExpiryDate) VALUES(@VID, @DESC, @EFFECTIVE, @EXPIRY);"
                };

                sqlCommand.Parameters.AddWithValue("VID", VehicleID);
                sqlCommand.Parameters.AddWithValue("DESC", Description);
                sqlCommand.Parameters.AddWithValue("EFFECTIVE", EffectiveDate);
                sqlCommand.Parameters.AddWithValue("EXPIRY", ExpiryDate);

                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();
            }

            // Now retreive the object we just added so we can return it
            using (SqlConnection connection = new SqlConnection(Settings.DBConnectionString_ShopVision))
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM VehicleInspections WHERE VehicleID=@VID AND InspectionDescription=@DESC AND InspectionEffectiveDate=@EFFECTIVE AND InspectionExpiryDate=@EXPIRY;"
                };

                sqlCommand.Parameters.AddWithValue("VID", VehicleID);
                sqlCommand.Parameters.AddWithValue("DESC", Description);
                sqlCommand.Parameters.AddWithValue("EFFECTIVE", EffectiveDate);
                sqlCommand.Parameters.AddWithValue("EXPIRY", ExpiryDate);

                sqlCommand.Connection.Open();
                SqlDataReader dbDataReader = sqlCommand.ExecuteReader();

                if (dbDataReader.HasRows)
                {
                    while (dbDataReader.Read())
                    {
                        VehicleInspection inspection = dataReaderToInspection(dbDataReader);
                        if (inspection != null)
                        {
                            return inspection;
                        }
                    }
                }
                sqlCommand.Connection.Close();
            }

            // If we're unable to find it in the database, return what we were given (with an invalid ID number)
            return new VehicleInspection()
            {
                VehicleID = VehicleID,
                Description = Description,
                EffectiveDate = EffectiveDate,
                ExpiryDate = ExpiryDate,
                ID = -1
            };
        }

        public VehicleInspection Add(VehicleInspection inspection)
        {
            return Add(inspection.VehicleID, inspection.Description, inspection.EffectiveDate, inspection.ExpiryDate);
        }
    }
}