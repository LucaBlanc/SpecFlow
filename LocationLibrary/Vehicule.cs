using System;
using System.Collections.Generic;
using System.Text;

namespace LocationLibrary
{
    public class Vehicule
    {
        public string Registration { get; private set; }
        public string Mark { get; private set; }
        public string Model { get; private set; }
        public string Color { get; private set; }
        public double ReservationPrice { get; private set; }
        public double KilometerRate { get; private set; }
        public int Horsepower { get; private set; }

        public Vehicule( string registration, string mark, string model, string color, double reservationPrice, double kilometerRate, int Horsepower)
        {
            this.Registration = registration;
            this.Mark = mark;
            this.Model = model;
            this.Color = color;
            this.ReservationPrice = reservationPrice;
            this.KilometerRate = kilometerRate;
            this.Horsepower = Horsepower;
        }
    }
}
