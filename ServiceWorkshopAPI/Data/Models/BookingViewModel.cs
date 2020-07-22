using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Models
{
    public class BookingViewModel
    {
        [StringLength(25, MinimumLength = 3)]
        public string Notes { get; set; }
        [Required]
        public DateTime bookingdate { get; set; }
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
    }
}
