using Npgsql;
using ServiceWorkshopAPI.Data.Contracts;
using ServiceWorkshopAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Services
{
    public class VehiclesService : IVehiclesService
    {
        private const string CONNECTIONSTRING = "Host=localhost;Port=5432;Username=postgres;Password=Mikan1967;Database=servicedb";
        private const int MAXIMUM_MODEL_LENGTH = 50;

        public VehiclesService()
        {
        }

        public List<VehiclesModel> GetVehicleDetails()
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"SELECT * FROM vehicles";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            var vehicles = new List<VehiclesModel>();

            while (dr.Read())
            {
                var vehicleModel = new VehiclesModel
                {
                    VehicleId = dr.GetInt32(0),
                    Model = dr.GetString(1),
                    CreatedOn = dr.GetDateTime(2)
                };
                vehicles.Add(vehicleModel);
            }

            conn.Close();
            return vehicles;
        }

        public VehiclesModel GetVehicleDetailsById(int id)
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"SELECT vehicleid, model FROM vehicles WHERE vehicleid = " + id;
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            var vehicle = new VehiclesModel();

            while (dr.Read())
            {
                var vehicleModel = new VehiclesModel
                {
                    VehicleId = dr.GetInt32(0),
                    Model = dr.GetString(1)
                };
                vehicle = vehicleModel;
            }

            conn.Close();
            return vehicle;
        }

        public List<VehiclesModel> GetFilteredVehicleSummariesByDateRange(int id, string startDate, string endDate)
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = "SELECT * FROM bookings WHERE (bookingdate >= cast('" + startDate + "' AS DATE) AND bookingdate <= cast('" + endDate + "' AS DATE)) && vehicleid = " + id;
            var cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            var vehicles = new List<VehiclesModel>();

            while (dr.Read())
            {
                var Vehicle = new VehiclesModel
                {
                    VehicleId = dr.GetInt32(0),
                    Model = dr.GetString(1)
                };

                vehicles.Add(Vehicle);
            }

            conn.Close();
            return vehicles;
        }

        public bool RemoveVehicle(int id)
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"DELETE FROM vehicles WHERE vehicleid = " + id;
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn)
            {
                CommandType = CommandType.Text
            };
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

            return true;
        }

        public void UpdateVehicleDetails(string Id, string path, string value)
        {
            if (string.IsNullOrWhiteSpace(Id)) throw new ArgumentNullException(nameof(Id));
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            _UpdateVehicle(Id, path, value);
        }

        private void _UpdateVehicle(string id, string path, string value)
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"SELECT EXISTS(SELECT true FROM vehicles WHERE vehicleid = " + id + ")";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            bool findVehicle = false;

            while (dr.Read())
            {
                string column = dr[0].ToString();
                findVehicle = Convert.ToBoolean(dr[0]);
            }

            if (!findVehicle) throw new ArgumentNullException($"No Vehicle was found that matched the specified Vehicle id ({id}).");

            cmd.Dispose();
            conn.Close();
            _SetVehicleValue(id, path, value);
        }

        private void _SetVehicleValue(string id, string path, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = "";
            switch (path.ToUpperInvariant())
            {
                case "MODEL":
                    if (value.Length > MAXIMUM_MODEL_LENGTH) throw new ArgumentException($"The length of the Model ({value}) can't exceed {MAXIMUM_MODEL_LENGTH} characters.");
                    sql = @"UPDATE vehicles SET model='" + value + "' WHERE vehicleid = " + id;

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
