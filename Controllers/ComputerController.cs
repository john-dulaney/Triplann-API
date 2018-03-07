using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thoughtless_eels.Data;
using thoughtless_eels.Models;

namespace thoughtless_eels.Controllers
{
    // tell .net that this is a controller and how to name the url
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    // define class
    public class ComputerController : Controller
    {    
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public ComputerController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        // Request statement
        [HttpGet]
        //GET Method
        public IActionResult Get()
        {
            // store all table info in variable, return variable. if table is empty, throw error
            var computer = _context.Computer.ToList();
            if (computer == null)
            {
                return NotFound();
            }
            return Ok(computer);
        }
        // GET single item Method, takes id of single item being requested as a parameter
        [HttpGet("{id}", Name = "GetSingleComputer")]
        public IActionResult Get(int id)
        {
            //check format of request, if bad, throw error
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check DB to ensure referenced tables exist
            try
            {
                Computer computer = _context.Computer.Single(c => c.ComputerId == id);
                //throw error if computerId that is requested deosn't exist
                if (computer == null)
                {
                    return NotFound();
                }

                return Ok(computer);
            }
            //throw invalid operation exception error
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        //POST Method
        public IActionResult Post([FromBody]Computer computer)
        {
            //check format of request, if bad, throw error
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // add to database table
            _context.Computer.Add(computer);
            // try to save save changes, catch duplicate errors
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ComputerExists(computer.ComputerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            // return what was just added to the db
            return CreatedAtRoute("GetSingleComputer", new { id = computer.ComputerId }, computer);
        }

        [HttpPut("{id}")]
        // PUT Method, takes id of item to be updated as a parameter
        public IActionResult Put(int id, [FromBody]Computer computer)
        {
            //check format of request, if bad, throw error
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // throw error if the id in JSON object doesn't match the id passed in the route
            if (id != computer.ComputerId)
            {
                return BadRequest();
            }
            // update table
            _context.Computer.Update(computer);
            //try to save the updated changes throw error if the 
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
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

        [HttpDelete("{id}")]
        // DELETE Method, takes id of item to be deleted as a parameter
        public IActionResult Delete(int id)
        {
            Computer computer = _context.Computer.Single(c => c.ComputerId == id);

            if (computer == null)
            {
                return NotFound();
            }
            _context.Computer.Remove(computer);
            _context.SaveChanges();
            return Ok(computer);
        }

        private bool ComputerExists(int computerId)
        {
            return _context.Computer.Any(c => c.ComputerId == computerId);
        }
    }
}