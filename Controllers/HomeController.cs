using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return View();
        }

        public async Task<IActionResult> Kitchen()
        {
            var model = await GetReservationAndCheckIns();
            return View(model);
        }       

        //[Authorize("ReceptionistPolicy")]
        public async Task<IActionResult> Reception()
        {
            var checkIns = (await context.CheckIns.ToListAsync()).Where(x => x.Date.Date == DateTime.Today);
            ReservationAndCheckIns receptionModel = new ReservationAndCheckIns
            {
                CheckIns = checkIns
            };
            return View(receptionModel);
        }

        //[Authorize("ReceptionistPolicy")]
        [HttpPost]
        public async Task<IActionResult> MakeReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                context.Add(reservation);
                await context.SaveChangesAsync();
            }
            var model = await GetReservationAndCheckIns();
            return View("Kitchen", model);
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

        private async Task<ReservationAndCheckIns> GetReservationAndCheckIns()
        {
            var checkIns = (await context.CheckIns.ToListAsync()).Where(x => x.Date.Date == DateTime.Today);
            if (checkIns == null)
            {
                checkIns = new List<CheckIn>();
            }
            Reservation reservation = (await context.Reservation.ToListAsync()).Where(x => x.Date.Date == DateTime.Today).FirstOrDefault();
            if (reservation == null)
            {
                reservation = new Reservation();
            }
            return new ReservationAndCheckIns
            {
                CheckIns = checkIns,
                Reservation = reservation
            };
        }
    }
}
