using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;
using Morgenmadsbuffeten.ViewModels;

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

        public IActionResult Kitchen()
        {
            return View();
        }

        //[Authorize("ReceptionistPolicy")]
        public async Task<IActionResult> Reception()
        {
            var checkIns = (await context.CheckIns.ToListAsync()).Where(x => x.Date.Date == DateTime.Today);
            ReceptionModel receptionModel = new ReceptionModel();
            receptionModel.CheckIns = checkIns;
            return View(receptionModel);
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                context.Add(reservation);
                await context.SaveChangesAsync();
                return RedirectToAction("Kitchen");
            }
            return View("Kitchen");
        }

        //[Authorize("WaiterPolicy")]
        public IActionResult Restaurant()
        {
            return View();
        }

        //[Authorize("WaiterPolicy")]
        [HttpPost]
        public async Task<IActionResult> CheckIn(CheckIn checkIn)
        {
            if (ModelState.IsValid)
            {
                context.Add(checkIn);
                await context.SaveChangesAsync();
                return RedirectToAction("Restaurant"); // Return to the same page to be ready to check in more guests
            }
            return View(checkIn);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
