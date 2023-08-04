using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
           
            [Test]
            public void Test1_CheckTheConstructor()
            {
                string name = "RepairShop";
                int mechanicsAvailable = 5;
                int carCount = 0;

                Garage garage = new Garage("RepairShop", 5);

                string actualName = garage.Name;
                int actualMechanics = garage.MechanicsAvailable;
                int actual = garage.CarsInGarage;
                
                Assert.AreEqual(name, actualName);
                Assert.AreEqual(mechanicsAvailable, actualMechanics);
                Assert.AreEqual(carCount, actual);
            }

            [TestCase("")]
            [TestCase(null)]
            public void Test2_CheckName_InvalidParameters(string name)
            {    
                Assert.Throws<ArgumentNullException>(() => new Garage(name, 5)); 
            }            

            [Test]
            public void Test4_CheckMechanics_ValidParameters()
            {
                Garage garage = new Garage("Poppy", 5);
                int expected = 5;
                int actual = garage.MechanicsAvailable;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Test5_CheckMEchanics_InValidParameters()
            {               
                Assert.Throws<ArgumentException>(() => new Garage("Poppy", 0));
            }

            [Test]
            public void Test6_CarInGarageCount_Working()
            {
                Garage garage = new Garage("Poppy", 5);
                Car car1 = new Car("BMW", 2);
                Car car2 = new Car("Mazda", 1);

                garage.AddCar(car1);
                garage.AddCar(car2);

                int expected = 2;
                int actual = garage.CarsInGarage;

                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Test7_AddCar_NoMechanicsAvailable()
            {
                Garage garage = new Garage("Poppy", 1);
                Car car1 = new Car("BMW", 2);
                Car car2 = new Car("Mazda", 1);

                garage.AddCar(car1);
                
                Assert.Throws<InvalidOperationException>(() => garage.AddCar(car2));
            }

            [Test]
            public void Test8_FixCar_CarDoNotExist()
            {
                Garage garage = new Garage("Poppy", 3);
                Car car1 = new Car("BMW", 2);

                garage.AddCar(car1);

                Assert.Throws<InvalidOperationException>(() => garage.FixCar("Tesla"));
            }

            [Test]
            public void Test9_FixCar_CarExist()
            {
                Garage garage = new Garage("Poppy", 3);
                Car car1 = new Car("BMW", 2);

                garage.AddCar(car1);
                garage.FixCar("BMW");

                int expected = 0;
                int actual = car1.NumberOfIssues;
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Test10_RemoveFixedCars_NoFixedCarsAvailable()
            {
                Garage garage = new Garage("Poppy", 3);
                Car car1 = new Car("BMW", 2);

                garage.AddCar(car1);

                Assert.Throws<InvalidOperationException>(() => garage.RemoveFixedCar());
            }

            [Test]
            public void Test11_RemoveFixedCars_FixedCarsAvailable()
            {
                Garage garage = new Garage("Poppy", 2);
                Car car1 = new Car("BMW", 1);
                Car car2 = new Car("Mazda", 1);

                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.FixCar(car1.CarModel);
                garage.FixCar(car2.CarModel);

                Assert.AreEqual(2, garage.RemoveFixedCar());
            }

            [Test]
            public void Test12_CarsReport_NotFixedCarsAvailable()
            {
                Garage garage = new Garage("Poppy", 2);
                Car car1 = new Car("BMW", 1);

                garage.AddCar(car1);
               
                string expected = "There are 1 which are not fixed: BMW.";
                Assert.AreEqual(expected, garage.Report());
            }

        }
    }
}