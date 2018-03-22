using System;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Triplann.Data;
using Triplann.Models;

// grab the correct namespace:
namespace Triplann.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    // tell .net that this is a controller and how to name the url:
    [Route("api/[controller]")]
    // Define the class
    public class TripController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public TripController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        // Request statement
        [HttpGet]
        public IActionResult Get()
        // GET 
        {
            // set specific DB entry to Trip
            var Trip = _context.Trip.ToList();
            // check if null, return 404 if true
            if (Trip == null)
            {
                return NotFound();
            }
            return Ok(Trip);
        }


        // GET Request:
        [HttpGet("{id}", Name = "GetSingleTrip")]
        public IActionResult Get(int id)
        {
            // Check if the data matches the Model
            if (!ModelState.IsValid)
            {
                // check if null, return 404 if true
                return BadRequest(ModelState);
            }
            // Check DB to ensure referenced tables exist
            try
            {
                Trip Trip = _context.Trip.Single(c => c.TripId == id);

                if (Trip == null)
                {
                    // Return 404 if null
                    return NotFound();
                }

                return Ok(Trip);
                // Catch statement return 404 for some reason
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Trip Trip)
        {
            ModelState.Remove("tripType");
            // check to see if data matches the Model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Trip.TripTypeId = Convert.ToInt32(Trip.TripTypeId);
            // add Trip to the table
            _context.Trip.Add(Trip);
            // Save the changes

            try
            {
                _context.SaveChanges();
                // Error statement
            }
            catch (DbUpdateException)
            {
                if (TripTypeExists(Trip.TripId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            // Return created Trip method:
            return CreatedAtRoute("GetSingleTrip", new { id = Trip.TripId }, Trip);
        }
        // PUT Request:
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Trip Trip)
        {
            // Check to see if the data matches the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check for id match, if true, update the table
            if (id != Trip.TripId)
            {
                // return 404
                return BadRequest();
            }
            // update table method
            _context.Trip.Update(Trip);
            try
            {
                // save changes
                _context.SaveChanges();
            }
            // Error message for something .net
            catch (DbUpdateConcurrencyException)
            {
                if (!TripTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        // DELETE Request:
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // locating the specific instance: 
            Trip Trip = _context.Trip.Single(c => c.TripId == id);

            if (Trip == null)
            {
                return NotFound();
            }
            // removing the instance from the Datbase:
            _context.Trip.Remove(Trip);
            // Save the Changes:
            _context.SaveChanges();
            return Ok(Trip);
        }

        // Simple Boolean check to see if the Department even exists:
        private bool TripTypeExists(int TripId)
        {
            return _context.Trip.Any(c => c.TripId == TripId);
        }
    }
}