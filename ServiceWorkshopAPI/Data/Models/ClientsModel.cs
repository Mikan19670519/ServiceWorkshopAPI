using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Models
{
    public class ClientsModel
    {
        public int ClientId { get; set; }

        [StringLength(250)]
        public string FirstName { get; set; }

    }
}
