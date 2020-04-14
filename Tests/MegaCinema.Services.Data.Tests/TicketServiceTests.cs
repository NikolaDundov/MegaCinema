namespace MegaCinema.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Data.Repositories;
    using MegaCinema.Web.Controllers;
    using MegaCinema.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class TicketServiceTests
    {
        //[Fact]
        //public void TestViewModelInIndexPage()
        //{
        //    var mockService = new Mock<ITicketsService>();
        //    var mockUserManager = new UserManager<ApplicationUser>();
        //    mockService.Setup(x => x.ShowAllMyTickets())
        //        .Returns(new List<MyTicketsViewModel>
        //     {
        //        new MyTicketsViewModel { Id = 1 },
        //        new MyTicketsViewModel { Id = 2 },
        //        new MyTicketsViewModel { Id = 3 },
        //     });

        //    var controller = new TicketsController(mockService.Object, mockUserManager);
        //    var result = controller.MyTickets();
        //    Assert.IsType<ViewResult>(result);
        //    var viewResult = result as ViewResult;
        //    Assert.IsType<MyTicketsViewModel>(viewResult.Model);
        //    var viewModel = viewResult.Model as List<MyTicketsViewModel>;
        //    var indexViewModel = new AllIndexMovieViewModel();
        //    indexViewModel.AllMovies = viewModel;
        //    Assert.Equal(3, indexViewModel.AllMovies.Count());
        //}
    }
}
