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
    public class EmployeeController : Controller {
        private bool EmployeeExists (int employeeId) {
            return _context.Employee.Any (g => g.EmployeeId == employeeId);
        }
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public EmployeeController (ApplicationDbContext ctx) {
            _context = ctx;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post ([FromBody] Employee employee) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            // add product to the table
            _context.Employee.Add (employee);
            // SAVE
            try {
                _context.SaveChanges ();
            }
            // return an error statement
            catch (DbUpdateException) {
                if (EmployeeExists (employee.EmployeeId)) {
                    return new StatusCodeResult (StatusCodes.Status409Conflict);
                } else {
                    throw;
                }
            }
            // return created Employee method
            return CreatedAtRoute ("GetSingleEmployee", new { id = employee.EmployeeId }, employee);
        }

        // Define overloaded API route for ComputerEmployee Join
        [HttpPost ("{id}/assignComputer", Name = "assignComputer")]
        public IActionResult Post (int id, [FromBody] EmployeeComputer employeeComputer) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                // return 404 if not matching
                return BadRequest (ModelState);
            }
            // Check the DB to ensure the tables Employee and Computer exist
            Employee employee = _context.Employee.Single (e => e.EmployeeId == id);
            Computer computer = _context.Computer.Single (c => c.ComputerId == employeeComputer.ComputerId);
            // return 404 if none found
            if (employee == null || computer == null) {
                return NotFound ();
            }

            _context.EmployeeComputer.Add (employeeComputer);
            _context.SaveChanges ();
            return CreatedAtRoute ("GetSingleProduct", new { id = employeeComputer.EmployeeId }, employee);
        }

        [HttpPost ("{id}/assignTraining")]
        public IActionResult Post (int id, [FromBody] EmployeeTraining employeeTraining) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                // return 404 if not matching
                return BadRequest (ModelState);
            }
            // Check the DB to ensure the tables Employee and Computer exist
            Employee employee = _context.Employee.Single (e => e.EmployeeId == id);
            TrainingProgram trainingProgram = _context.TrainingProgram.Single (tp => tp.TrainingProgramId == employeeTraining.TrainingProgramId);
            // return 404 if none found
            if (employee == null || trainingProgram == null) {
                return NotFound ();
            }
            // add join to the table
            _context.EmployeeTraining.Add (employeeTraining);
            // save the changes
            _context.SaveChanges ();
            // return Created Route method
            return CreatedAtRoute ("GetSingleProduct", new { id = employeeTraining.EmployeeTrainingId }, employee);
        }

        //GET
        [HttpGet]
        public IActionResult Get () {
            // set specific DB entry to products
            var employees = _context.Employee.ToList ();
            // check if null, return 404 if true
            if (employees == null) {
                return NotFound ();
            }
            return Ok (employees);
        }

        // GET api/values/5
        [HttpGet ("{id}", Name = "GetSingleEmployee")]
        public IActionResult Get (int id) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                // error msg
                return BadRequest (ModelState);
            }
            // Check DB to ensure referenced tables exist
            try {
                Employee employee = _context.Employee.Single (g => g.EmployeeId == id);
                // Return 404 if null
                if (employee == null) {
                    return NotFound ();
                }

                return Ok (employee);
            }
            // Catch statement return 404 for some reason
            catch (System.InvalidOperationException ex) {
                return NotFound ();
            }
        }

        [HttpPut ("{id}")]
        public IActionResult Put (int id, [FromBody] Employee employee) {
            // Check if data matches the Model 
            if (!ModelState.IsValid) {
                // return 404
                return BadRequest (ModelState);
            }

            // Check for id match, if true, update the table
            if (id != employee.EmployeeId) {
                // return 404
                return BadRequest ();
            }
            // update table method
            _context.Employee.Update (employee);
            // save changes
            try {
                _context.SaveChanges ();
            }
            // Error message for something .net
            catch (DbUpdateConcurrencyException) {
                if (!EmployeeExists (id)) {
                    return NotFound ();
                } else {
                    throw;
                }
            }

            return new StatusCodeResult (StatusCodes.Status204NoContent);
        }
    }
}