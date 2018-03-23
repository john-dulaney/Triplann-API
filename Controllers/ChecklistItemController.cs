using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Triplann.Data;
using Triplann.Models;

namespace Triplann.Controllers {
    [EnableCors ("AllowSpecificOrigin")]
    [Route ("api/[controller]")]

    public class ChecklistItemController : Controller {
        private ApplicationDbContext _context;
        public ChecklistItemController (ApplicationDbContext ctx) {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Get () {
            // set specific DB entry to ChecklistItems
            var ChecklistItems = _context.ChecklistItem.ToList ();
            // check if null, return 404 if true
            if (ChecklistItems == null) {
                return NotFound ();
            }
            return Ok (ChecklistItems);
        }

        // GET api/ChecklistItem/5
        [HttpGet ("{id}", Name = "GetSingleChecklistItem")]
        public IActionResult Get (int id) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                // error msg
                return BadRequest (ModelState);
            }
            // Check DB to ensure referenced tables exist
            try {
                ChecklistItem ChecklistItem = _context.ChecklistItem.Single (g => g.ChecklistItemId == id);
                // Return 404 if null
                if (ChecklistItem == null) {
                    return NotFound ();
                }

                return Ok (ChecklistItem);
            }
            // Catch statement return 404 for some reason
            catch (System.InvalidOperationException)
            {
                return NotFound ();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post (string ChecklistAction, int tripTypeId) {
            // var requestBody = new System.IO.StreamReader(HttpContext.Request.Body).ReadToEnd();
            var ChecklistItem = new ChecklistItem {
                ChecklistAction = ChecklistAction,
                TripTypeId = tripTypeId
            };
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            // add ChecklistItem to the table
            _context.ChecklistItem.Add (ChecklistItem);
            // SAVE
            try {
                _context.SaveChanges ();
            }
            // return an error statement
            catch (DbUpdateException) {
                if (ChecklistItemExists (ChecklistItem.ChecklistItemId)) {
                    return new StatusCodeResult (StatusCodes.Status409Conflict);
                } else {
                    throw;
                }
            }
            // return created ChecklistItem method
            return CreatedAtRoute ("GetSingleChecklistItem", new { id = ChecklistItem.ChecklistItemId }, ChecklistItem);
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public IActionResult Put (int id, [FromBody] ChecklistItem ChecklistItem) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                // return 404
                return BadRequest (ModelState);
            }

            // Check for id match, if true, update the table
            if (id != ChecklistItem.ChecklistItemId) {
                // return 404
                return BadRequest ();
            }
            // update table method
            _context.ChecklistItem.Update (ChecklistItem);
            // save changes
            try {
                _context.SaveChanges ();
            }
            // Error message for something .net
            catch (DbUpdateConcurrencyException) {
                if (!ChecklistItemExists (id)) {
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
            ChecklistItem ChecklistItem = _context.ChecklistItem.Single (g => g.ChecklistItemId == id);
            // Return 404 if not found
            if (ChecklistItem == null) {
                return NotFound ();
            }
            // Remove method
            _context.ChecklistItem.Remove (ChecklistItem);
            // Save table after removal
            _context.SaveChanges ();
            return Ok (ChecklistItem);
        }

        // Simple Boolean check to see if ChecklistItem Even exists.
        private bool ChecklistItemExists (int ChecklistItemId) {
            return _context.ChecklistItem.Any (g => g.ChecklistItemId == ChecklistItemId);
        }

    }
}