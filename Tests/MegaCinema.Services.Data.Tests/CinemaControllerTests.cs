namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MegaCinema.Data.Common.Repositories;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Web.Controllers;
    using MegaCinema.Web.ViewModels.Cinema;
    using MegaCinema.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class CinemaControllerTests
    {
        public List<Cinema> GetTestData()
        {
            return new List<Cinema>
            {
                new Cinema()
                {
                    City = "Sofia",
                    Address = "Cheri vrah 100",
                    Id = 1,
                    ClosingHour = new DateTime(2020, 01, 01, 22, 30, 00),
                    OpenHour = new DateTime(2020, 01, 01, 10, 30, 00),
                },
                new Cinema()
                {
                    City = "Varna",
                    Address = "Varnenchik",
                    Id = 3,
                    ClosingHour = new DateTime(2020, 01, 01, 22, 30, 00),
                    OpenHour = new DateTime(2020, 01, 01, 10, 30, 00),
                },
            };
        }

        [Fact]
        public void MethosReturnsCorrectWithCorrectData()
        {
            var repo = new Mock<IRepository<Cinema>>();
            repo.Setup(r => r.All()).Returns(this.GetTestData().AsQueryable());

            var service = new CinemasService(repo.Object);
            var cinema = service.ShowProjections<Cinema>(1);

            Assert.Equal(1, cinema.Id);
            Assert.Equal("Sofia", cinema.City);
        }
    }
}
