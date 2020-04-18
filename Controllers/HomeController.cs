using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;

namespace Morgenmadsbuffeten.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View("Kitchen");
        }

        //[Authorize("ReceptionistPolicy")]
        public async Task<IActionResult> Reception()
        {
            var reservations = (await context.Reservations.ToListAsync()).Where(x => x.Date.Date == DateTime.Today);
            return View(reservations);
        }

        //[Authorize("WaiterPolicy")]
        public IActionResult Restaurant()
        {
            return View();
        }

        //[Authorize("WaiterPolicy")]
        [HttpPost]
        public async Task<IActionResult> Restaurant(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                context.Add(reservation);
                await context.SaveChangesAsync();
                return RedirectToAction("Restaurant"); // Return to the same page to be ready to check in more guests
            }
            return View(reservation);
        }

        public IActionResult Kitchen()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
