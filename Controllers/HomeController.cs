using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Areas.Identity.Pages.Account;
using TheBlogProject.Models;
using TheBlogProject.ViewModels;

namespace TheBlogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBlogEmailSender _blogEmailSender;

        public HomeController(ILogger<HomeController> logger, IBlogEmailSender blogEmailSender)
        {
            _logger = logger;
            _blogEmailSender = blogEmailSender;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe contactMe)
        {
            contactMe.Message = $"{contactMe.Message} <hr/> Email: {contactMe.Email}";

            await _blogEmailSender.SendContactEmailAsync(contactMe.Email, contactMe.Name, contactMe.Subject, contactMe.Message);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
