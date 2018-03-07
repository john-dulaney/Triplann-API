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
    // tell .net that this is a controller and how to name the url
    [EnableCors("AllowSpecificOrigin")]
    [Route ("api/[controller]")]
    // Define class
    public class ProductController : Controller {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ProductController (ApplicationDbContext ctx) {
            _context = ctx;
        }
        // Request statement
        [HttpGet]
        // GET
        public IActionResult Get () {
            // set specific DB entry to products
            var products = _context.Product.ToList ();
            // check if null, return 404 if true
            if (products == null) {
                return NotFound ();
            }
            return Ok (products);
        }

        // GET api/Product/5
        [HttpGet ("{id}", Name = "GetSingleProduct")]
        public IActionResult Get (int id) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                // error msg
                return BadRequest (ModelState);
            }
            // Check DB to ensure referenced tables exist
            try {
                Product product = _context.Product.Single (g => g.ProductId == id);
                // Return 404 if null
                if (product == null) {
                    return NotFound ();
                }

                return Ok (product);
            }
            // Catch statement return 404 for some reason
            catch (System.InvalidOperationException ex) {
                return NotFound ();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post ([FromBody] Product product) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            // add product to the table
            _context.Product.Add (product);
            // SAVE
            try {
                _context.SaveChanges ();
            }
            // return an error statement
            catch (DbUpdateException) {
                if (ProductExists (product.ProductId)) {
                    return new StatusCodeResult (StatusCodes.Status409Conflict);
                } else {
                    throw;
                }
            }
            // return created Product method
            return CreatedAtRoute ("GetSingleProduct", new { id = product.ProductId }, product);
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public IActionResult Put (int id, [FromBody] Product product) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                // return 404
                return BadRequest (ModelState);
            }

            // Check for id match, if true, update the table
            if (id != product.ProductId) {
                // return 404
                return BadRequest ();
            }
            // update table method
            _context.Product.Update (product);
            // save changes
            try {
                _context.SaveChanges ();
            }
            // Error message for something .net
            catch (DbUpdateConcurrencyException) {
                if (!ProductExists (id)) {
                    return NotFound ();
                } else {
                    throw;
                }
            }

            return new StatusCodeResult (StatusCodes.Status204NoContent);
        }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            // Check if data matches the Model 
            Product product = _context.Product.Single (g => g.ProductId == id);
            // Return 404 if not found
            if (product == null) {
                return NotFound ();
            }
            // Remove method
            _context.Product.Remove (product);
            // Save table after removal
            _context.SaveChanges ();
            return Ok (product);
        }

        // Simple Boolean check to see if Product Even exists.
        private bool ProductExists (int productId) {
            return _context.Product.Any (g => g.ProductId == productId);
        }

    }
}