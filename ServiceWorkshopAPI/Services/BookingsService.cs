using Npgsql;
using ServiceWorkshopAPI.Data.Contracts;
using ServiceWorkshopAPI.Data.Entities;
using ServiceWorkshopAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Services
{
    public class BookingsService : IBookingsService
    {
        private const string CONNECTIONSTRING = "Host=localhost;Port=5432;Username=postgres;Password=Mikan1967;Database=servicedb";
        private const int MAXIMUM_NOTE_LENGTH = 25;

        public BookingsService()
        {
        }

        public List<BookingsModel> GetBookingDetails()
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"SELECT * FROM bookings";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            var bookings = new List<BookingsModel>();

            while (dr.Read())
            {
                var booking = new BookingsModel
                {
                    BookingId = dr.GetInt32(0),
                    ClientId = dr.GetInt32(1),
                    VehicleId = dr.GetInt32(2),
                    Notes = dr.GetString(3),
                    BookingDate = dr.GetDateTime(4)
                };

                bookings.Add(booking);
            }

            conn.Close();
            return bookings;
        }

        public BookingsModel GetBookingDetailsById(int id)
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"SELECT * FROM bookings WHERE bookingid = " + id;
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            var booking = new BookingsModel();

            while (dr.Read())
            {
                var bookingModel = new BookingsModel
                {
                    BookingId = dr.GetInt32(0),
                    ClientId = dr.GetInt32(1),
                    VehicleId = dr.GetInt32(2),
                    Notes = dr.GetString(3),
                    BookingDate = dr.GetDateTime(4)
                };
                booking = bookingModel;
            }

            conn.Close();
            return booking;
        }

        public List<BookingsModel> GetFilteredBookingSummariesByDateRange(string startDate, string endDate)
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = "SELECT * FROM bookings WHERE bookingdate >= cast('" + startDate + "' AS DATE) AND bookingdate <= cast('" + endDate + "' AS DATE)";
            var cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            var bookings = new List<BookingsModel>();

            while (dr.Read())
            {
                var booking = new BookingsModel
                {
                    BookingId = dr.GetInt32(0),
                    ClientId = dr.GetInt32(1),
                    VehicleId = dr.GetInt32(2),
                    Notes = dr.GetString(3),
                    BookingDate = dr.GetDateTime(4)
                };

                bookings.Add(booking);
            }

            conn.Close();
            return bookings;
        }

        public BookingsModel AddBooking(BookingsModel bookingModel)
        {
            if (bookingModel == null) throw new ArgumentNullException(nameof(bookingModel));

            return _AddBooking(bookingModel);
        }

        private BookingsModel _AddBooking(BookingsModel bookingModel)
        {
            if (bookingModel == null) throw new ArgumentNullException(nameof(bookingModel));
            if (bookingModel.Notes.Length > MAXIMUM_NOTE_LENGTH) throw new ArgumentException($"Booking Notes {bookingModel.Notes} not valid for model {bookingModel}.");

            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"INSERT INTO bookings(vehicleid, clientid, notes, bookingdate) VALUES(" + bookingModel.VehicleId + "," + bookingModel.ClientId + ",'" + bookingModel.Notes + "','" + bookingModel.BookingDate + "'); SELECT CAST(lastval() AS integer)";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            int insertedID = Convert.ToInt32(cmd.ExecuteScalar());
            bookingModel.BookingId = insertedID;

            cmd.Dispose();
            conn.Close();

            return bookingModel;
        }

        public bool RemoveBooking(int id)
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"DELETE FROM bookings WHERE bookingid = " + id;
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn)
            {
                CommandType = CommandType.Text
            };
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            return true;
        }

        public void UpdateBookingDetails(string Id, string path, string value)
        {
            if (string.IsNullOrWhiteSpace(Id)) throw new ArgumentNullException(nameof(Id));
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            _UpdateBooking(Id, path, value);
        }

        private void _UpdateBooking(string id, string path, string value)
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"SELECT EXISTS(SELECT true FROM bookings WHERE bookingid = " + id + ")";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            bool findBooking = false;

            while (dr.Read())
            {
                string column = dr[0].ToString();
                findBooking = Convert.ToBoolean(dr[0]);
            }

            if (!findBooking) throw new ArgumentNullException($"No Booking was found that matched the specified Booking id ({id}).");

            cmd.Dispose();
            conn.Close();
            _SetBookingValue(id, path, value);
        }

        private void _SetBookingValue(string id, string path, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = "";
            switch (path.ToUpperInvariant())
            {
                case "NOTES":
                    if (value.Length > MAXIMUM_NOTE_LENGTH) throw new ArgumentException($"The length of the Note ({value}) can't exceed {MAXIMUM_NOTE_LENGTH} characters.");
                    sql = @"UPDATE bookings SET notes='" + value + "' WHERE bookingid = " + id;

                    break;
                case "BOOKINGDATE":
                    sql = @"UPDATE bookings SET bookingdate=" + Convert.ToDateTime(value) + " WHERE bookingid = " + id;

                    break;
                case "VEHICLEID":
                    sql = @"UPDATE bookings SET vehicleid=" + Convert.ToInt32(value) + " WHERE bookingid = " + id;

                    break;
                case "CLIENTID":
                    sql = @"UPDATE bookings SET clientid=" + Convert.ToInt32(value) + " WHERE bookingid = " + id;

                    break;
                default:
                    throw new ArgumentException($"Invalid path supplied. ({path.ToUpperInvariant()}).");
            }

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn)
            {
                CommandType = CommandType.Text
            };
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            conn.Close();
        }
    }
}
