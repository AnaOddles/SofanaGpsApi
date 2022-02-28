using SofanaGPSApi.Models;
using SofanaGPSApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SofanaGPSApi.AuthAttribute;

namespace SofanaGPSApi.Controllers
{
    [BasicAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly LocationService _locationService;

        //Constructor - Initializes the location service 
        public LocationsController(LocationService locationService)
        {
            _locationService = locationService;
        }

        //Gets all the locations using location service
        [HttpGet]
        public ActionResult<List<Location>> Get() =>
            _locationService.Get();

        //Gets all the last location using location service
        [HttpGet("{lastLocation}")]
        public ActionResult<Location> GetLast() =>
            _locationService.GetLast();

        //Gets all the locations for a cartId 
        [HttpGet("{cartId:int}" , Name ="GetLocationWithCartId")]
        public ActionResult <List<Location>> GetAllWithCartId(int cartId) =>
            _locationService.GetAllWithCartId(cartId);

        //Gets specific location with location id using location service
        [HttpGet("{id:length(24)}", Name = "GetLocation")]
        public ActionResult<Location> Get(string id)
        {
            var location = _locationService.Get(id);

            //No match throws NotFoundException
           if (location == null)
           {
                return NotFound();
            }

            return location;
        }

        //Passes location information to the location service to insert a new row to database
        [HttpPost]
        public ActionResult<Location> Create(Location location)
        {
            _locationService.Create(location);

            return CreatedAtRoute("GetLocation", new { id = location.Id.ToString() }, location);
        }

        //Passes location information with location id to the location service to update the specified location information
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Location locationIn)
        {
            var location = _locationService.Get(id);
            
            //No match throws NotFoundException
            if (location == null)
            {
                return NotFound();
            }

            _locationService.Update(id, locationIn);

            return NoContent();
        }

        //Passes the location id to the location service to remove the specified location row from database
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var location = _locationService.Get(id);

            //No match throws NotFoundException
            if (location == null)
            {
                return NotFound();
            }

            _locationService.Remove(location.Id);

            return NoContent();
        }
     }
   
}
