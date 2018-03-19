﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopVision.FleetVision
{
    public class FleetVisionWorkOrder
    {
        public int RecordID { get; set; }
        public string WorkOrderNumber { get; set; }
        public string RequestBy { get; set; }
        public int VehicleRecordID { get; set; }
        public string WorkRequested { get; set; }
        public string Status { get; set; }
        public DateTime InDateTime { get; set; }
        public DateTime OutDateTime { get; set; }
        public DateTime EstDateTime { get; set; }
        public string WorkPerformed { get; set; }
        public decimal PartsTotal { get; set; }
        public decimal LaborTotal { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal ShopFee { get; set; }
        public string InvoiceNumber { get; set; }
        public string _priority { get; set; }
        public string Priority => string.IsNullOrEmpty(this._priority) ? string.Empty : _priority;

        public int PrioritySortOrder
        {
            get
            {
                switch (this.Priority)
                {
                    case "Immediate - Today":
                        return 0;
                        break;
                    case "Medium - 1 to 2 Day":
                        return 1;
                        break;
                    case "Low":
                        return 2;
                        break;
                }

                return 99;
            }
        }

        public int VehicleNumberForSorting
        {
            get
            {
                if (this.Vehicle != null)
                {
                    return Parsers.ParseInt(this.Vehicle.VehicleNumber);
                }

                return 9999;
            }
        }

        public bool IsClosed => this.Status.ToLower().Equals("completed");

        public FleetVisionVehicle Vehicle { get; set; }
    }
}