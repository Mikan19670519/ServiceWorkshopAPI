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
    public class ClientController : ControllerBase
    {
        protected readonly ILogger _logger;
        private readonly IClientsService _clientService;

        public ClientController(ILogger<ClientController> logger, IClientsService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<List<ClientsModel>> Get()
        {
            try
            {
                List<ClientsModel> clientsModel = _clientService.GetClientDetails();
                return Ok(clientsModel);
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