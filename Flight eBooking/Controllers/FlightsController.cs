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
using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> TicketSite() 
        {
            var allflights = await _unitOfWork.Flight.GetFlights();

            var destList = _unitOfWork.Destination.GetAll().ToList();
            ViewBag.data = destList;

            return View(allflights);
        }

        // Flights edit
        [Authorize(Policy = Constants.Policies.RequireAgent)]
        public async Task<IActionResult> Edit(int id)
        {
            var flight = await _unitOfWork.Flight.GetFlightAsync(id);

            var vm = new EditFlightViewModel { Flight = flight };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFlightViewModel data)
        {
            var flight = await _unitOfWork.Flight.GetFlightAsync(data.Flight.Id);
            if (flight == null)
            {
                return NotFound();
            }

            flight.DepartureDate = data.Flight.DepartureDate;
            flight.TicketPrice = data.Flight.TicketPrice;
            flight.Seats = data.Flight.Seats;
            flight.FlightClass = data.Flight.FlightClass;

            _unitOfWork.Flight.UpdateFlight(flight);

            return RedirectToAction("Edit", new { id = flight.Id });
        }

        // Flights delete
        public async Task<IActionResult> Delete(int id) 
        {
            var flight = await _unitOfWork.Flight.GetFlightAsync(id);

            if (flight == null) { return View("Not Found"); }
            var vm = new EditFlightViewModel { Flight = flight };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var flight = await _unitOfWork.Flight.GetFlightAsync(id);
            if (!ModelState.IsValid) { return View(); }

            await _unitOfWork.Flight.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        // Flights create
        public IActionResult Create()
        {
            var destList = _unitOfWork.Destination.GetAll().ToList();
            ViewBag.data = destList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFlightViewModel createFlightViewModel)
        {
            var destList = _unitOfWork.Destination.GetAll().ToList();
            ViewBag.data = destList;
            try 
            {
                int DestinationDepartureId = (int)createFlightViewModel.DestinationDepartureId;
                int DestinationArrivalId = (int)createFlightViewModel.DestinationArrivalId;
                if (createFlightViewModel.DepartureDate < DateTime.Now) 
                {
                    ModelState.AddModelError(string.Empty, "Date and time of the Flight's departure can't be before the current time!");
                    return View(createFlightViewModel);
                }
                DateTime DepartureDate = (DateTime)createFlightViewModel.DepartureDate;
                FlightClass flightClass = (FlightClass)createFlightViewModel.FlightClass;
                float TicketPrice = (float)createFlightViewModel.TicketPrice;
                int Seats = (int)createFlightViewModel.Seats;

                if (ModelState.IsValid)
                {
                    String FlightName = _unitOfWork.Flight.FlightNameGenerator(DestinationDepartureId, DestinationArrivalId);
                    var flight = new Flight()
                    {
                        FlightName = FlightName,
                        DestinationDepartureId = DestinationDepartureId,
                        DestinationArrivalId = DestinationArrivalId,
                        DepartureDate = DepartureDate,
                        TicketPrice = TicketPrice,
                        FlightClass = flightClass,
                        Seats = Seats
                    };
                    _unitOfWork.Flight.InsertFlight(flight);
                    return RedirectToAction("Index");
                }

                else if (DestinationDepartureId == DestinationArrivalId && DestinationArrivalId != 0)
                {
                    ModelState.AddModelError(string.Empty, "Departure and Arrival can't be from the same place!");
                    return View(createFlightViewModel);
                }                
            } catch(InvalidOperationException ex) 
            {
                Console.WriteLine(ex);

                ModelState.AddModelError(string.Empty, "Please Enter All Information About The Flight!");
                return View(createFlightViewModel);
            }

            return View(createFlightViewModel);
        }

        // Flights details
        public async Task<IActionResult> Details(int id) 
        {
            var flight = await _unitOfWork.Flight.GetFlightAsync(id);

            if (flight == null) { return View("Not Found"); }
            var vm = new EditFlightViewModel { Flight = flight };
            return View(vm);
        }
    }
}
