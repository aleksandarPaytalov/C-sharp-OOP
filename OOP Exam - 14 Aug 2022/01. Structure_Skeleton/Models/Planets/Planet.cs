using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        private double militaryPower;

        private UnitRepository units;
        private WeaponRepository weapons;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;

            this.units = new UnitRepository();
            this.weapons = new WeaponRepository();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPlanetName));
                }

                this.name = value;
            }         
        }

        public double Budget
        {
            get => this.budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidBudgetAmount));
                }

                this.budget = value;
            }
        }
        public double MilitaryPower => this.militaryPower = CalculateMilitaryPower();
        
        public IReadOnlyCollection<IMilitaryUnit> Army => this.units.Models;

        public IReadOnlyCollection<IWeapon> Weapons => this.weapons.Models;

        public void AddUnit(IMilitaryUnit unit) => this.units.AddItem(unit);

        public void AddWeapon(IWeapon weapon) => this.weapons.AddItem(weapon);

        public void TrainArmy()
        {
            foreach (var unit in this.units.Models)
            {
                unit.IncreaseEndurance();
            }
        }
        public void Spend(double amount)
        {
            if (Budget < amount)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnsufficientBudget));
            }

            Budget -= amount;
        }
        public void Profit(double amount)
        {
            Budget += amount;
        }
        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");

            if (!units.Models.Any()) //this.Army.Count == 0
            {
                sb.AppendLine($"--Forces: No units");
            }
            else 
            {
                var units = new Queue<string>();

                foreach (var unit in Army)
                {
                     units.Enqueue(unit.GetType().Name); 
                }

                sb.AppendLine($"--Forces: {string.Join(", ", units)}");
            }

            if (!weapons.Models.Any())
            {
                sb.AppendLine($"--Combat equipment: No weapons");
            }
            else 
            {
                var weapons = new Queue<string>();

                foreach (var unit in Weapons)
                {
                    weapons.Enqueue(unit.GetType().Name);
                }

                sb.AppendLine($"--Combat equipment: {string.Join(", ", weapons)}");
            }

            sb.AppendLine($"--Military Power: {this.militaryPower}");

            return sb.ToString().TrimEnd();
        }

        private double CalculateMilitaryPower()
        {
            double totalMilitaryEndurance = Army.Sum(e => e.EnduranceLevel);
            double totalWeaponDestructionLevel = Weapons.Sum(w => w.DestructionLevel);
            double totalPlanetPower = totalMilitaryEndurance + totalWeaponDestructionLevel;

            if (units.Models.Any(u => u.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                totalPlanetPower *= 1.30;
            }
            if (weapons.Models.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
            {
                totalPlanetPower *= 1.45;
            }

            double totalPlanetPowerRounded = Math.Round(totalPlanetPower, 3);

            return totalPlanetPowerRounded;
        }

    }
}
