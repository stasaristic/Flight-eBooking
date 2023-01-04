using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Core;
using Flight_eBooking.Core.Repositories;
using Flight_eBooking.Core.ViewModels;
using Flight_eBooking.Data.Enums;
using Flight_eBooking.Models;
using Flight_eBooking.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Flight_eBooking.Controllers
{
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public FlightsController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = Constants.Policies.RequireAgent)]
        public async Task<IActionResult> Index()
        {
            var model = await _unitOfWork.Flight.GetFlights();

            return View(model);
        }

        [Authorize(Policy = Constants.Policies.RequireAgent)]
        public async Task<IActionResult> Edit(int id)
        {
            var flight = _unitOfWork.Flight.GetFlight(id);
            var flightclass = flight.FlightClass;
            IList<bool> selected = new List<bool>();
            foreach (FlightClass item in Enum.GetValues(typeof(FlightClass)))
            {
                if (item.ToString() != flight.FlightClass.ToString())
                {
                    selected.Add(false);
                }
                else
                {
                    selected.Add(true);
                }
            }

            var vm = new EditFlightViewModel { Flight = flight }; //, FlightClasses = flightClass };  
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditFlightViewModel data)
        {
            var flight = _unitOfWork.Flight.GetFlight(data.Flight.Id);
            if (flight == null)
            {
                return NotFound();
            }

            flight.DepartureDate = data.Flight.DepartureDate;
            flight.TicketPrice = data.Flight.TicketPrice;
            flight.Seats = data.Flight.Seats;

            _unitOfWork.Flight.UpdateFlight(flight);

            return RedirectToAction("Edit", new { id = flight.Id });
        }

        public async Task<IActionResult> Create() 
        {
            var destList = _unitOfWork.Destination.GetAll().ToList();
            ViewBag.data = destList;

            return View();
        }

    }
}
