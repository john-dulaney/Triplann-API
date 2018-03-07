using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thoughtless_eels.Data;
using thoughtless_eels.Models;

namespace thoughtless_eels.Controllers {
    [EnableCors ("AllowSpecificOrigin")]
    [Route ("api/[controller]")]
    public class CustomerController : Controller {
        // create a function that returns the customer data
        // corresponding to the id passed in
        private bool CustomerExists (int customerId) {
            return _context.Customer.Any (g => g.CustomerId == customerId);
        }
        // capture an instance of application db context
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public CustomerController (ApplicationDbContext ctx) {
            _context = ctx;
        }

        // POST api/values
        [HttpPost]
        // pass in a customer 
        public IActionResult Post ([FromBody] Customer customer) {
            // check that the input matches the strucure of our model
            if (!ModelState.IsValid) {
                // throw an error if not
                return BadRequest (ModelState);
            }

            // add customer to the db
            _context.Customer.Add (customer);

            // attempt to save the changes and throw an error if any 
            // errors occur
            try {
                _context.SaveChanges ();
            } catch (DbUpdateException) {
                if (CustomerExists (customer.CustomerId)) {
                    return new StatusCodeResult (StatusCodes.Status409Conflict);
                } else {
                    throw;
                }
            }
            // return a success message
            return CreatedAtRoute ("GetSingleCustomer", new { id = customer.CustomerId }, customer);
        }

        [HttpGet ("getCustomers")]
        public IActionResult Get () {
            var customers = _context.Customer.ToList ();
            if (customers == null) {
                return NotFound ();
            }
            return Ok (customers);
        }

        // GET api/values/5
        [HttpGet ("{id}", Name = "GetSingleCustomer")]
        public IActionResult Get (int id) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            try {
                Customer customer = _context.Customer.Single (g => g.CustomerId == id);

                if (customer == null) {
                    return NotFound ();
                }

                return Ok (customer);
            } catch (System.InvalidOperationException ex) {
                return NotFound ();
            }
        }

        [HttpPut ("{id}")]
        public IActionResult Put (int id, [FromBody] Customer customer) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            if (id != customer.CustomerId) {
                return BadRequest ();
            }
            _context.Customer.Update (customer);
            try {
                _context.SaveChanges ();
            } catch (DbUpdateConcurrencyException) {
                if (!CustomerExists (id)) {
                    return NotFound ();
                } else {
                    throw;
                }
            }

            return new StatusCodeResult (StatusCodes.Status204NoContent);
        }

        [HttpGet]
        //{flag:bool}????
        public IActionResult Get (bool? ActiveOrder) {

          if (ActiveOrder == false) {
                var customer = _context.Customer.Where (c =>
                    !_context.CurrentOrder.Any (o => o.CustomerId == c.CustomerId));

                return Ok (customer);

            } else {
                return NotFound ();
            }
        }
    }

}