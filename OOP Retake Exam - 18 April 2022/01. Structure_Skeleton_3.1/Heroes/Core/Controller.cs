using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository weapons;

        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            var currentHero = heroes.FindByName(name);

            if (currentHero != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyExist, name));
            }

            if (type != nameof(Barbarian) && type != nameof(Knight))
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroTypeIsInvalid));
            }

            IHero hero;
            bool isBarb = true;
            if (type == nameof(Barbarian))
            {
                hero = new Barbarian(name, health, armour);
            }
            else
            {
                hero = new Knight(name, health, armour);
                isBarb = false;
            }

            this.heroes.Add(hero);
            if (isBarb)
            {
                return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
            }
            else
            {
                return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
            }
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            var currentWeapon = this.weapons.FindByName(name);

            if (currentWeapon != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponAlreadyExists, name));
            }
                        
            if (type != "Claymore" && type != "Mace")
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponTypeIsInvalid));
            }

            IWeapon weapon;
            if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
            }
            else
            {
                weapon = new Claymore(name, durability);
            }

            this.weapons.Add(weapon);
            return string.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var currentHero = this.heroes.FindByName(heroName);

            if (currentHero == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroDoesNotExist, heroName));
            }

            var currentWeapon = this.weapons.FindByName(weaponName);
            if (currentWeapon == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }

            if (currentHero.Weapon != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            }

            currentHero.AddWeapon(currentWeapon);
            this.weapons.Remove(currentWeapon);
            return string.Format(OutputMessages.WeaponAddedToHero, heroName, currentWeapon.GetType().Name.ToLower());
        }

        public string StartBattle()
        {
            Map map = new Map();

            var warriorsToFight = this.heroes.Models.Where(h => h.IsAlive && h.Weapon != null).ToList();
            return map.Fight(warriorsToFight); 
        }

        public string HeroReport()
        {
            var orderedHeroes = this.heroes.Models.OrderBy(h => h.GetType().Name)
                .ThenByDescending(h => h.Health)
                .ThenBy(h => h.Name);

            StringBuilder sb = new StringBuilder();

            foreach (var hero in orderedHeroes)
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");

                if (hero.Weapon != null)
                {
                    sb.AppendLine($"--Weapon: {hero.Weapon.Name}");
                }
                else
                {
                    sb.AppendLine($"--Weapon: Unarmed");
                }                
            }

            return sb.ToString().TrimEnd();
        }

    }
}
