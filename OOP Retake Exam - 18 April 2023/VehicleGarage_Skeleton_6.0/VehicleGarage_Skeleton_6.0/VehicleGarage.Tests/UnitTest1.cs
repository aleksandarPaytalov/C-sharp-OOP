using NUnit.Framework;
using System.Linq;

namespace VehicleGarage.Tests
{
    public class Tests
    {
       
       

        [Test]
        public void CheckIfTheAddingOfNewCarIsWorkingWithValidParameters()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            garage.AddVehicle(vehicle);

            int expectedVehicleCount = 1;
            int actualVehicleCount = garage.Vehicles.Count;

            Assert.AreEqual(expectedVehicleCount, actualVehicleCount);
        }

        [Test]
        public void CheckIfTheCapacityOfTheGarageIsWorkingIfWeTryToAddMoreCarsThanTheCapacity()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            Vehicle vehicle1 = new Vehicle("VW", "Passat", "PB9999RW");
            Vehicle vehicle2 = new Vehicle("VW", "Golf", "PB9999AP");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            int expectedVehicleCount = 2;
            int actualVehicleCount = garage.Vehicles.Count;

            Assert.AreEqual(expectedVehicleCount, actualVehicleCount);
        }

        [Test]
        public void CheckIfWeCanAddNewCarWhenTheGivenLicensePlateNumberExistInTheCollection()
        {
            Garage garage = new Garage(2);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            Vehicle vehicle1 = new Vehicle("VW", "Passat", "PB5555CT");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle1);

            int expectedVehicleCount = 1;
            int actualVehicleCount = garage.Vehicles.Count;

            Assert.AreEqual(expectedVehicleCount, actualVehicleCount);
        }

        [Test]
        public void CheckIfCounterOfChargedVehiclesIsWorkingWithInvalidParametersForSomeOfTheCars()
        {
            Garage garage = new Garage(3);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            Vehicle vehicle1 = new Vehicle("VW", "Passat", "PB5995CT");
            Vehicle vehicle2 = new Vehicle("VW", "Golf", "PB9999AP");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            vehicle.BatteryLevel = 50;
            vehicle1.BatteryLevel = 90;
            vehicle2.BatteryLevel = 99;

            int expectedVehicleCount = 1;
            int actualVehicleCount = garage.ChargeVehicles(80);

            Assert.AreEqual(expectedVehicleCount, actualVehicleCount);
        }

        [Test]
        public void CheckIfBatteryIsFullyChargedIfParametersAreValid()
        {
            Garage garage = new Garage(3);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            garage.AddVehicle(vehicle);
            vehicle.BatteryLevel = 50;
            garage.ChargeVehicles(80);

            int expectedBatteryLevel = 100;
            int actualBatteryLevel = vehicle.BatteryLevel;

            Assert.AreEqual(expectedBatteryLevel, actualBatteryLevel);
        }

        [Test]
        public void CheckFindMethodByLicensePlateNumber()
        {
            Garage garage = new Garage(3);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            garage.AddVehicle(vehicle);

            string expecteResult = "PB5555CT";
            var car = garage.Vehicles.FirstOrDefault(l => l.LicensePlateNumber == "PB5555CT");
            string actualResult = car.LicensePlateNumber;

            Assert.AreEqual(expecteResult, actualResult);
        }

        [Test]
        public void CheckDriveMethodIfVehicleIsDamaged()
        {
            Garage garage = new Garage(3);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            garage.AddVehicle(vehicle);

            garage.DriveVehicle("PB5555CT", 70, true);
            garage.DriveVehicle("PB5555CT", 70, true);
            int expectedResult = 30;
            int actualResult = vehicle.BatteryLevel;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CheckDriveMethodIfBatteryDrainageIsGreaterThanOneHundred ()
        {
            Garage garage = new Garage(3);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            garage.AddVehicle(vehicle);
            vehicle.BatteryLevel = 70;
            garage.DriveVehicle("PB5555CT", 101, false);
            int exoectedResult = 70;
            int actualResult = vehicle.BatteryLevel;

            Assert.AreEqual(exoectedResult, actualResult);
        }
        [Test]
        public void CheckDriveMethodIfBatteryDrainageIsGreaterThanBatteryLevelOfTheCar()
        {
            Garage garage = new Garage(3);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            garage.AddVehicle(vehicle);

            garage.DriveVehicle("PB5555CT", 90, false);
            garage.DriveVehicle("PB5555CT", 90, false);
            int exoectedResult = 10;
            int actualResult = vehicle.BatteryLevel;

            Assert.AreEqual(exoectedResult, actualResult);
        }

        [Test]
        public void CheckDriveMethodWithValidParameters()
        {
            Garage garage = new Garage(3);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            garage.AddVehicle(vehicle);

            garage.DriveVehicle("PB5555CT", 60, false);
            garage.DriveVehicle("PB5555CT", 60, false);

            int exoectedResult = 40;
            int actualResult = vehicle.BatteryLevel;

            Assert.AreEqual(exoectedResult, actualResult);
        }

        [Test]
        public void CheckDriveMethodWhenAccidentIsHappened()
        {
            Garage garage = new Garage(3);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("PB5555CT", 60, true);
            bool actualResult = vehicle.IsDamaged;
           
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void CheckTheCounterOfRepairedVehicelsWithValidParameters()
        {
            Garage garage = new Garage(5);

            Vehicle vehicle = new Vehicle("BMW", "M5", "PB5555CT");
            Vehicle car = new Vehicle("VW", "Passat", "PB6666CT");
            Vehicle truck = new Vehicle("Fiat", "Bravo", "PB7777CT");

            garage.AddVehicle(vehicle);
            garage.AddVehicle(car);
            garage.AddVehicle(truck);

            garage.DriveVehicle("PB5555CT", 55, true);
            garage.DriveVehicle("PB6666CT", 55, true);
            garage.DriveVehicle("PB7777CT", 55, false);


            var expectedResult = "Vehicles repaired: 2";
            var actualResult = garage.RepairVehicles();

            bool vehicleIsRepaired = vehicle.IsDamaged;
            bool carIsRepaired = car.IsDamaged;
            bool truckIsRepaired = truck.IsDamaged;

            Assert.AreEqual(expectedResult, actualResult);
            Assert.IsFalse(vehicleIsRepaired);
            Assert.IsFalse(carIsRepaired);
            Assert.IsFalse(truckIsRepaired);
        }
    }
}