using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{   
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private List<IMilitaryUnit> units;

        public UnitRepository()
        {
            this.units = new List<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models => this.units;

        public void AddItem(IMilitaryUnit unit)
        {
            this.units.Add(unit);
        }

        public IMilitaryUnit FindByName(string name)
        {
            IMilitaryUnit unit = units.FirstOrDefault(u => u.GetType().Name == name);

            return unit;        
        }

        public bool RemoveItem(string name)
        {
            IMilitaryUnit unit = units.FirstOrDefault(w => w.GetType().Name == name);

            if (unit == null)
            {
                return false;
            }
            else
            {
                units.Remove(unit);
                return true;
            }
        }
    }
}
