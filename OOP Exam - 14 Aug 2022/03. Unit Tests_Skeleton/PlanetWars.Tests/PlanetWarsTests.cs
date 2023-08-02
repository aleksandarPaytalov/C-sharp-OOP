using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Linq;
using System.Numerics;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [TestCase("")]
            [TestCase(null)]
            public void CheckTheNameProperty_InvalidInput(string name)
            {
                string expectedMessage = "Invalid planet Name";

                var actualMessage = Assert.Throws<ArgumentException>(() => new Planet(name, 10));

                Assert.AreEqual(expectedMessage, actualMessage.Message);
            }
            [Test]
            public void CheckTheNameProperty_ValidInput()
            {
                string expected = "Earth";

                Planet planet = new Planet("Earth", 1000);
                string actual = planet.Name;

                Assert.AreEqual(expected, actual);
            }

            [TestCase(-20)]
            [TestCase(-1)]
            public void CheckTheBudgetProperty_InvalidInput(double budget) // а ако е 0?
            {
                string expected = "Budget cannot drop below Zero!";

                var actual = Assert.Throws<ArgumentException>(() => new Planet("Earth", budget));

                Assert.AreEqual(expected, actual.Message);
            }

            [Test]
            public void CheckTheBudgetProperty_ValidInput()
            {
                double expected = 1000;

                Planet planet = new Planet("Earth", 1000);
                double actual = planet.Budget;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void IReadOnlyCollectionIsReturningCorrectNumberOfWeapons()
            {
                Planet planet = new Planet("Earth", 1000);

                Weapon weapon1 = new Weapon("Missle", 200, 5);
                Weapon weapon2 = new Weapon("Bomb", 500, 8);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                int expected = 2;
                int actual = planet.Weapons.Count;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void MilitaryPowerRatio_CheckIfReturnTheRightSum()
            {
                Planet planet = new Planet("Earth", 1000);

                Weapon weapon1 = new Weapon("Missle", 200, 5);
                Weapon weapon2 = new Weapon("Bomb", 500, 8);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                int expected = 13;
                int actual = planet.Weapons.Sum(w => w.DestructionLevel);

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Profit_CheckIfReturnTheRightSum()
            {
                Planet planet = new Planet("Earth", 1000);
                planet.Profit(800);

                double expected = 1800;
                double actual = planet.Budget;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void SpendFunds_CheckIfReturnTheRightValueWhenBudgetIsEnough()
            {
                Planet planet = new Planet("Earth", 1000);

                planet.SpendFunds(800);

                double expected = 200;
                double actual = planet.Budget;

                Assert.AreEqual(expected, actual);
            }
            [Test]
            public void SpendFunds_CheckIfReturnTheRightMessageWhenBudgetIsNotEnough()
            {
                Planet planet = new Planet("Earth", 1000);

                

                string expected = "Not enough funds to finalize the deal.";
                var actual = Assert.Throws<InvalidOperationException>(() => planet.SpendFunds(1200));

                Assert.AreEqual(expected, actual.Message);
            }

            [Test]
            public void AddWeapon_CheckIfReturnTheRightMessageWhenWeaponExist()
            {
                Planet planet = new Planet("Earth", 1000);

                Weapon weapon1 = new Weapon("Missle", 200, 5);
                Weapon weapon2 = new Weapon("Bomb", 500, 8);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                string expected = "There is already a Missle weapon.";
                var actual = Assert.Throws<InvalidOperationException>(() => planet.AddWeapon(weapon1));

                Assert.AreEqual(expected, actual.Message);
            }

            [Test]
            public void RemoveWeapon_CheckIfReturnTheRightCountWhenWeapon()
            {
                Planet planet = new Planet("Earth", 1000);

                Weapon weapon1 = new Weapon("Missle", 200, 5);
                Weapon weapon2 = new Weapon("Bomb", 500, 8);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                var expected = 1;
                planet.RemoveWeapon("Bomb");
                var actual = planet.Weapons.Count;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void UpgradeWeapon_CheckIfReturnTheRightMessageIfWeaponDoNotExist()
            {
                Planet planet = new Planet("Earth", 1000);

                Weapon weapon1 = new Weapon("Missle", 200, 5);
                Weapon weapon2 = new Weapon("Bomb", 500, 8);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                var expected = "Machine gun does not exist in the weapon repository of Earth";
                var actual = Assert.Throws<InvalidOperationException> (() => planet.UpgradeWeapon("Machine gun"));                 

                Assert.AreEqual(expected, actual.Message);
            }

            [Test]
            public void UpgradeWeapon_CheckIfReturnTheRightDestructionLevelIfWeaponExist()
            {
                Planet planet = new Planet("Earth", 1000);

                Weapon weapon1 = new Weapon("Missle", 200, 5);
                Weapon weapon2 = new Weapon("Bomb", 500, 8);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                var expected = 6;
                planet.UpgradeWeapon("Missle");
                var actual = weapon1.DestructionLevel;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void IncreaseDestructionLevel_CheckIfReturnTheRightDestructionLevelAmount()
            {
                Planet planet = new Planet("Earth", 1000);


                Weapon weapon1 = new Weapon("Missle", 200, 5);
                Weapon weapon2 = new Weapon("Bomb", 500, 8);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);

                var expected = 6;
                weapon1.IncreaseDestructionLevel();
                var actual = weapon1.DestructionLevel;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void DestructOpponent_CheckIfReturnTheRightMessageIfOponentPowerIsToBig()
            {
                Planet planet1 = new Planet("Earth", 1000);
                Planet planet2 = new Planet("Mars", 5000);

                Weapon weapon1 = new Weapon("Missle", 200, 5);
                Weapon weapon2 = new Weapon("Bomb", 500, 8);

                planet1.AddWeapon(weapon1);
                planet2.AddWeapon(weapon2);

                var expected = $"Mars is too strong to declare war to!";
                var actual = Assert.Throws<InvalidOperationException>(() => planet1.DestructOpponent(planet2));

                Assert.AreEqual(expected, actual.Message);
            }

            [Test]
            public void DestructOpponent_CheckIfReturnTheRightMessageIfOponentPowerIsLessThanTheAttacker()
            {
                Planet planet1 = new Planet("Earth", 1000);
                Planet planet2 = new Planet("Mars", 800);

                Weapon weapon1 = new Weapon("Missle", 200, 9);
                Weapon weapon2 = new Weapon("Bomb", 500, 8);

                planet1.AddWeapon(weapon1);
                planet2.AddWeapon(weapon2);

                var expected = "Mars is destructed!";
                var actual = planet1.DestructOpponent(planet2);
            
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void WeaponPrice_CheckIfReturnTheRightMessageIfPriceIsNegative()
            {
                var actual = Assert.Throws< ArgumentException>(() => new Weapon("Missle", -200, 9));
                            
                var expected = "Price cannot be negative.";

                Assert.AreEqual(expected, actual.Message);
            }


            [Test]
            public void IsNuclear_IfWeaponDestructionLevelIsgreaterorEqualTen()
            {
                Weapon weapon = new Weapon("Missle", 200, 11);

                var actual = weapon.IsNuclear;

                Assert.IsTrue(actual);
            }
        }
    }
}
