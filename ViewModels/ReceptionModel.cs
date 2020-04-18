using Morgenmadsbuffeten.Models;
using System.Collections.Generic;

namespace Morgenmadsbuffeten.ViewModels
{
    public class ReceptionModel
    {
        public IEnumerable<CheckIn> CheckIns { get; set; }
        public Reservation Reservation { get; set; }
    }
}
