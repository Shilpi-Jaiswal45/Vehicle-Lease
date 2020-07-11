using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseVehicle.Models
{
    public class VMSerach
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
    }
}