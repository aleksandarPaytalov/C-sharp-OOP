using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planets;

        public PlanetRepository()
        {
            this.planets = new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => this.planets;

        public void AddItem(IPlanet planet)
        {
            planets.Add(planet);
        }

        public IPlanet FindByName(string planetName)
        {
            IPlanet planet = planets.FirstOrDefault(p => p.Name == planetName);

            return planet;
        }

        public bool RemoveItem(string planetName)
        {
            IPlanet planet = planets.FirstOrDefault(w => w.Name == planetName);

            if (planet == null)
            {
                return false;
            }
            else
            {
                planets.Remove(planet);
                return true;
            }
        }
    }
}
