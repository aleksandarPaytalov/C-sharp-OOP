using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public class TechnicalSubject : Subject
    {
        private const double rate = 1.3;
        public TechnicalSubject(int id, string name) : base(id, name, rate)
        {
        }
    }
}
