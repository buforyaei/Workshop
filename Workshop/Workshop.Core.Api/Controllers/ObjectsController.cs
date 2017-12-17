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
    [Route("api/Objects")]
    public class ObjectsController : Controller
    {
        private readonly WorkshopContext _context;

        public ObjectsController(WorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Objects
        [HttpGet]
        public IEnumerable<WorkshopObject> GetObjects()
        {
            return _context.Objects;
        }

        // GET: api/Objects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkshopObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workshopObject = await _context.Objects.SingleOrDefaultAsync(m => m.Id == id);

            if (workshopObject == null)
            {
                return NotFound();
            }

            return Ok(workshopObject);
        }

        // PUT: api/Objects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshopObject([FromRoute] int id, [FromBody] WorkshopObject workshopObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workshopObject.Id)
            {
                return BadRequest();
            }

            _context.Entry(workshopObject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkshopObjectExists(id))
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

        // POST: api/Objects
        [HttpPost]
        public async Task<IActionResult> PostWorkshopObject([FromBody] WorkshopObject workshopObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Objects.Add(workshopObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkshopObject", new { id = workshopObject.Id }, workshopObject);
        }

        // DELETE: api/Objects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkshopObject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workshopObject = await _context.Objects.SingleOrDefaultAsync(m => m.Id == id);
            if (workshopObject == null)
            {
                return NotFound();
            }

            await DeleteRestOfEntities(workshopObject);

            _context.Objects.Remove(workshopObject);
            await _context.SaveChangesAsync();

            return Ok(workshopObject);
        }

        private bool WorkshopObjectExists(int id)
        {
            return _context.Objects.Any(e => e.Id == id);
        }

        private async Task DeleteRestOfEntities(WorkshopObject obj)
        {
            var problems = _context.Problems.Where(p => p.ObjectId == obj.Id);
            if (problems.Any())
            {
                foreach (var problem in problems)
                {
                    var tasks = _context.Tasks.Where(t => t.ProblemId == problem.Id);
                    if (tasks.Any())
                    {
                        foreach (var t in tasks)
                        {
                            await DeleteUtils.DeleteTask(t, _context);
                        }
                    }
                    await DeleteUtils.DeleteProblem(problem, _context);
                }
            }
        }
    }
}