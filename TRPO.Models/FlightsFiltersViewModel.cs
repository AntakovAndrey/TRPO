using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO.Models
{
    public class FlightsFiltersViewModel
    {
        public string startWayPoint { get; set; }
        public string finishWayPoint { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }

        FlightsFiltersViewModel() { }
    }
}
