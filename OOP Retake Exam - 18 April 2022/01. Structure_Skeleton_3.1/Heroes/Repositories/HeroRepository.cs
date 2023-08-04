using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private List<IHero> heroes;

        public HeroRepository()
        {
            this.heroes = new List<IHero>();
        }

        public IReadOnlyCollection<IHero> Models => this.heroes;

        public void Add(IHero hero) => this.heroes.Add(hero);

        public IHero FindByName(string name) => this.heroes.FirstOrDefault(h => h.Name == name);

        public bool Remove(IHero hero) => this.heroes.Remove(hero);
    }
}
