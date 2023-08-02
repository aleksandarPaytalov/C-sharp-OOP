using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private PlanetRepository planets;

        public Controller()
        {
            this.planets = new PlanetRepository();
        }

        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = planets.FindByName(name);

            if (planet != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            IPlanet createPlanet = new Planet(name, budget);
            planets.AddItem(createPlanet);
            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string AddUnit(string unitName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (unitName != nameof(AnonymousImpactUnit) 
                && unitName != nameof(SpaceForces) 
                && unitName != nameof(StormTroopers))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitName));
            }

            if (planet.Army.Any(u => u.GetType().Name == unitName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitName, planetName));
            }

            IMilitaryUnit unit;

            if (unitName == nameof(AnonymousImpactUnit))
            {
                unit = new AnonymousImpactUnit();
            }
            else if (unitName == nameof(SpaceForces))
            {
                unit = new SpaceForces();
            }
            else 
            {
                unit = new StormTroopers();
            }

            planet.Spend(unit.Cost);
            planet.AddUnit(unit);

            return string.Format(OutputMessages.UnitAdded, unitName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            if (weaponTypeName != nameof(BioChemicalWeapon)
                && weaponTypeName != nameof(NuclearWeapon)
                && weaponTypeName != nameof(SpaceMissiles))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            IWeapon weapon;

            if (weaponTypeName == nameof(BioChemicalWeapon))
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else
            {
                weapon = new SpaceMissiles(destructionLevel);
            }

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);

        }

        public string ForcesReport()
        {
            var orderedPlanets = planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in orderedPlanets)
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = planets.FindByName(planetOne);
            IPlanet secondPlanet = planets.FindByName(planetTwo);

            double budgetLostFirstPlanet = firstPlanet.Budget / 2;
            double budgetLostSecondPlanet = secondPlanet.Budget / 2;

            double firstPlanetValue = firstPlanet.Army.Sum(a => a.Cost) + firstPlanet.Weapons.Sum(w => w.Price);
            double secondPlanetValue = secondPlanet.Army.Sum(a => a.Cost) + secondPlanet.Weapons.Sum(w => w.Price);

            var firstPlanedNuclear = firstPlanet.Weapons.FirstOrDefault(w => w.GetType().Name == nameof(NuclearWeapon));
            var secondPlanedNuclear = secondPlanet.Weapons.FirstOrDefault(w => w.GetType().Name == nameof(NuclearWeapon));

            double firstPlanetPower = firstPlanet.MilitaryPower;
            double secondPlanetPower = secondPlanet.MilitaryPower;

            if (firstPlanetPower > secondPlanetPower)
            {
                firstPlanet.Spend(budgetLostFirstPlanet);
                firstPlanet.Profit(budgetLostSecondPlanet);
                firstPlanet.Profit(secondPlanetValue);
                
                planets.RemoveItem(secondPlanet.Name);
                return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
            }

            else if (firstPlanetPower < secondPlanetPower)
            {
                secondPlanet.Spend(budgetLostSecondPlanet);
                secondPlanet.Profit(budgetLostFirstPlanet);
                secondPlanet.Profit(firstPlanetValue);

                planets.RemoveItem(firstPlanet.Name);
                return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
            }

            else
            {
                if (firstPlanedNuclear == null && secondPlanedNuclear == null)
                {
                    firstPlanet.Spend(budgetLostFirstPlanet);
                    secondPlanet.Spend(budgetLostSecondPlanet);

                    return string.Format(OutputMessages.NoWinner);
                }                

                else if (firstPlanedNuclear != null && secondPlanedNuclear == null)
                {
                    firstPlanet.Spend(budgetLostFirstPlanet);
                    firstPlanet.Profit(budgetLostSecondPlanet);
                    firstPlanet.Profit(secondPlanetValue);

                    planets.RemoveItem(secondPlanet.Name);
                    return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);                   
                }

                else if (firstPlanedNuclear == null && secondPlanedNuclear != null)
                {
                    secondPlanet.Spend(budgetLostSecondPlanet);
                    secondPlanet.Profit(budgetLostFirstPlanet);
                    secondPlanet.Profit(firstPlanetValue);

                    planets.RemoveItem(firstPlanet.Name);
                    return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
                }

                else
                {
                    firstPlanet.Spend(budgetLostFirstPlanet);
                    secondPlanet.Spend(budgetLostSecondPlanet);
                    return string.Format(OutputMessages.NoWinner);
                }
            }
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (!planet.Army.Any())
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
            }

            planet.TrainArmy();
            planet.Spend(1.25);
            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
