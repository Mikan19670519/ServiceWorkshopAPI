using Npgsql;
using ServiceWorkshopAPI.Data.Contracts;
using ServiceWorkshopAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Services
{
    public class ClientsService : IClientsService
    {
        private const string CONNECTIONSTRING = "Host=localhost;Port=5432;Username=postgres;Password=Mikan1967;Database=servicedb";

        public List<ClientsModel> GetClientDetails()
        {
            string cs = CONNECTIONSTRING;
            var conn = new NpgsqlConnection(cs);
            conn.Open();

            string sql = @"SELECT * FROM clients";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            NpgsqlDataReader dr = cmd.ExecuteReader();
            var clients = new List<ClientsModel>();

            while (dr.Read())
            {
                var clientModel = new ClientsModel
                {
                    ClientId = dr.GetInt32(0),
                    FirstName = dr.GetString(1)
                };
                clients.Add(clientModel);
            }

            conn.Close();
            return clients;
        }
    }
}
