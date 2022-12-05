using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPO.Services;

namespace TRPO.Models
{
    public class Route
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
        public Route() { }

        public void saveToDB()
        {

        }

         

        
    }
}
