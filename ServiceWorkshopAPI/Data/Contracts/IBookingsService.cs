using ServiceWorkshopAPI.Data.Entities;
using ServiceWorkshopAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Contracts
{
    public interface IBookingsService
    {
        List<BookingsModel> GetBookingDetails();
        BookingsModel GetBookingDetailsById(int id);
        List<BookingsModel> GetFilteredBookingSummariesByDateRange(string startDate, string endDate);
        BookingsModel AddBooking(BookingsModel bookingModel);
        void UpdateBookingDetails(string Id, string path, string value);
        bool RemoveBooking(int id);
    }
}
