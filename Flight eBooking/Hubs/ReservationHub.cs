using Microsoft.AspNetCore.SignalR;

namespace Flight_eBooking.Hubs
{
    public class ReservationHub : Hub
    {
        public Task UpdateResStatus(string status, int id) 
        {
            return Clients.All.SendAsync("UpdateReservationStatus", status, id);
        }
    }
}
