using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Models
{
    public class BookingsModel
    {
        public int BookingId { get; set; }

        [StringLength(25)]
        public string Notes { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        public int ClientId { get; set; }

        public int VehicleId { get; set; }
    }
}
