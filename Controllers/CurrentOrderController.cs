// Just a Note to Remember With Capitalization: 
// Parameters are lower case 
// class properties are Upper Case

using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thoughtless_eels.Data;
using thoughtless_eels.Models;


namespace thoughtless_eels.Controllers {
    // tell .net that this is a controller and how to name the url
    [EnableCors("AllowSpecificOrigin")]
    [Route ("api/[controller]")]
    // Define the class
    public class CurrentOrderController : Controller {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public CurrentOrderController (ApplicationDbContext ctx) {
            _context = ctx;
        }
        // Request statement
        [HttpGet]
        // GET
        public IActionResult Get () {
            // set specific DB entry to current order
            var currentOrder = _context.CurrentOrder.ToList ();
            // check if null, return 404 if true
            if (currentOrder == null) {
                return NotFound ();
            }
            return Ok (currentOrder);
        }
        // GET
        [HttpGet ("{id}", Name = "GetSingleOrder")]
        public IActionResult Get (int id) {
            var OrderDetail = _context.CurrentOrder
                .Where(co => co.CurrentOrderId == id)
                .Select(co => new {
                    CurrentOrderId = co.CurrentOrderId,
                    PaymentTypeId = co.PaymentTypeId,
                    Buyer = co.Customer.FirstName,
                    monkeyButt = co.ProductOrders.Select(po => po.Product)
                });

            // Check if the data matches the Model
            if (!ModelState.IsValid) {
                // check if null, return 404 if true
                return BadRequest (ModelState);
            }
            // Check DB to ensure referenced tables exist
            try {
                CurrentOrder currentOrder = _context.CurrentOrder.Single (c => c.CurrentOrderId == id);

                if (currentOrder == null) {
                    // Return 404 if null
                    return NotFound ();
                }

                return Ok (OrderDetail);
                // Catch statement return 404 for some reason
            } catch (System.InvalidOperationException ex) {
                return NotFound ();
            }
        }

        // POST
        [HttpPost]
        public IActionResult Post ([FromBody] CurrentOrder currentOrder) {
            // check to see if data matches the Model
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            // add order to the table
            _context.CurrentOrder.Add (currentOrder);
            // Save the changes
            try {
                _context.SaveChanges ();
                // Error statement
            } catch (DbUpdateException) {
                if (CurrentOrderExists (currentOrder.CurrentOrderId)) {
                    return new StatusCodeResult (StatusCodes.Status409Conflict);
                } else {
                    throw;
                }
            }
            // Return created order method
            return CreatedAtRoute ("GetSingleOrder", new { id = currentOrder.CurrentOrderId }, currentOrder);
        }

        // POST
        [HttpPost ("{id}")]
        public IActionResult Post (int id, [FromBody] ProductOrder productOrder) {
            // Check to see if the data matches the model
            if (!ModelState.IsValid) {
                // return 404
                return BadRequest (ModelState);
            }
            // Check DB to ensure referenced tables exist
            CurrentOrder currentOrder = _context.CurrentOrder.Single (co => co.CurrentOrderId == id);
            Product Product = _context.Product.Single (p => p.ProductId == productOrder.ProductId);

            // Return 404 if null
            if (currentOrder == null || Product == null) {
                return NotFound ();
            }
            // Add order to the table
            _context.ProductOrder.Add (productOrder);
            // Save the changes
            _context.SaveChanges ();
            // Create current order method
            return CreatedAtRoute ("GetSingleProduct", new { id = productOrder.CurrentOrderId });
        }

        // PUT
        [HttpPut ("{id}")]
        public IActionResult Put (int id, [FromBody] CurrentOrder currentOrder) {
            // Check to see if the data matches the model
            if (!ModelState.IsValid) {
                // return 404
                return BadRequest (ModelState);
            }
            // Check for id match, if true, update the table
            if (id != currentOrder.CurrentOrderId) {
                // return 404
                return BadRequest ();
            }
            // update table method
            _context.CurrentOrder.Update (currentOrder);
            // save changes
            try {
                _context.SaveChanges ();
            }
            // Error message for something .net
            catch (DbUpdateConcurrencyException) {
                if (!CurrentOrderExists (id)) {
                    return NotFound ();
                } else {
                    throw;
                }
            }

            return new StatusCodeResult (StatusCodes.Status204NoContent);
        }

        // DELETE
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            // Check if the data matches the Model
            CurrentOrder currentOrder = _context.CurrentOrder.Single (c => c.CurrentOrderId == id);
            // Return 404 if not found
            if (currentOrder == null) {
                return NotFound ();
            }
            // Remove method
            _context.CurrentOrder.Remove (currentOrder);
            // Save table after removal
            _context.SaveChanges ();
            return Ok (currentOrder);
        }

        // Simple Boolean check to see if the Order even exists.
        private bool CurrentOrderExists (int currentOrderId) {
            return _context.CurrentOrder.Any (c => c.CurrentOrderId == currentOrderId);
        }
    }
}

