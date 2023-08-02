﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;

namespace UniversityCompetition.Models
{
    public class HumanitySubject : Subject
    {
        private const double rate = 1.15;

        public HumanitySubject(int id, string name) : base(id, name, rate)
        {
        }
    }
}
