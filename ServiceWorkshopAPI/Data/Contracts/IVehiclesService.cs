using ServiceWorkshopAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Contracts
{
    public interface IVehiclesService
    {
        List<VehiclesModel> GetVehicleDetails();
        VehiclesModel GetVehicleDetailsById(int id);
        List<VehiclesModel> GetFilteredVehicleSummariesByDateRange(int id, string startDate, string endDate);
        void UpdateVehicleDetails(string Id, string path, string value);
        bool RemoveVehicle(int id);
    }
}
