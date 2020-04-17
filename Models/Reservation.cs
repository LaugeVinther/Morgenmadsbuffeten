using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morgenmadsbuffeten.Models
{
    public class Reservation
    {
        public int Adults { get; set; }
        public int Children { get; set; }
        public int RoomNumber  { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime Date { get; set; } 
    }
}
