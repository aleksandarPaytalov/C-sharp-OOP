using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class UniversityRepository : IRepository<IUniversity>
    {
        private List<IUniversity> univercities;

        public UniversityRepository()
        {
            this.univercities = new List<IUniversity>();
        }

        public IReadOnlyCollection<IUniversity> Models => this.univercities;

        public void AddModel(IUniversity univercity)
        {
            univercities.Add(univercity);
        }

        public IUniversity FindById(int id)
        {
            IUniversity univercity = univercities.FirstOrDefault(u => u.Id == id);
            return univercity;
        }

        public IUniversity FindByName(string name)
        {
            IUniversity univercity = univercities.FirstOrDefault(u => u.Name == name);
            return univercity;
        }
    }
}
