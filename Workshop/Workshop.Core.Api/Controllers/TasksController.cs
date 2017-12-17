using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.Domain.DataBase;
using Workshop.Domain.Models;

namespace Workshop.Core.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        private readonly WorkshopContext _context;

        public TasksController(WorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<WorkshopTask> GetTasks()
        {
            return _context.Tasks;
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkshopTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workshopTask = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);

            if (workshopTask == null)
            {
                return NotFound();
            }

            return Ok(workshopTask);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshopTask([FromRoute] int id, [FromBody] WorkshopTask workshopTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workshopTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(workshopTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkshopTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> PostWorkshopTask([FromBody] WorkshopTask workshopTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tasks.Add(workshopTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkshopTask", new { id = workshopTask.Id }, workshopTask);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshopTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workshopTask = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            if (workshopTask == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(workshopTask);
            await _context.SaveChangesAsync();

            return Ok(workshopTask);
        }

        private bool WorkshopTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}