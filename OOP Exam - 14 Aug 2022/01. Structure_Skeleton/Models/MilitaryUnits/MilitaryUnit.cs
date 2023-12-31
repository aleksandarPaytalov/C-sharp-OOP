﻿using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            Cost = cost;
            this.enduranceLevel = 1;
        }

        public double Cost { get => this.cost; private set => this.cost = value; }

        public int EnduranceLevel { get => this.enduranceLevel; private set => this.enduranceLevel = value; }

        public void IncreaseEndurance()
        {
            this.enduranceLevel++;

            if (this.enduranceLevel > 20)
            {
                this.enduranceLevel = 20;
                throw new ArgumentException(string.Format(ExceptionMessages.EnduranceLevelExceeded));
            }
        }
    }
}
