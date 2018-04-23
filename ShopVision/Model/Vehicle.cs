using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInService { get; set; }
        public string Plate { get; set; }
        public List<string> PreviousPlates { get; set; }
        public string VIN { get; set; }

        public List<VehicleInspection> Inspections { get; set; }

        public string LastInspectionInEnglish
        {
            get
            {
                DateTime d = this.LastInspectionDate;
                if (d == DateTime.MinValue)
                {
                    return "Unknown";
                }
                else
                {
                    return d.ToShortDateString();
                }
            }
        }

        public string ExpiryDateInEnglish
        {
            get
            {
                DateTime d = this.ExpiryDate;
                if (d == DateTime.MinValue)
                {
                    return "Unknown";
                }
                else
                {
                    return d.ToShortDateString();
                }
            }
        }

        public DateTime LastInspectionDate
        {
            get
            {
                if (this.Inspections.Count == 0)
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return Inspections.OrderByDescending(x => x.EffectiveDate).First().EffectiveDate;
                }
            }
        }

        public DateTime ExpiryDate
        {
            get
            {
                if (this.Inspections.Count == 0)
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return Inspections.OrderByDescending(x => x.ExpiryDate).First().ExpiryDate;
                }
            }
        }


        public Vehicle()
        {
            Inspections = new List<VehicleInspection>();
        }
    }
}