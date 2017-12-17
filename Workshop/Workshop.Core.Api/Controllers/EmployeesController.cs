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
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private readonly WorkshopContext _context;

        public EmployeesController(WorkshopContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToArray();
        }

        // For now NOT USED GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // Edit employee -> PUT: api/Employees/5  to change password look Accounts PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/Employees    format as string table -> "password", "username", "Role", "Phonenumber"
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] IEnumerable<string> data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newUserData = data as object[] ?? data.ToArray();
            if (!newUserData.Any() || newUserData.Count() !=4)
            {
                return BadRequest(ModelState);
            }
            string password;
            Employee empl;
            try
            {
                password = newUserData[0].ToString();
                empl = new Employee
                {
                    Usersame = newUserData[1].ToString(),
                    Role = newUserData[2].ToString(),
                    PhoneNumber = newUserData[3].ToString()
                };
            }
            catch
            {
                return BadRequest();
            }
            var existingUserWithThatName = _context.Employees.SingleOrDefault(u => u.Usersame == empl.Usersame);
            if (existingUserWithThatName != null)
            {
                return BadRequest();
            }
            _context.Employees.Add(empl);
            await _context.SaveChangesAsync();
            var createdUser = _context.Employees.SingleOrDefault(p => p.Usersame == empl.Usersame);
            _context.UsersAccounts.Add(new UserAccount {EmployeeId = createdUser.Id, Password = password});
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = createdUser.Id }, createdUser);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            var account = await _context.UsersAccounts.SingleOrDefaultAsync(u => u.EmployeeId == employee.Id);
            if (account == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            _context.UsersAccounts.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}