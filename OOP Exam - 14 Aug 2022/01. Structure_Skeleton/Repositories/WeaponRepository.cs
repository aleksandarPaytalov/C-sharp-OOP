using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> weapons;

        public WeaponRepository()
        {
            this.weapons = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => this.weapons;

        public void AddItem(IWeapon weapon) => this.weapons.Add(weapon);

        public IWeapon FindByName(string name)
        {
            IWeapon weapon = weapons.FirstOrDefault(w => w.GetType().Name == name);

            return weapon;
        }

        public bool RemoveItem(string name)
        {
            IWeapon weapon = weapons.FirstOrDefault(w => w.GetType().Name == name);

            if (weapon == null)
            {
                return false;
            }
            else
            {
                weapons.Remove(weapon);
                return true;
            }
        }
    }
}
