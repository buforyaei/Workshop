using System.Threading.Tasks;
using Workshop.Domain.DataBase;
using Workshop.Domain.Models;

namespace Workshop.Core.Api.Utils
{
    public static class DeleteUtils
    {
        public static async Task DeleteTask(WorkshopTask task, WorkshopContext context)
        {
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteProblem(WorkshopProblem problem, WorkshopContext context)
        {
            context.Problems.Remove(problem);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteObject(WorkshopObject obj, WorkshopContext context)
        {
            context.Objects.Remove(obj);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteClient(Client client, WorkshopContext context)
        {
            context.Clients.Remove(client);
            await context.SaveChangesAsync();
        }
    }
}
