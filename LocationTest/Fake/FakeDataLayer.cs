using LocationLibrary;
using System.Collections.Generic;

namespace LocationTest.Fake
{
    class FakeDataLayer : IDataLayer
    {
        public List<Client> Clients { get; set; }

        public List<Vehicule> Vehicules { get; set; }

        public FakeDataLayer()
        {
            this.Clients    = new List<Client>();
            this.Vehicules  = new List<Vehicule>();
        }
    }
}
