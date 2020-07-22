using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Entities
{
    public class Booking
    {
        public string Notes { get; set; }
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
        public DateTime BookingDate { get; set; }
        public virtual Client Client { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public bool Update(Booking booking)
        {
            bool shouldUpdate = false;

            if (!Notes.Equals(booking.Notes, StringComparison.OrdinalIgnoreCase))
            {
                Notes = booking.Notes;
                shouldUpdate = true;
            }

            if (!BookingDate.Equals(booking.BookingDate))
            {
                BookingDate = booking.BookingDate;
                shouldUpdate = true;
            }

            if (!ClientId.Equals(booking.ClientId))
            {
                ClientId = booking.ClientId;
                shouldUpdate = true;
            }

            if (!VehicleId.Equals(booking.ClientId))
            {
                VehicleId = booking.VehicleId;
                shouldUpdate = true;
            }

            return shouldUpdate;
        }
    }
}
