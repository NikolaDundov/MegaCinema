namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Identity;

    public class TicketsService : ITicketsService
    {
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IRepository<Projection> projectionsRepository;
        private readonly IRepository<Hall> hallRepository;
        private readonly IRepository<Seat> seatRepository;
        private readonly IRepository<Movie> movieRepository;
        private readonly IRepository<Cinema> cinemaRepository;

        private List<char> rowsList = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L' };
        private List<int> seatsNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        public TicketsService(
            IRepository<Ticket> ticketRepository,
            IRepository<Projection> projectionsRepository,
            IRepository<Hall> hallRepository,
            IRepository<Seat> seatRepository,
            IRepository<Movie> movieRepository,
            IRepository<Cinema> cinemaRepository)
        {
            this.ticketRepository = ticketRepository;
            this.projectionsRepository = projectionsRepository;
            this.hallRepository = hallRepository;
            this.seatRepository = seatRepository;
            this.movieRepository = movieRepository;
            this.cinemaRepository = cinemaRepository;
        }

        public ClaimsPrincipal User { get; private set; }

        public async Task<int> AddTicketAndSeat(int projectionId, string userId, char row, int seat, decimal price)
        {
            var projection = this.projectionsRepository.All().FirstOrDefault(x => x.Id == projectionId);
            if (projection == null)
            {
                return -1;
            }

            var seatsInProjection = this.seatRepository.All().Where(x => x.ProjectionId == projection.Id).ToList();
            projection.Seats = seatsInProjection;
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
            return ticket.Id;
        }

        public async Task DeleteByTicketId(int id)
        {
            var ticket = this.ticketRepository.All().FirstOrDefault(x => x.Id == id);
            this.ticketRepository.Delete(ticket);
            await this.ticketRepository.SaveChangesAsync();
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
                Rows = this.rowsList,
                SeatNumbers = this.seatsNumbers,
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

            var occupiedSeats = this.seatRepository.All()
                .Where(x => x.ProjectionId == viewModel.ProjectionId && x.IsOccupied == true)
                .OrderBy(x => x.Row).ThenBy(x => x.SeatNumer)
                .ToList();

            viewModel.OccupiedSeats = occupiedSeats;
            viewModel.TicketPrice = prices;
            return viewModel;
        }

        public IEnumerable<MyTicketsViewModel> ShowAllMyTickets()
        {
            var allMyTickets = this.ticketRepository.All().ToList();
            var ticketsViewModel = new List<MyTicketsViewModel>();

            foreach (var ticket in allMyTickets)
            {
                var movie = this.movieRepository.All().FirstOrDefault(x => x.Id == ticket.MovieId);
                var projection = this.projectionsRepository.All().FirstOrDefault(x => x.Id == ticket.ProjectionId);

                var ticketToAdd = new MyTicketsViewModel
                {
                    CreatedOn = ticket.CreatedOn,
                    UserId = ticket.UserId,
                    MovieTitle = movie.Title,
                    Price = ticket.Price,
                    ProjectionStartTime = projection.StartTime,
                    Row = ticket.Row,
                    SeatNumber = ticket.SeatNumer,
                    Type = ticket.Type,
                };
                ticketsViewModel.Add(ticketToAdd);
            }

            return ticketsViewModel;
        }

        public BookedTicketViewModel ShowBookedTicket(int ticketId)
        {
            var ticket = this.ticketRepository.All().FirstOrDefault(x => x.Id == ticketId);
            var projectionId = ticket.ProjectionId;
            var projection = this.projectionsRepository.All().FirstOrDefault(x => x.Id == projectionId);
            var movie = this.movieRepository.All().FirstOrDefault(x => x.Id == ticket.MovieId);
            var cinema = this.cinemaRepository.All().FirstOrDefault(x => x.Id == projection.CinemaId);

            var ticketToShow = new BookedTicketViewModel
            {
                MovieTitle = movie.Title,
                CinemaAdress = cinema.Address,
                Price = ticket.Price,
                ProjectionStartTime = projection.StartTime,
                Row = ticket.Row,
                SeatNumber = ticket.SeatNumer,
                Type = ticket.Type,
            };

            return ticketToShow;
        }

        public int TicketsCount()
        {
            return this.ticketRepository.All().Count();
        }
    }
}
