using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Domain.DataBase;
using Workshop.Domain.Models;

namespace Workshop.Core.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly WorkshopContext _db;
        public ValuesController()
        {
            _db = new WorkshopContext(new DbContextOptions<WorkshopContext>()); 
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //_db.Employees.Add(new Employee
            //{
            //    Usersame = "Admin",
            //    PhoneNumber = "123456789",
            //    Role = "A"

            //});
            //_db.SaveChanges();
            //var user = _db.Employees.ToArray().Single(u => u.Usersame == "Admin");
            //_db.UsersAccounts.Add(new UserAccount
            //{
            //    Password = "Admin".GetHashCode().ToString(),
            //    EmployeeId = user.Id
            //});
            //_db.SaveChanges();
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
