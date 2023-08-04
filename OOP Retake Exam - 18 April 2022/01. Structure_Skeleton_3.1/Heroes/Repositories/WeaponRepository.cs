using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> weapons;

        public WeaponRepository()
        {
            this.weapons = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models => this.weapons;

        public void Add(IWeapon weapon) => this.weapons.Add(weapon);

        public IWeapon FindByName(string name) => this.weapons.FirstOrDefault(w => w.Name == name);

        public bool Remove(IWeapon weapon) => this.weapons.Remove(weapon);
    }
}
