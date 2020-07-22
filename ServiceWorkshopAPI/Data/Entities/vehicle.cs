using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Entities
{
    public class Vehicle
    {
        public string Model { get; set; }
        public int VehicleId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
