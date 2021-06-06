using System.Collections.Generic;

namespace LocationLibrary
{
    internal class DataLayer : IDataLayer
    {
        public List<Client> Clients { get; private set; }
        public List<Vehicule> Vehicules { get; private set; }

        public DataLayer()
        {
            this.Clients    = new List<Client>();
            this.Vehicules  = new List<Vehicule>();
        }
    }
}