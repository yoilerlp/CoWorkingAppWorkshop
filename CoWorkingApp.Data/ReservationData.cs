using CoWorkingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CoWorkingApp.Data {
    public class ReservationData {
        private JsonManager<Reservation> jsonManager;

        public ReservationData()
        {
           jsonManager = new JsonManager<Reservation>();
        }

        public bool CreateReservation(Reservation newReservation)
        {
            try
            {
                var reservationCollection = jsonManager.GetCollection();
                reservationCollection.Add(newReservation);
                jsonManager.SaveCollection(reservationCollection);
                return true;                
            }
            catch 
            {
                return false;
            }
        }

        public bool CalcelReservation(Guid idReservation)
        {
            try
            {
                var reservationCollection = jsonManager.GetCollection();

                var indexReservation = reservationCollection.FindIndex(r => r.ReservationId == idReservation);
                reservationCollection.RemoveAt(indexReservation);
                jsonManager.SaveCollection(reservationCollection);
                return true;

            }
            catch 
            {
                return false;
            }
        }

        public List<Reservation> GetReservationsByUserId(Guid userId)
        {
            var reservationCollection = jsonManager.GetCollection();

            return reservationCollection.Where(r => r.UserId == userId && r.ReservationDate > DateTime.Now).ToList();
        }
    }
}