using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Models
{
    public class VehiclesModel
    {
        public int VehicleId { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
