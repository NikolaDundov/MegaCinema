using MegaCinema.Data.Common.Repositories;
using MegaCinema.Data.Models;
using MegaCinema.Web.ViewModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaCinema.Services.Data
{
    public class TicketsService : ITicketsService
    {
        private readonly IRepository<Ticket> repository;
        private readonly IRepository<Projection> projectionsRepository;

        public TicketsService(
            IRepository<Ticket> repository,
            IRepository<Projection> projectionsRepository)
        {
            this.repository = repository;
            this.projectionsRepository = projectionsRepository;
        }

        public TicketViewModel CreateTicket(int id)
        {
            var projection = this.projectionsRepository.All().FirstOrDefault(x => x.Id == id);

            var ticket = new TicketViewModel
            {
                Projection = projection,
                ProjectionId = projection.Id,
            };

            this.repository.SaveChangesAsync();
            return null;
        }
    }
}
