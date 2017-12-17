using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.Domain.DataBase;

namespace Workshop.Core.Api.Utils
{
    public class DeleteUtils
    {
        public async Task DeleteTask(int id, WorkshopContext context)
        {
            var workshopTask = await context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            if (workshopTask != null)
            {
                context.Tasks.Remove(workshopTask);
                await context.SaveChangesAsync();
            }  
        }

        public async Task DeleteProblem(int id, WorkshopContext context)
        {
            var problem = await context.Problems.SingleOrDefaultAsync(m => m.Id == id);
            if (problem != null)
            {
                context.Problems.Remove(problem);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteObject(int id, WorkshopContext context)
        {
            var obj = await context.Objects.SingleOrDefaultAsync(m => m.Id == id);
            if (obj != null)
            {
                context.Objects.Remove(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteClient(int id, WorkshopContext context)
        {
            var client = await context.Clients.SingleOrDefaultAsync(m => m.Id == id);
            if (client != null)
            {
                context.Clients.Remove(client);
                await context.SaveChangesAsync();
            }
        }
    }
}
