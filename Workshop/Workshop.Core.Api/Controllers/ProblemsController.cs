using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Api.Utils;
using Workshop.Domain.DataBase;
using Workshop.Domain.Models;

namespace Workshop.Core.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Problems")]
    public class ProblemsController : Controller
    {
        private readonly WorkshopContext _context;

        public ProblemsController(WorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Problems
        [HttpGet]
        public IEnumerable<WorkshopProblem> GetProblems()
        {
            return _context.Problems;
        }

        // GET: api/Problems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkshopProblem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workshopProblem = await _context.Problems.SingleOrDefaultAsync(m => m.Id == id);

            if (workshopProblem == null)
            {
                return NotFound();
            }

            return Ok(workshopProblem);
        }

        // PUT: api/Problems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshopProblem([FromRoute] int id, [FromBody] WorkshopProblem workshopProblem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workshopProblem.Id)
            {
                return BadRequest();
            }

            _context.Entry(workshopProblem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkshopProblemExists(id))
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

        // POST: api/Problems
        [HttpPost]
        public async Task<IActionResult> PostWorkshopProblem([FromBody] WorkshopProblem workshopProblem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Problems.Add(workshopProblem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkshopProblem", new { id = workshopProblem.Id }, workshopProblem);
        }

        // DELETE: api/Problems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshopProblem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workshopProblem = await _context.Problems.SingleOrDefaultAsync(m => m.Id == id);
            if (workshopProblem == null)
            {
                return NotFound();
            }

            await DeleteRestOfEntities(workshopProblem);

            _context.Problems.Remove(workshopProblem);
            await _context.SaveChangesAsync();

            return Ok(workshopProblem);
        }

        private bool WorkshopProblemExists(int id)
        {
            return _context.Problems.Any(e => e.Id == id);
        }

        private async Task DeleteRestOfEntities(WorkshopProblem problem)
        {
            var tasks = _context.Tasks.Where(t => t.ProblemId == problem.Id);
            if (tasks.Any())
            {
                foreach (var t in tasks)
                {
                    await DeleteUtils.DeleteTask(t, _context);
                }
            }
        }
    }
}