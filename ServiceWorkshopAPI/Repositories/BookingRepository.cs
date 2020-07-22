using Microsoft.EntityFrameworkCore;
using Npgsql;
using ServiceWorkshopAPI.Contexts;
using ServiceWorkshopAPI.Data.Contracts;
using ServiceWorkshopAPI.Data.Entities;
using ServiceWorkshopAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Repositories
{
    internal sealed class BookingRepository : Repository<Booking>, IBookingsRepository
    {
        private const string CONNECTIONSTRING = "Host=localhost;Port=5432;Username=postgres;Password=Mikan1967;Database=servicedb";

        public BookingRepository(ServiceWorkshopDbContext context) : base(context)
        {
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            Booking booking = await Context.Bookings.AsNoTracking()
                .Include(b => b.Client)
                .Include(b => b.Vehicle)
                .FirstOrDefaultAsync(c => c.BookingId.Equals(bookingId));

            return booking;
        }

        public List<BookingsModel> GetFilteredBookingSummariesByDateRange(DateTime startDate, DateTime endDate)
        {
            string cs = CONNECTIONSTRING;
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM bookings WHERE bookingDate >= " + startDate + " AND bookingDate <= " + endDate;
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();
            var bookings = new List<BookingsModel>();

            while (rdr.Read())
            {
                var booking = new BookingsModel
                {
                    BookingId = rdr.GetInt32(0),
                    Notes = rdr.GetString(1),
                    BookingDate = rdr.GetDateTime(2)
                };

                bookings.Add(booking);
            }
            return bookings;
        }

        public async Task<List<Booking>> GetListOfAllBookingsByClientIdAsync(int clientId)
        {
            var bookinglist = await Context.Bookings.AsNoTracking()
                .Where(m => m.ClientId == clientId)
                .Include(m => m.Client)
                .ToListAsync();

            return bookinglist;
        }

        public async Task<List<Booking>> GetListOfAllBookingsByVehicleIdAsync(int vehicleId)
        {
            var bookinglist = await Context.Bookings.AsNoTracking()
                .Where(m => m.VehicleId == vehicleId)
                .Include(m => m.Vehicle)
                .ToListAsync();

            return bookinglist;
        }
    }
}
