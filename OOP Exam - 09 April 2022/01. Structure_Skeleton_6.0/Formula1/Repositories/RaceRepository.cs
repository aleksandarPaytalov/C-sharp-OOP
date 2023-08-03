using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        private List<IRace> races;

        public RaceRepository()
        {
            this.races = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models => this.races;

        public void Add(IRace race) => this.races.Add(race);

        public IRace FindByName(string name) => this.races.FirstOrDefault(r => r.RaceName == name);

        public bool Remove(IRace race) => this.races.Remove(race);
    }
}
