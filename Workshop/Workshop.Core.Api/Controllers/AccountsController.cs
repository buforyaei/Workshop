using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Api.Utils;
using Workshop.Domain.DataBase;

namespace Workshop.Core.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountsController : Controller
    {
        private readonly WorkshopContext _db;

        public AccountsController()
        {
            _db = new WorkshopContext(new DbContextOptions<WorkshopContext>());
        }

        // Login -> POST: api/Accounts
        [HttpPost]
        public IActionResult Post([FromBody]string[] parameters)
        {
            if (parameters?.Length != 2) return BadRequest();
            var users = _db.Employees.Where(u => u.Usersame == parameters[0]);
            if (!users.Any()) return Unauthorized();
            var credentials = _db.UsersAccounts.Where(c => c.EmployeeId == users.First().Id);
            if (!credentials.Any()) return Unauthorized();
            if (AccountUtils.Decrytp(credentials.First().Password) == AccountUtils.Decrytp(parameters[1]))
            {
                return Ok(users.First());
            }
            return Unauthorized();
        }

        //Change Password -> PUT: api/Accounts/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] string newPassword)
        {
            var employee = await _db.Employees.SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            var account = await _db.UsersAccounts.SingleOrDefaultAsync(u => u.EmployeeId == id);
            if (account == null)
            {
                return NotFound();
            }

            account.Password = newPassword;
            _db.Entry(account).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        private bool EmployeeExists(int id)
        {
            return _db.Employees.Any(e => e.Id == id);
        }
    }
}
