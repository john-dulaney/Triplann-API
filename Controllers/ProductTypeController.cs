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

namespace thoughtless_eels.Controllers
// tell .net that this is a controller and how to name the url
{
    [EnableCors("AllowSpecificOrigin")]
    [Route ("api/[controller]")]
    // Define the class
    public class ProductTypeController : Controller {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ProductTypeController (ApplicationDbContext ctx) {
            _context = ctx;
        }
        // Request Statement
        [HttpGet]
        // GET
        public IActionResult Get () {
            // set specific DB entry to Product Type
            var productTypes = _context.ProductType.ToList ();
            // check if null, return 404 if true
            if (productTypes == null) {
                return NotFound ();
            }
            return Ok (productTypes);
        }

        // GET api/productType/5
        [HttpGet ("{id}", Name = "GetSingleProductType")]
        public IActionResult Get (int id) {
            // Check if the data matches the model
            if (!ModelState.IsValid) {
                // check if null, return 404 if true
                return BadRequest (ModelState);
            }
            // Check DB to ensure referenced tables exist
            try {
                ProductType productType = _context.ProductType.Single (g => g.ProductTypeId == id);

                if (productType == null) {
                    // return 404 if null
                    return NotFound ();
                }

                return Ok (productType);
                // catch statement return 404 for some reason
            } catch (System.InvalidOperationException ex) {
                return NotFound ();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post ([FromBody] ProductType productType) {
            // check to see if data matches the model
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            // add product type to the table
            _context.ProductType.Add (productType);
            // save the changes
            try {
                _context.SaveChanges ();
                //error statement
            } catch (DbUpdateException) {
                if (ProductTypeExists (productType.ProductTypeId)) {
                    return new StatusCodeResult (StatusCodes.Status409Conflict);
                } else {
                    throw;
                }
            }
            // Return Created product type method
            return CreatedAtRoute ("GetSingleProductType", new { id = productType.ProductTypeId }, productType);
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public IActionResult Put (int id, [FromBody] ProductType productType) {
            // check to see if the data matches themodel
            if (!ModelState.IsValid) {
                // return 404 if error
                return BadRequest (ModelState);
            }
            // Check for id match, if true, update the table
            if (id != productType.ProductTypeId) {
                //return 404 if error
                return BadRequest ();
            }
            // update table method
            _context.ProductType.Update (productType);
            // save the changes
            try {
                _context.SaveChanges ();
            }
            // Error message for something .net
            catch (DbUpdateConcurrencyException) {
                if (!ProductTypeExists (id)) {
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
            // Check if the data matches the Model
            ProductType productType = _context.ProductType.Single (g => g.ProductTypeId == id);
                        // Return 404 if not found
            if (productType == null) {
                return NotFound ();
            }
            // Remove method
            _context.ProductType.Remove (productType);
            // Save table after removal
            _context.SaveChanges ();
            return Ok (productType);
        }

        // Simple Boolean check to see if the productType even exists.
        private bool ProductTypeExists (int productTypeId) {
            return _context.ProductType.Any (g => g.ProductTypeId == productTypeId);
        }

    }
}