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
        public bool IsInService { get; set; }
        public List<VehicleInspection> Inspections { get; set; }
    }
}