using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Core.Api.Utils;
using Workshop.Domain.DataBase;
using Workshop.Domain.Models;

namespace Workshop.Core.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly WorkshopContext _context;

        public ValuesController()
        {
            _context = new WorkshopContext(new DbContextOptions<WorkshopContext>()); 
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
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            //////////
            var client = await _context.Clients.SingleOrDefaultAsync(m => m.Id == 2);
            if (client == null)
            {
                return new[] {""};
            }
            await DeleteRestOfEntities(client);

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            /// 
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
