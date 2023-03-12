using Flight_eBooking.Data.Enums;
using Flight_eBooking.Models;
using Microsoft.AspNetCore.SignalR;

namespace Flight_eBooking.Hubs
{
    public class ReservationHub : Hub
    {

        public Task UpdateResStatus(string status, int id) 
        {
            return Clients.All.SendAsync("UpdateReservationStatus", status, id);
        }

        public Task NewReservationSend(int id, string userName, string userEmail, string flightName,
            string flightDepartureDate, string flightClass, string totalPrice, int numOfSeats, string statusRes) 
        {
            return Clients.All.SendAsync("NewReservationRecieve", id, userName, userEmail, flightName, 
                flightDepartureDate, flightClass, totalPrice, numOfSeats, statusRes);
        }

        public Task MyNewReservationSend(int id, string flightName,
            string flightDepartureDate, string flightClass, 
            string totalPrice, int numOfSeats, string statusRes)
        {
            return Clients.All.SendAsync("MyNewReservationRecieve", id, flightName,
                flightDepartureDate, flightClass, totalPrice, numOfSeats, statusRes);
        }
    }
}
