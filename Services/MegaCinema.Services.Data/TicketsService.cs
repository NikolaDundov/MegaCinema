using MegaCinema.Data.Common.Repositories;
using MegaCinema.Data.Models;
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

        public void CreateTicket<T>(string userId, int projectionId, string projectionType, string ticketType)
        {
            var price = 10m;
            if (ticketType == "Children" || ticketType == "Adult")
            {
                price *= 0.8m;
            }

            var ticket = new Ticket
            {
                UserId = userId,
                ProjectionId = projectionId,
                Type = (TicketType)Enum.Parse(typeof(TicketType), ticketType),
                Price = price,
            };

            this.repository.AddAsync(ticket);
            this.repository.SaveChangesAsync();
        }

        public T CreateTicket<T>(int id)
        {
            var projection = this.projectionsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            throw new NotImplementedException();
        }
    }
}
