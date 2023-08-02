using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        private List<ISubject> subjects;

        public SubjectRepository()
        {
            this.subjects = new List<ISubject>();
        }

        public IReadOnlyCollection<ISubject> Models => this.subjects;

        public void AddModel(ISubject subject)
        {
            subjects.Add(subject);
        }

        public ISubject FindById(int id)
        {
            var currentSubject = subjects.FirstOrDefault(s => s.Id == id);
            return currentSubject;
        }

        public ISubject FindByName(string name)
        {
            ISubject currentSubject = subjects.FirstOrDefault(s => s.Name == name);
            return currentSubject;
        }
    }
}
