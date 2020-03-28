using MegaCinema.Data.Common.Repositories;
using MegaCinema.Data.Models;
using MegaCinema.Web.ViewModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinema.Services.Data
{
    public class TicketsService : ITicketsService
    {
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IRepository<Projection> projectionsRepository;
        private readonly IRepository<Hall> hallRepository;
        private readonly IRepository<Seat> seatRepository;
        private List<char> RowsList = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L' };
        private List<int> SeatsNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        public TicketsService(
            IRepository<Ticket> ticketRepository,
            IRepository<Projection> projectionsRepository,
            IRepository<Hall> hallRepository,
            IRepository<Seat> seatRepository)
        {
            this.ticketRepository = ticketRepository;
            this.projectionsRepository = projectionsRepository;
            this.hallRepository = hallRepository;
            this.seatRepository = seatRepository;
        }

        public async Task<int> AddTicketAndSeat(int projectionId, string userId, char row, int seat, decimal price)
        {
            var projection = this.projectionsRepository.All().FirstOrDefault(x => x.Id == projectionId);
            if (projection == null)
            {
                return -1;
            }

            var seatToCheck = projection.Seats.FirstOrDefault(x => x.Row == row && x.SeatNumer == seat);
            if (seatToCheck == null)
            {
                return -1;
            }

            if (seatToCheck.IsOccupied == true)
            {
                return -2;
            }

            TicketType ticketType;
            if (price == 10)
            {
                ticketType = TicketType.Regular;
            }
            else if (price == 8.50m)
            {
                ticketType = TicketType.Children;
            }
            else
            {
                ticketType = TicketType.Adult;
            }

            seatToCheck.IsOccupied = true;
            var ticket = new Ticket
            {
                MovieId = projection.MovieId,
                Price = price,
                ProjectionId = projectionId,
                SeatNumer = seatToCheck.SeatNumer,
                Row = seatToCheck.Row,
                Type = ticketType,
                UserId = userId,
            };

            await this.ticketRepository.AddAsync(ticket);
            await this.ticketRepository.SaveChangesAsync();
            return 0;
        }

        public TicketViewModel GetTicketDetails(int projectionId)
        {
            var projection = this.projectionsRepository.All().FirstOrDefault(x => x.Id == projectionId);
            if (projection == null)
            {
                return null;
            }

            var hallId = projection.HallId;
            var hall = this.hallRepository.All().FirstOrDefault(x => x.Id == hallId);

            var viewModel = new TicketViewModel
            {
                ProjectionId = projectionId,
                MovieId = projection.MovieId,
                Rows = this.RowsList,
                SeatNumbers = this.SeatsNumbers,
            };

            var prices = new List<TicketByPriceModel>()
            {
                new TicketByPriceModel
                {
                    Type = TicketType.Regular,
                    Price = 10,
                },
                new TicketByPriceModel
                {
                    Type = TicketType.Children,
                    Price = 8.50m,
                },
                new TicketByPriceModel
                 {
                     Type = TicketType.Adult,
                     Price = 9,
                 },
            };

            viewModel.TicketPrice = prices;
            return viewModel;
        }
    }
}
