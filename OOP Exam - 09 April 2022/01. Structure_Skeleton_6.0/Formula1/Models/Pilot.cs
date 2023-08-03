using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullName;
        //private bool canRace = false;
        private IFormulaOneCar car;

        public Pilot(string fullName)
        {
            FullName = fullName;
        }

        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                }

                this.fullName = value;
            }
        }
        public IFormulaOneCar Car
        {
            get => this.car;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCarForPilot));
                }

                this.car = value;
            }

        }

        public int NumberOfWins { get; private set; }

        public bool CanRace { get; private set; } = false;

        public void AddCar(IFormulaOneCar car)
        {
            this.car = car;
            CanRace = true;
        }

        public void WinRace() => NumberOfWins++; 

        public override string ToString()
        {
            return $"Pilot {FullName} has {NumberOfWins} wins.";
        }

    }
}
