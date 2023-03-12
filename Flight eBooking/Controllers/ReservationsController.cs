using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Core;
using Flight_eBooking.Core.Repositories;
using Flight_eBooking.Hubs;
using Flight_eBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Flight_eBooking.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<ReservationHub> _hubContext;

        public ReservationsController(ApplicationDbContext context, IUnitOfWork unitOfWork, IHubContext<ReservationHub> hubContext)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
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

            // update on client realtime using the hub context
            await _hubContext.Clients.All.SendAsync("UpdateReservationStatus", reservation.StatusRes.ToString(), reservation.Id);

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Decline(int Id)
        {
            var reservation = await _unitOfWork.Reservation.GetReservationAsync(Id);

            reservation.StatusRes = Data.Enums.ReservationStatus.Declined;

            _unitOfWork.Reservation.UpdateReservation(reservation);

            await _hubContext.Clients.All.SendAsync("UpdateReservationStatus", reservation.StatusRes.ToString(), reservation.Id);

            return RedirectToAction("Index");

        }

        [Authorize]
        public async Task<IActionResult> MyReservations()
        {
            // getting userId
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var myReservations = await _unitOfWork.Reservation.GetReservationsByUserIdAsync(userId);
            var myReservationsList = myReservations.ToList();
            for (int i = 0; i < myReservationsList.Count; i++)
            {
                await _hubContext.Clients.All.SendAsync("UpdateReservationStatus", myReservationsList[i].StatusRes.ToString(), myReservationsList[i].Id);
            }
            return View(myReservations);
        }

    }
}
