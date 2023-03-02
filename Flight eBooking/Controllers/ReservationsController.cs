using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Core;
using Flight_eBooking.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_eBooking.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationsController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = Constants.Policies.RequireAgent)]
        public async Task<IActionResult> Index()
        {
            var allReservations = await _unitOfWork.Reservation.GetReservationsAsync();
            return View(allReservations);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int Id)
        {
            var reservation = await _unitOfWork.Reservation.GetReservationAsync(Id);

            reservation.StatusRes = Data.Enums.ReservationStatus.Approved;

            _unitOfWork.Reservation.UpdateReservation(reservation);

            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async Task<IActionResult> Decline(int Id)
        {
            var reservation = await _unitOfWork.Reservation.GetReservationAsync(Id);

            reservation.StatusRes = Data.Enums.ReservationStatus.Declined;

            _unitOfWork.Reservation.UpdateReservation(reservation);

            return RedirectToAction("Index");

        }

    }
}
