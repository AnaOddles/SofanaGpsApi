using SofanaGPSApi.Models;
using SofanaGPSApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SofanaGPSApi.AuthAttribute;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SofanaGPSApi.Controllers
{
    [BasicAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly LocationService _locationService;
        private readonly ILogger<LocationsController> _logger;

        //Constructor - Initializes the location service 
        public LocationsController(LocationService locationService, ILogger<LocationsController> logger) {
            _locationService = locationService;
            _logger = logger;
        }

        //Gets all the locations using location service
        [HttpGet]
        public ActionResult<List<Location>> Get() {
            _logger.LogInformation("/locations - All Locations grabbed");
            return _locationService.Get();
        }
            
        //Gets all the last location of both golf carts using location service
        [HttpGet("{lastLocation}")]
        public ActionResult<List<Location>>GetLast() {
            List<Location> locations = new List<Location>();

            //Use location service for both golf carts
            locations.Add(_locationService.GetLastWithCartId(0));
            locations.Add(_locationService.GetLastWithCartId(1));

            string jsonObject = JsonConvert.SerializeObject(locations);
            _logger.LogInformation("/lastLocation - last locations grabbed - {0}", jsonObject);

            return locations;
        }

        //Gets all the locations for a cartId 
        [HttpGet("cartId/{cartId:int}")]
        public ActionResult<List<Location>> GetAllWithCartId(int cartId) {
            _logger.LogInformation("/cardId/:cartId - locations with {0}- cartId grabbed", cartId);
            List<Location> locations =  _locationService.GetAllWithCartId(cartId);

            //No match throws NotFoundException
            if (locations == null)
            {
                _logger.LogInformation("/cardId/:cartId - no locations with {0}- cartId found", cartId);
                return NotFound();
            }

            return locations;
        }
             
        //Gets specific location with location id using location service
        [HttpGet("{id:length(24)}", Name = "GetLocation")]
        public ActionResult<Location> Get(string id)
        {
            _logger.LogInformation("/location/:id - location with {0}- locationId grabbed", id);
            var location = _locationService.Get(id);

            //No match throws NotFoundException
           if (location == null)
           {
                _logger.LogInformation("/location/:id - no location with {0}- id grabbed", id);
                return NotFound();
           }

            return location;
        }

        //Passes location information to the location service to insert a new row to database
        [HttpPost]
        public ActionResult<Location> Create(Location location)
        {
            string jsonObject = JsonConvert.SerializeObject(location);
            _logger.LogInformation("/location/ - adding location - {0}", jsonObject);
            _locationService.Create(location);
            return CreatedAtRoute("GetLocation", new { id = location.Id.ToString() }, location);
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
