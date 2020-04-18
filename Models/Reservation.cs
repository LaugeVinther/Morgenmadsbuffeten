using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Morgenmadsbuffeten.Models
{
    public class Reservation
    {
        public long ReservationId { get; set; }

        [Display(Name = "Voksne")]
        public int Adults { get; set; }

        [Display(Name = "Børn")]
        public int Children { get; set; }

        [Display(Name = "Værelsesnummer")]
        public int RoomNumber  { get; set; }
        public bool CheckedIn { get; set; } = true;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
