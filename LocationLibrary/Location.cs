using System;
using System.Linq;
using System.Collections.Generic;

namespace LocationLibrary
{
    public class Location
    {
        private IDataLayer _dataLayer;

        public bool UserConnected { get; private set; }
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }
        private Client Client { get; set; }
        private Vehicule Vehicule { get; set; }
        public string Registration { get; private set; }
        public Reservation Reservation { get; set; }
        private int EstimateKm { get; set; }
        private Boolean LocationPending { get; set; }
        public Location()
        {
            /// si on utilisais la librairie au travers d'une application
            this._dataLayer = new DataLayer();
        }

        public Location(IDataLayer dataLayer)
        {
            this._dataLayer = dataLayer;
            this.LocationPending = false;
        }

        public string ConnectUser(string username, string password)
        {
            this.Client = this._dataLayer.Clients.SingleOrDefault(_ => _.Username == username);
            if (this.Client == null)
            {
                this.UserConnected = false;
                return "Username not recognized";
            }
            else
            {
                if (this.Client.Password == password)
                {
                    this.UserConnected = true;
                }
                else
                {
                    this.UserConnected = false;
                    return "Incorrect password";
                }
            }

            return "";
        }

        public void SetDates( DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Age( DateTime Birth )
        {
            TimeSpan span = DateTime.Now.Subtract(Birth);
            return span.Days / 365;
        }

        public List<Vehicule> GetVehicules()
        {
            List<Vehicule> vehicules = new List<Vehicule>();
            int age = Age(this.Client.Birth);

            if ( age < 21)
            {
                vehicules = _dataLayer.Vehicules.FindAll(_ => _.Horsepower <= 8);
            } 
            else if ( age >= 21 && age <= 25)
            {
                vehicules = _dataLayer.Vehicules.FindAll(_ => _.Horsepower <= 13);
            }
            else if ( age > 25)
            {
                vehicules = _dataLayer.Vehicules;
            }

            return vehicules;
        }

        public string CreateLocation()
        {
            string error = null;
            int age = Age(this.Client.Birth);
            if (age >= 18)
            {
                if ( this.Client.LicenseNumber != 0 && this.LocationPending == false )
                {
                    Vehicule = _dataLayer.Vehicules.FirstOrDefault(_ => _.Registration == this.Registration);
                    Reservation = new Reservation(this.Client, Vehicule, this.StartDate, this.EndDate, this.EstimateKm);
                    LocationPending = true;
                }
                else if (this.LocationPending == true)
                {
                    error = String.Format("Too location '{0}'", Reservation.Vehicule.Registration.ToString());
                }
                else
                {
                    error = "Invalid license";
                }
            }
            else
            {
                error = "-18";
            }

            return error;
        }

        public void SetSelectedClientVehicule(string registration)
        {
            this.Registration = registration;
        }

        public void SetEstimateKm( int estimateKm )
        {
            this.EstimateKm = estimateKm;
        }
    }
}
