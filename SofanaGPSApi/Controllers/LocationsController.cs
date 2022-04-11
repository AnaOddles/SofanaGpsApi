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
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationsController> _logger;

        //Constructor - Initializes the location service
        [ActivatorUtilitiesConstructor]
        public LocationsController(ILocationService locationService, ILogger<LocationsController> logger) {
            _locationService = locationService;
            _logger = logger;
        }

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        //Gets all the locations using location service
        [HttpGet]
        public async Task<IActionResult> Get() {
            if(_logger != null)
                _logger.LogInformation("/locations - All Locations grabbed");

            var result = await _locationService.Get();
            if (result.Count == 0)
                return NoContent();
            return Ok(result);
        }
            
        //Gets all the last location of both golf carts using location service
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

        //Gets all the locations for a cartId 
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
             
        //Gets specific location with location id using location service
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

        //Passes location information to the location service to insert a new row to database
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

        //Passes location information with location id to the location service to update the specified location information
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
