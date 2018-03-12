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
    public class TripTypeController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public TripTypeController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        // Request statement
        [HttpGet]
        public IActionResult Get()
        // GET 
        {
            // set specific DB entry to TripType
            var TripType = _context.TripType.ToList();
            // check if null, return 404 if true
            if (TripType == null)
            {
                return NotFound();
            }
            return Ok(TripType);
        }


        // GET Request:
        [HttpGet("{id}", Name = "GetSinglePayment")]
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
                TripType TripType = _context.TripType.Single(c => c.TripTypeId == id);

                if (TripType == null)
                {
                    // Return 404 if null
                    return NotFound();
                }

                return Ok(TripType);
                // Catch statement return 404 for some reason
            }
            catch (System.InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]TripType TripType)
        {
            // check to see if data matches the Model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // add TripType to the table
            _context.TripType.Add(TripType);
            // Save the changes

            try
            {
                _context.SaveChanges();
                // Error statement
            }
            catch (DbUpdateException)
            {
                if (TripTypeExists(TripType.TripTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            // Return created TripType method:
            return CreatedAtRoute("GetSinglePayment", new { id = TripType.TripTypeId }, TripType);
        }
        // PUT Request:
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TripType TripType)
        {
            // Check to see if the data matches the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check for id match, if true, update the table
            if (id != TripType.TripTypeId)
            {
                // return 404
                return BadRequest();
            }
            // update table method
            _context.TripType.Update(TripType);
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
            TripType TripType = _context.TripType.Single(c => c.TripTypeId == id);

            if (TripType == null)
            {
                return NotFound();
            }
            // removing the instance from the Datbase:
            _context.TripType.Remove(TripType);
            // Save the Changes:
            _context.SaveChanges();
            return Ok(TripType);
        }

        // Simple Boolean check to see if the Department even exists:
        private bool TripTypeExists(int TripTypeId)
        {
            return _context.TripType.Any(c => c.TripTypeId == TripTypeId);
        }
    }
}