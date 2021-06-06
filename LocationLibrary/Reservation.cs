using System;
using System.Collections.Generic;
using System.Text;

namespace LocationLibrary
{
    public class Reservation
    {
        public Client Client { get; set; }
        public Vehicule Vehicule { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EstimateKm { get; set; }
        public double Price { get; set; }

        public Reservation(Client client, Vehicule vehicule, DateTime startTime, DateTime endTime, int estimateKm )
        {
            this.Client = client;
            this.Vehicule = vehicule;
            this.StartDate = startTime;
            this.EndDate = endTime;
            this.EstimateKm = estimateKm;
            this.Price = this.Vehicule.ReservationPrice + this.Vehicule.KilometerRate * estimateKm;
        }
    }
}
