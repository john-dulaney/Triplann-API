using System;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thoughtless_eels.Data;
using thoughtless_eels.Models;

namespace thoughtless_eels.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class TrainingProgramController : Controller
    {    
        private ApplicationDbContext _context;
        // Constructor method to create an instance of context to communicate with our database.
        public TrainingProgramController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        // http request to get a list of training programs
        [HttpGet]
        public IActionResult Get()
        {
            //store the training program tables as a list in a variable
            var trainingProgram = _context.TrainingProgram.ToList();
            // if there are no training programs, return error
            if (trainingProgram == null)
            {
                return NotFound();
            }
            // return 201 and training program list upon succesful get
            return Ok(trainingProgram);
        }

        //http request to get a single training program
        [HttpGet("{id}", Name = "GetSingleTrainingProgram")]
        public IActionResult Get(int id)
        {
            // verify the JSON object matches the model state
            if (!ModelState.IsValid)
            {
                // if not return a 400 error
                return BadRequest(ModelState);
            }

            // check and see if the training program requested exists, 
            // if no return an error, if it does then return the
            // training program
            try
            {
                TrainingProgram trainingProgram = _context.TrainingProgram.Single(c => c.TrainingProgramId == id);

                if (trainingProgram == null)
                {
                    return NotFound();
                }

                return Ok(trainingProgram);
            }
            // return an error if the system throws an invalid operation exception
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // http method to create a new row in the training program table
        [HttpPost]
        public IActionResult Post([FromBody]TrainingProgram trainingProgram)
        {
            // check that the structure of the JSON object matches
            // the model
            if (!ModelState.IsValid)
            {
                // if not return an error
                return BadRequest(ModelState);
            }

            //add new entry to database
            _context.TrainingProgram.Add(trainingProgram);
            
            //save changes to db and catch system errors
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TrainingProgramExists(trainingProgram.TrainingProgramId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("GetSingleTrainingProgram", new { id = trainingProgram.TrainingProgramId }, trainingProgram);
        }

        // http method to update databse
        [HttpPut("{id}")]
        // pass in the id of the program to be updated
        public IActionResult Put(int id, [FromBody]TrainingProgram trainingProgram)
        {   
            // check that the JSON structure matches the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // if the id passed in differs from the program Id, throw an error
            if (id != trainingProgram.TrainingProgramId)
            {
                return BadRequest();
            }

            // update the specified training program
            _context.TrainingProgram.Update(trainingProgram);
            try
            {
                _context.SaveChanges();
            }
            // look for DbConcurrencyException
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingProgramExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // return a message for a successful update
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        //http method to delete a training program
        [HttpDelete("{id}")]
        // pass in the id of the training program to be deleted
        public IActionResult Delete(int id)
        {
            // capture the db entry for training program in a variable
            TrainingProgram trainingProgram = _context.TrainingProgram.Single(c => c.TrainingProgramId == id);

            // get todays date in a DateTime 
            DateTime today = DateTime.Today;
            // get the DateTime of the starting date of the program
            DateTime startDate = trainingProgram.StartDate;
            
            // if the given training program exists
            if (trainingProgram != null)
            {
                //compare start date with today for an integer result
                int results = DateTime.Compare(today, startDate);
                // if start date is in the future, execute logic to
                // create new database entry
                if(results < 0) {
                    _context.TrainingProgram.Remove(trainingProgram);
                    _context.SaveChanges();
                    return Ok(trainingProgram);
                } else {
                // if not, throw an error
                    return BadRequest("Training Program has already started. Cannot complete request");
                }
            } else {
                // throw an error if the training program is not found
                return NotFound();
            }
        }

        private bool TrainingProgramExists(int trainingProgramId)
        {
            return _context.TrainingProgram.Any(c => c.TrainingProgramId == trainingProgramId);
        }
    }
}