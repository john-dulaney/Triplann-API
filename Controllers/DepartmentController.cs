// Department Controller Page:

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

// grab the correct namespace:
namespace thoughtless_eels.Controllers
{

    [EnableCors("AllowSpecificOrigin")]
    // tell .net that this is a controller and how to name the url
    [Route("api/[controller]")]
    // Define the class
    public class DepartmentController : Controller
    {
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public DepartmentController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        // Request statement
        [HttpGet]
        public IActionResult Get()
        // GET 
        {
            // set specific DB entry to Department
            var Department = _context.Department.ToList();
            // check if null, return 404 if true
            if (Department == null)
            {
                return NotFound();
            }
            return Ok(Department);
        }

        // GET Request:

        [HttpGet("{id}", Name = "GetSingleDepartment")]
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
                Department department = _context.Department.Single(g => g.DepartmentId == id);

                if (department == null)
                {
                    // Return 404 if null
                    return NotFound();
                }

                return Ok(department);
                // Catch statement return 404 for some reason
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // Initiate the Post Request
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Department department)
        {  // check to see if data matches the Model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // add department to the table
            _context.Department.Add(department);
            // Save the changes
            try
            {
                _context.SaveChanges();
                // Error statement
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(department.DepartmentId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            // Return created Department method:
            return CreatedAtRoute("GetSingleDepartment", new { id = department.DepartmentId }, department);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Department department)
        {
            // Check to see if the data matches the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check for id match, if true, update the table
            if (id != department.DepartmentId)
            {
                // return 404
                return BadRequest();
            }
            // update table method
            _context.Department.Update(department);
           
            try
            {
                 // save changes
                _context.SaveChanges();
            }
            // Error message for something .net
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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
        // Simple Boolean check to see if the Department even exists:
        private bool DepartmentExists(int departmentId)
        {
            return _context.Department.Any(g => g.DepartmentId == departmentId);
        }

    }
}