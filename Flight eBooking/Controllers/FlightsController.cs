using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Core;
using Flight_eBooking.Core.Repositories;
using Flight_eBooking.Core.ViewModels;
using Flight_eBooking.Data.Enums;
using Flight_eBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

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

        // Ticket website
        public async Task<ActionResult> TicketSite() 
        {
            var destList = _unitOfWork.Destination.GetAll();
            ViewBag.data = destList;

            var allflights = await _unitOfWork.Flight.GetFlights();

            return View(allflights);
        }
        [HttpPost]
        public async Task<IActionResult> TicketSite(int DestinationDepartureId, int DestinationArrivalId) 
        {
            var currentDateTime = DateTime.Now.AddDays(3);  // can book flights ONLY if they are after 3 days in the future

            // get all flights that have selected departure and selected arrival and 
            var foundFlights = await _context.Flights.Include(f => f.DestinationDeparture).Where(f => f.DestinationDepartureId == DestinationDepartureId)
                                        .Include(f => f.DestinationArrival).Where(f => f.DestinationArrivalId == DestinationArrivalId)
                                        .Where(f => f.DepartureDate > currentDateTime)
                                        .ToListAsync();

            /*
            if (foundFlights.Count == 0)
            {
                // not practical and slow tbh
                var allAirports = _unitOfWork.Destination.GetAll().ToList();

                //all flights in the database (three days in the future)         
                var allFlights = await _context.Flights
                                            .Where(f => f.DepartureDate > currentDateTime)
                                            .ToListAsync();

                string[] Airports = new string[allAirports.Count];  // save all destination names
                for (int i = 0; i < allAirports.Count; i++) 
                {
                    Airports[i] = allAirports[i].NameDest;                                
                }

                int numOfFlights = allFlights.Count;
                int counter = 0;
                uint[] edges = new uint [numOfFlights*3];
                for (int i = 0; i < numOfFlights*3; i=i+3) 
                {
                    // {0, 1, 100$, ... x, y, $$$}                   
                    if (counter < numOfFlights)
                    {
                        // ! greska 
                        // 0 1 2, 
                        //   1, 2, 3
                        // prepisuje se jedno preko drugog, drugacije formiraj loo
                        edges[i] = (uint)allFlights[counter].DestinationDepartureId;
                        edges[i + 1] = (uint)allFlights[counter].DestinationArrivalId;
                        edges[i + 2] = (uint)allFlights[counter].TicketPrice;
                        counter++;
                    }                   
                }

                // setup graph
                Dijkstra.DijkstraGraph dijkstraGraph = Dijkstra.SetupGraph(Airports, edges);
                // setup the algorithm
                Dijkstra.DijkstraAlgoData algo = Dijkstra.StartDijkstra(dijkstraGraph, (uint)DestinationDepartureId, (uint)DestinationArrivalId);
                while (!Dijkstra.Process(algo)) { }
                var path = Dijkstra.GetPath(algo);

                // debugging
                var s = "";
                for (int i = 0; i < path.Length; ++i)
                {
                    if (i != 0)
                    {
                        s += ", ";
                    }
                    s += "" + path[i];
                }
                Console.WriteLine(s);
            }
            */
            var destList = _unitOfWork.Destination.GetAll();
            ViewBag.data = destList;

            return View(foundFlights);
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
        [Authorize(Policy = Constants.Policies.RequireAgent)]
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
        [Authorize(Policy = Constants.Policies.RequireAgent)]
        public IActionResult Create()
        {
            var destList = _unitOfWork.Destination.GetAll();
            ViewBag.data = destList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFlightViewModel createFlightViewModel)
        {
            var destList = _unitOfWork.Destination.GetAll();
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

        
        public async Task<IActionResult> Details(int id)
        {
            var flight = await _unitOfWork.Flight.GetFlightAsync(id);

            if (flight == null) { return View("Not Found"); }
            var vm = new DetailsFlightViewModel { Flight = flight };
            return View(vm);
        }

        // Flights details
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(DetailsFlightViewModel data) 
        {
            /*  create new reservation and add it to the database   */

            // getting userId
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            // getting flight id
            var flightId = data.Flight.Id;

            // reservation status is set to pending so the agent will aprove it
            var reservationStatus = ReservationStatus.Pending;

            // number of reserved seats
            var numOfSeats = data.Reservation.NumberOfSeats;

            /*  create new reservation and add it to the database   */
            var reservation = new Reservation()
            {
                NumberOfSeats = numOfSeats,
                UserId = userId,
                FlightId = flightId,
                StatusRes = reservationStatus
            };
            
            _unitOfWork.Reservation.InsertReservation(reservation);

            /*  lower the number of taken seats on the flight   */
            var flight = await _unitOfWork.Flight.GetFlightAsync(data.Flight.Id);
            if (flight == null)
            {
                return NotFound();
            }

            flight.Seats = data.Flight.Seats - numOfSeats;

            _unitOfWork.Flight.UpdateFlight(flight);

            return RedirectToAction("Details", new { id = flight.Id });
        }
    }
}
