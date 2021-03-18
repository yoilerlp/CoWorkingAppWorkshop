using System;
namespace CoWorkingApp.Models
{
    public class Reservation
    {
        public Guid ReservationId {set; get;} = Guid.NewGuid();

        public DateTime ReservationDate {set; get;}

        public Guid DeskId {set; get;}

        public Guid UserId {set; get;}
        
    }
}