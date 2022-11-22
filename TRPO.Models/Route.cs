using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPO.Models
{
    internal class Route
    {
        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string FinishPoint { get; set; }
        public int Distance { get; set; }

        public Route(int id, string startPoint, string finishPoint, int distance)
        {
            Id = id;
            StartPoint = startPoint;
            FinishPoint = finishPoint;
            Distance = distance;
        }
    }
}
