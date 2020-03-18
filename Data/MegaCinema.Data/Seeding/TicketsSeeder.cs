using MegaCinema.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinema.Data.Seeding
{
    public class TicketsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Tickets.AnyAsync())
            {
                return;
            }

            var projectionsId = dbContext.Projections.Select(x => x.Id).ToList();

            foreach (var projectionId in projectionsId)
            {
                var ticket = new Ticket
                {
                    ProjectionId = projectionId,
                };
            }
        }
    }
}
