namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using MegaCinema.Services.Data;
    using MegaCinema.Web.ViewModels;
    using MegaCinema.Web.ViewModels.ContactForm;
    using MegaCinema.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class HomeController : BaseController
    {
        private const double MovieScore = 7.0;
        private const string Subject = "MegaCinema ContactForm Message";
        private readonly IMoviesService moviesService;
        private readonly ILogger<HomeController> logger;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IWebHostEnvironment env;

        public HomeController(
            IMoviesService moviesService,
            ILogger<HomeController> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IWebHostEnvironment env)
        {
            this.moviesService = moviesService;
            this.logger = logger;
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
            this.env = env;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexAllMoviesViewModel
            {
                AllMovies = this.moviesService.AllMovies<IndexMovieViewModel>()
                .Where(x => x.ReleaseDate < DateTime.UtcNow && x.Score >= MovieScore)
                .ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult ContactUs()
        {
            return this.View();
        }

        public IActionResult MessageSentSuccessfully()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactForm input)
        {
            this.logger.LogDebug("Contact.OnPostSync entered");

            if (!this.ModelState.IsValid)
            {
                this.logger.LogDebug("Model state not valid");
                return this.View();
            }

            var recaptchaResponse = this.Request.Form["g-recaptcha-response"];

            if (string.IsNullOrEmpty(recaptchaResponse)
                || !this.RecaptchaPassed(recaptchaResponse))
            {
                this.logger.LogDebug("Recaptcha empty or failed");
                this.ModelState.AddModelError(string.Empty, "You failed the CAPTCHA");
                return this.View();
            }

            // var from = new EmailAddress(input.Email, input.Name);
            // var to = new EmailAddress(
            //    this.configuration.GetSection("ContactUsMailbox").Value,
            //    this.configuration.GetSection("ContactUsNickName").Value);

            // var apiKey = this.configuration.GetSection("SENDGRID_API_KEY").Value;

            // var client = new SendGridClient(apiKey);

            // var msg = MailHelper.CreateSingleEmail(from, to, subject, input.Message, input.Message);

            // this.logger.LogDebug("Sending email via SendGrid");
            // var response = await client.SendEmailAsync(msg);

            //// response always returning statuscode Forbidden ?!?
            // if (response.StatusCode != HttpStatusCode.Accepted)
            // {
            //    this.logger.LogDebug($"Sendgrid problem {response.StatusCode}");
            //    throw new ExternalException("Error sending message");
            // }

            // this.logger.LogDebug("Email sent via SendGrid");
            return this.RedirectToAction("Index");
        }

        private bool RecaptchaPassed(string recaptchaResponse)
        {
            this.logger.LogDebug("Contact.RecaptchaPassed entered");

            var secret =
                this.configuration.GetSection("RecaptchaKey").Value;

            var endPoint =
                this.configuration.GetSection("RecaptchaEndPoint").Value;

            var googleCheckUrl =
                $"{endPoint}?secret={secret}&response={recaptchaResponse}";

            this.logger.LogDebug("Checking reCaptcha");
            var httpClient = this.httpClientFactory.CreateClient();

            var response = httpClient.GetAsync(googleCheckUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                this.logger.LogDebug($"reCaptcha bad response {response.StatusCode}");
                return false;
            }

            dynamic jsonData =
                JObject.Parse(response.Content.ReadAsStringAsync().Result);

            this.logger.LogDebug("reCaptcha returned successfully");

            return jsonData.success == "true";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
