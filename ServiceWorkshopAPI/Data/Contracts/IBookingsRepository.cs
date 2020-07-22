using ServiceWorkshopAPI.Data.Entities;
using ServiceWorkshopAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Contracts
{
    public interface IBookingsRepository :IRepository<Booking>
    {
        Task<Booking> GetBookingByIdAsync(int bookingId);
        List<BookingsModel> GetFilteredBookingSummariesByDateRange(DateTime startDate, DateTime endDate);
        Task<List<Booking>> GetListOfAllBookingsByClientIdAsync(int clientId);
        Task<List<Booking>> GetListOfAllBookingsByVehicleIdAsync(int vehicleId);
    }
}
