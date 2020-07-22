using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Model { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
