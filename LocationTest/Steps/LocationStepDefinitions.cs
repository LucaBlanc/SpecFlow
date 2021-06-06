using TechTalk.SpecFlow;
using FluentAssertions;
using LocationLibrary;
using LocationTest.Fake;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocationTest.Steps
{
    [Binding]
    public sealed class LocationStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        private string _username;
        private string _password;
        private string _lastErrorMessage;
        private Location _target;
        private FakeDataLayer _fakeDataLayer;
        private List<Vehicule> _vehicules;
        private Reservation _reservation;

        public LocationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._fakeDataLayer = new FakeDataLayer();
            this._target = new Location(this._fakeDataLayer);
            this._vehicules = new List<Vehicule>();
        }

        #region Background

        [Given(@"following existing clients")]
        public void GivenFollowingExistingClients(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                this._fakeDataLayer.Clients.Add(new Client(row[0], row[1], DateTime.Parse(row[2]), int.Parse(row[3]), DateTime.Parse(row[4])));
            }
        }

        [Given(@"following cars")]
        public void GivenFollowingCars(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                this._fakeDataLayer.Vehicules.Add(new Vehicule(row[0], row[1], row[2], row[3], double.Parse(row[4]), double.Parse(row[5]), int.Parse(row[6])));
            }
        }

        #endregion

        [Given(@"my username is ""(.*)""")]
        public void GivenMyUsernameIs(string username)
        {
            this._username = username;
        }

        [Given(@"my password is ""(.*)""")]
        public void GivenMyPasswordIs(string password)
        {
            this._password = password;
        }

        [When(@"I try to connect to my account")]
        public void WhenITryToConnectToMyAccount()
        {
            this._lastErrorMessage = this._target.ConnectUser(this._username, this._password);
        }

        [Then(@"the connection is refused")]
        public void ThenTheConnectionIsRefused()
        {
            this._target.UserConnected.Should().BeFalse();
        }

        [Then(@"the error message is ""(.*)""")]
        public void ThenTheErrorMessageIs(string errorMessage)
        {
            this._lastErrorMessage.Should().Be(errorMessage);
        }

        [Then(@"the connection is established")]
        public void ThenTheConnectionIsEstablished()
        {
            this._target.UserConnected.Should().BeTrue();
        }

        [Given(@"following location dates")]
        public void GivenFollowingLocationDates(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                this._target.SetDates(DateTime.Parse(row[0]), DateTime.Parse(row[1]));
            }
        }

        [When(@"set location dates")]
        public void WhenSetLocationDates()
        {
            this._vehicules = _target.GetVehicules();
        }

        [Then(@"the vehicules list should be")]
        public void ThenTheVehiculesListShouldBe(Table table)
        {
            List<Vehicule> VehiculesList = new List<Vehicule>();
            foreach (TableRow row in table.Rows)
            {
                VehiculesList.Add(new Vehicule(row[0], row[1], row[2], row[3], double.Parse(row[4]), double.Parse(row[5]), int.Parse(row[6])));
            }

            _vehicules.Should().BeEquivalentTo(VehiculesList);
        }

        [Given(@"the vehicule is ""(.*)""")]
        public void GivenTheVehiculeIs(string registration)
        {
            this._target.SetSelectedClientVehicule(registration);
        }

        [When(@"set location")]
        public void WhenSetLocation()
        {
            this._lastErrorMessage = _target.CreateLocation();
            this._reservation = _target.Reservation;
        }

        [Then(@"the location should be")]
        public void ThenThelocationShouldBe(Table table)
        {
            Reservation reservation = new Reservation(_fakeDataLayer.Clients.FirstOrDefault(_ => _.Username == table.Rows[0][0]), _fakeDataLayer.Vehicules.FirstOrDefault(_ => _.Registration == table.Rows[0][1]),DateTime.Parse(table.Rows[0][2]), DateTime.Parse(table.Rows[0][3]), int.Parse(table.Rows[0][4]));
            
            _reservation.Should().BeEquivalentTo(reservation);
        }

        [Given(@"estimate km to ""(.*)""")]
        public void GivenEstimateTheNumberOfKmTo(int estimateKm)
        {
            _target.SetEstimateKm(estimateKm);
        }
    }
}