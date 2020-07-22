using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceWorkshopAPI.Data.Contracts;
using ServiceWorkshopAPI.Data.Models;

namespace ServiceWorkshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class BookingController : ControllerBase
    {
        protected readonly ILogger _logger;
        private readonly IBookingsService _bookingsService;

        private string STARTDATE = DateTime.Today.ToShortDateString();
        private string ENDDATE = DateTime.Today.AddDays(1).ToShortDateString();

        public BookingController(ILogger<BookingController> logger, IBookingsService bookingsService)
        {
            _logger = logger;
            _bookingsService = bookingsService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<List<BookingsModel>> Get()
        {
            try
            {
                List<BookingsModel> bookingsModel = _bookingsService.GetBookingDetails();
                return Ok(bookingsModel);
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
        public ActionResult<BookingsModel> Get(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return new StatusCodeResult((int)HttpStatusCode.BadRequest);
                }

                int.TryParse(id, out int Id);
                BookingsModel bookingsModel = _bookingsService.GetBookingDetailsById(Id);
                return Ok(bookingsModel);
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

        [HttpGet("{startDate}/{endDate}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<List<BookingsModel>> Get(string startDate, string endDate)
        {
            try
            {
                if (startDate == "{startDate}" && endDate == "{endDate}")
                {
                    startDate = STARTDATE;
                    endDate = ENDDATE;
                }

                List<BookingsModel> bookingsModel = _bookingsService.GetFilteredBookingSummariesByDateRange(startDate, endDate);
                return Ok(bookingsModel);
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

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<BookingsModel> Patch(string id, PatchModel model)
        {
            try
            {
                _bookingsService.UpdateBookingDetails(id, model.Path, model.Value);
                return NoContent();
            }
            catch (ArgumentNullException anEx)
            {
                _logger.LogError(anEx, $"The specified id is null or has no value.");
                return BadRequest($"The specified id is null or has no value. See log for details.");
            }
            catch (ArgumentException arg)
            {
                _logger.LogError(arg, $"The specified path is not valid.");
                return BadRequest(arg.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred attempting to patch a BookingsModel. {model} for booking {id}.");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<BookingsModel> Post([FromBody] BookingsModel model)
        {
            try
            {
                BookingsModel newBooking = _bookingsService.AddBooking(model);
                return Created(string.Empty, newBooking);
            }
            catch (ArgumentNullException ane)
            {
                _logger.LogError(ane, $"Unable to create Model: {model}");
                return BadRequest($"Unable to create the booking model: {model}");
            }
            catch (ArgumentException arg)
            {
                _logger.LogError(arg, $"Unabled to add the booking.");
                return BadRequest($"Unabled to add the booking. See log for details.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred trying to create the booking");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}