using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
        public string Notes { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
