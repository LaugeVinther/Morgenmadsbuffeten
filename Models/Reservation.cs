using System;
using System.ComponentModel.DataAnnotations;

namespace Morgenmadsbuffeten.Models
{
    public class Reservation
    {
        public long ReservationId { get; set; }

        [Display(Name = "Antal voksne")]
        public int NumberOfAdults { get; set; }

        [Display(Name = "Antal børn")]
        public int NumberOfChildren { get; set; }

        [Display(Name = "Dato")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
