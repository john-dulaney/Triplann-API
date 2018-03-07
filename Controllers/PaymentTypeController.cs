// PaymentType Controller Page:
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thoughtless_eels.Data;
using thoughtless_eels.Models;

// grab the correct namespace:
namespace thoughtless_eels.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    // tell .net that this is a controller and how to name the url:
    [Route("api/[controller]")]
    // Define the class
    public class PaymentTypeController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public PaymentTypeController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        // Request statement
        [HttpGet]
        public IActionResult Get()
        // GET 
        {
            // set specific DB entry to paymentType
            var paymentType = _context.PaymentType.ToList();
            // check if null, return 404 if true
            if (paymentType == null)
            {
                return NotFound();
            }
            return Ok(paymentType);
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
                PaymentType paymentType = _context.PaymentType.Single(c => c.PaymentTypeId == id);

                if (paymentType == null)
                {
                    // Return 404 if null
                    return NotFound();
                }

                return Ok(paymentType);
                // Catch statement return 404 for some reason
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]PaymentType paymentType)
        {
            // check to see if data matches the Model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // add PaymentType to the table
            _context.PaymentType.Add(paymentType);
            // Save the changes

            try
            {
                _context.SaveChanges();
                // Error statement
            }
            catch (DbUpdateException)
            {
                if (PaymentTypeExists(paymentType.PaymentTypeId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            // Return created PaymentType method:
            return CreatedAtRoute("GetSinglePayment", new { id = paymentType.PaymentTypeId }, paymentType);
        }
        // PUT Request:
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PaymentType paymentType)
        {
            // Check to see if the data matches the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check for id match, if true, update the table
            if (id != paymentType.PaymentTypeId)
            {
                // return 404
                return BadRequest();
            }
            // update table method
            _context.PaymentType.Update(paymentType);
            try
            {
                // save changes
                _context.SaveChanges();
            }
            // Error message for something .net
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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
            PaymentType paymentType = _context.PaymentType.Single(c => c.PaymentTypeId == id);

            if (paymentType == null)
            {
                return NotFound();
            }
            // removing the instance from the Datbase:
            _context.PaymentType.Remove(paymentType);
            // Save the Changes:
            _context.SaveChanges();
            return Ok(paymentType);
        }

        // Simple Boolean check to see if the Department even exists:
        private bool PaymentTypeExists(int paymentTypeId)
        {
            return _context.PaymentType.Any(c => c.PaymentTypeId == paymentTypeId);
        }
    }
}