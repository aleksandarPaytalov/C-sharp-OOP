using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Utilities.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        //private List<IHero> heroes; 

        public string Fight(ICollection<IHero> heroes)
        {           
            List<IHero> knightsTeam = new List<IHero>();
            List<IHero> barbariansTeam = new List<IHero>();


            foreach (var hero in heroes)
            {
                var typeName = hero.GetType().Name;

                if (typeName == nameof(Knight))
                {
                    knightsTeam.Add(hero);
                }
                else
                {
                    barbariansTeam.Add(hero);
                }
            }

            bool isKnightsTurn = true;

            while (barbariansTeam.Any(b => b.IsAlive) && knightsTeam.Any(k => k.IsAlive))
            {

                if (isKnightsTurn)
                {
                    CurrentMap(knightsTeam, barbariansTeam);
                    isKnightsTurn = false;
                }
                else
                {
                    CurrentMap(barbariansTeam, knightsTeam);
                    isKnightsTurn = true;
                }
            }

            if (knightsTeam.Any(k => k.IsAlive))
            {
                return $"The knights took {knightsTeam.Where(k => !k.IsAlive).Count()} casualties but won the battle.";
            }
            else
            {
                return $"The barbarians took {barbariansTeam.Where(k => !k.IsAlive).Count()} casualties but won the battle.";
            }
        }

        private void CurrentMap(List<IHero> attacks, List<IHero> defenses)
        {
            foreach (var atack in attacks.Where(a => a.IsAlive))
            {
                foreach (var defense in defenses.Where(d => d.IsAlive))
                {
                    defense.TakeDamage(atack.Weapon.DoDamage());
                }
            }
        }
    }
}
