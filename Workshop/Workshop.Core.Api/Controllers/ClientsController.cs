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
    [Route("api/Clients")]
    public class ClientsController : Controller
    {
        private readonly WorkshopContext _context;

        public ClientsController(WorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public IEnumerable<Client> GetClients()
        {
            return _context.Clients;
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient([FromRoute] int id, [FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        [HttpPost]
        public async Task<IActionResult> PostClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            await DeleteRestOfEntities(client);

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok(client);
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }

        private async Task DeleteRestOfEntities(Client client)
        {
            var objects = _context.Objects.Where(o => o.ClientId == client.Id);
            if (objects.Any())
            {
                foreach (var obj in objects)
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
                    await DeleteUtils.DeleteObject(obj, _context);
                }
            }  
        }
    }
}