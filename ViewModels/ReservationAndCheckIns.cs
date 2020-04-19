using Morgenmadsbuffeten.Models;
using System.Collections.Generic;

namespace Morgenmadsbuffeten.ViewModels
{
    public class ReservationAndCheckIns
    {
        public IEnumerable<CheckIn> CheckIns { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
