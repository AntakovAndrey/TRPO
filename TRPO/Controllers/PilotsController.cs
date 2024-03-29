﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRPO.Database;

namespace TRPO.Controllers
{
    public class PilotsController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Check(Models.Pilot pilot)
        {
            if (ModelState.IsValid)
            {
                PilotDB.SavePilotToDB(pilot);
                return RedirectToAction("PilotAdded", pilot);
            }
            else
            {
                return View("Index", pilot);
            }
        }
        public IActionResult PilotAdded(Models.Pilot pilot)
        {
            return View(pilot);
        }
    }
}