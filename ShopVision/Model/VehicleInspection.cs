using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopVision
{
    public class VehicleInspection
    {
        public int ID { get; set; }
        public int VehicleID { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}