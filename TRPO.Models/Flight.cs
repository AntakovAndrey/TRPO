using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlTypes;
using System.Numerics;
using TRPO.Interfaces;
using TRPO.Services;

namespace TRPO.Models
{
    public class Flight
    {
        
        public Route? FlightRoute { get; set; }
        public Plane? Plane { get; set; }
        public Pilot? Pilot { get; set; }

        [Required]
        public int FlightId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan FinishTime { get; set; }

        [Required]
        public int RouteId { get; set; }
        
        [Required]
        public int PlaneId { get; set; }
        
        [Required]
        public int PilotId { get; set; }

        public string? Status { get; set; }

        public Flight(int flightId, DateTime date, TimeSpan startTime, TimeSpan finishTime, string status, Route route, Plane plane, Pilot pilot)
        {
            FlightId = flightId;
            Date = date;
            StartTime = startTime;
            FinishTime = finishTime;
            Plane = plane;
            FlightRoute = route;
            Status=status;
            Pilot=pilot;
        }
        public Flight() { }
    }
   
}