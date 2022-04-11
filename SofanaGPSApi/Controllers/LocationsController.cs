using SofanaGPSApi.Models;
using SofanaGPSApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SofanaGPSApi.AuthAttribute;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SofanaGPSApi.Controllers
{
    [BasicAuth]
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Exposes Locaiton Api endpoints
    /// </summary>
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationsController> _logger;

        /// <summary>
        /// Constructor - Initializes the location service
        /// </summary>
        /// <param name="locationService"></param>
        /// <param name="logger"></param>
        [ActivatorUtilitiesConstructor]
        public LocationsController(ILocationService locationService, ILogger<LocationsController> logger) {
            _locationService = locationService;
            _logger = logger;
        }

        /// <summary>
        /// Constructor - Initializes the location service
        /// Used for unit testing
        /// </summary>
        /// <param name="locationService"></param>
        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Gets all the locations using location service
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Get() {
            if(_logger != null)
                _logger.LogInformation("/locations - All Locations grabbed");

            var result = await _locationService.Get();
            if (result.Count == 0)
                return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Gets all the last location of both golf carts using location service
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet("{lastLocation}")]
        public async Task<IActionResult> GetLast() {

            //Use location service for both golf carts
            List<Location> locations = await _locationService.GetLastCoordinates();

            if (_logger != null) {
                string jsonObject = JsonConvert.SerializeObject(locations);
                _logger.LogInformation("/lastLocation - last locations grabbed - {0}", jsonObject);
            }

            if (locations.Count == 0)
                return NoContent();

            return Ok(locations);
        }

        /// <summary>
        /// Gets all the locations for a cartId 
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>IActionResult</returns>
        [HttpGet("cartId/{cartId:int}")]
        public async Task<IActionResult> GetAllWithCartId(int cartId) { 
            if(_logger != null)
                _logger.LogInformation("/cardId/:cartId - locations with {0}- cartId grabbed", cartId);
            List<Location> locations =  await _locationService.GetAllWithCartId(cartId);

            //No match throws NotFoundException
            if (locations == null)
            {
                _logger.LogInformation("/cardId/:cartId - no locations with {0}- cartId found", cartId);
                return NotFound();
            }

            return Ok(locations);
        }

        /// <summary>
        /// Gets specific location with location id using location service
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id:length(24)}", Name = "GetLocation")]
        public async Task<IActionResult> Get(string id)
        {
            if(_logger != null)
                _logger.LogInformation("/location/:id - location with {0}- locationId grabbed", id);

            var location = await _locationService.Get(id);

            //No match throws NotFoundException
           if (location == null)
           {
                _logger.LogInformation("/location/:id - no location with {0}- id grabbed", id);
                return NotFound();
           }

            return Ok(location);
        }

        /// <summary>
        /// Passes location information to the location service to insert a new row to database
        /// </summary>
        /// <param name="location"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Location location)
        {
            if (_logger != null) {
                string jsonObject = JsonConvert.SerializeObject(location);
                _logger.LogInformation("/location/ - adding location - {0}", jsonObject);
            }
            await _locationService.Create(location);
            return Ok(CreatedAtRoute("GetLocation", new { id = location.Id.ToString() }, location));
        }

        /// <summary>
        /// Passes location information with location id to the location service to update the specified location information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="locationIn"></param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Location locationIn)
        {
            _logger.LogInformation("/location/ - updating with id - {0}", id);
            var location = _locationService.Get(id);
            
            //No match throws NotFoundException
            if (location == null)
            {
                _logger.LogInformation("/location/:id - no location with {0}- id grabbed", id);
                return NotFound();
            }

            _locationService.Update(id, locationIn);
            return NoContent();
        }

        //Passes the location id to the location service to remove the specified location row from database
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            _logger.LogInformation("/location/ - deleting with id - {0}", id);
            var location = _locationService.Get(id);

            //No match throws NotFoundException
            if (location == null)
            {
                _logger.LogInformation("/location/:id - no location with {0}- id grabbed", id);
                return NotFound();
            }
            _locationService.Remove(location.Id);

            return NoContent();
        }
     }
}
