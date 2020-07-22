using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceWorkshopAPI.Data.Contracts;
using ServiceWorkshopAPI.Data.Models;

namespace ServiceWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        protected readonly ILogger _logger;
        private readonly IVehiclesService _vehicleService;

        public VehicleController(ILogger<BookingController> logger, IVehiclesService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<List<VehiclesModel>> Get()
        {
            try
            {
                List<VehiclesModel> vehiclesModel = _vehicleService.GetVehicleDetails();
                return Ok(vehiclesModel);
            }
            catch (ArgumentNullException ane)
            {
                return BadRequest(ane.Message);
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(Get), ex);
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<VehiclesModel> Get(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return new StatusCodeResult((int)HttpStatusCode.BadRequest);
                }

                int.TryParse(id, out int Id);
                VehiclesModel vehiclesModel = _vehicleService.GetVehicleDetailsById(Id);
                return Ok(vehiclesModel);
            }
            catch (ArgumentNullException ane)
            {
                return BadRequest(ane.Message);
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(Get), ex);
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}