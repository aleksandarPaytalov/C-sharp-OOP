using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void Test1_TestTheConstructor()
        {
            int expected = 5;
            string phoneName = "GSM";
            int batteryMaximumCharge = 50;

            Shop shop = new Shop(5);

            int actual = shop.Capacity;
            Smartphone phone1 = new Smartphone("GSM", 50);

            shop.Add(phone1);

            string actualPhoneName = phone1.ModelName;
            int ActualBateryMaximumCharge = phone1.MaximumBatteryCharge;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(phoneName, actualPhoneName);
            Assert.AreEqual(batteryMaximumCharge, ActualBateryMaximumCharge);
        }

        [Test]
        public void Test1_Capacity_InvalidParameters()
        {    
            var actual = Assert.Throws<ArgumentException>(() => new Shop(-10));

            Assert.AreEqual("Invalid capacity.", actual.Message);
        } //

        [Test]
        public void Test3_CheckCounterIsItReturningTheRightPhoneCount()
        {
            Shop shop = new Shop(5);

            Smartphone phone1 = new Smartphone("Iphone", 100);
            Smartphone phone2 = new Smartphone("Nokia", 100);

            shop.Add(phone1);
            shop.Add(phone2);

            int expected = 2;
            int actual = shop.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test4_CheckAddPhoneAlreadyExist()
        {
            Shop shop = new Shop(5);

            Smartphone phone1 = new Smartphone("Iphone", 100);
            Smartphone phone2 = new Smartphone("Iphone", 100);

            shop.Add(phone1);

            string expected = "The phone model Iphone already exist.";
            var actual = Assert.Throws< InvalidOperationException >(() => shop.Add(phone2));

            Assert.AreEqual(expected, actual.Message);
        }
        [Test]
        public void Test5_CheckAddCapacityIsFull()
        {
            Shop shop = new Shop(1);

            Smartphone phone1 = new Smartphone("Iphone", 100);
            Smartphone phone2 = new Smartphone("Nokia", 100);

            shop.Add(phone1);

            string expected = "The shop is full.";
            var actual = Assert.Throws<InvalidOperationException>(() => shop.Add(phone2));

            Assert.AreEqual(expected, actual.Message);
        }
        [Test]
        public void Test12_CheckIfPhoneIsAdded()
        {
            Shop shop = new Shop(1);

            Smartphone phone1 = new Smartphone("Iphone", 100);

            shop.Add(phone1);

            int expected = 1;
            int actual = shop.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test5_CheckRemove_IfPhoneDoNotExist()
        {
            Shop shop = new Shop(3);

            Smartphone phone1 = new Smartphone("Iphone", 100);
            shop.Add(phone1);

            string expected = "The phone model Motorola doesn't exist.";
            var actual = Assert.Throws<InvalidOperationException>(() => shop.Remove("Motorola"));

            Assert.AreEqual(expected, actual.Message);
        }

        [Test]
        public void Test6_CheckRemove_IfPhoneIsRemoved()
        {
            Shop shop = new Shop(3);

            Smartphone phone1 = new Smartphone("Iphone", 100);
            Smartphone phone2 = new Smartphone("Motorola", 90);
            shop.Add(phone1);
            shop.Add(phone2);
            shop.Remove("Iphone");

            int expected = 1;
            int actual = shop.Count;

            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Test7_TestPhone_IfPhoneBatteryChargeIsLowerThanTheBatteryUsage()
        {
            Shop shop = new Shop(3);

            Smartphone phone1 = new Smartphone("Iphone", 70);
            shop.Add(phone1);

            var actual = Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Iphone", 100)); 
            

            string expected = "The phone model Iphone is low on batery.";
           

            Assert.AreEqual(expected, actual.Message);
        }


        [Test]
        public void Test8_TestPhone_IfPhoneBatteryChargeIsHigherThanTheBatteryUsage()
        {
            Shop shop = new Shop(3);

            Smartphone phone1 = new Smartphone("Iphone", 100);
            shop.Add(phone1);

            shop.TestPhone("Iphone", 80);


            int expected = 20;
            int actual = phone1.CurrentBateryCharge;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test11_TestPhone_IfPhoneDoNotExist()
        {
            Shop shop = new Shop(3);

            Smartphone phone1 = new Smartphone("Iphone", 100);
            shop.Add(phone1);

            string expected = "The phone model Motorola doesn't exist.";
            var actual = Assert.Throws<InvalidOperationException>(() => shop.TestPhone("Motorola", 80)); 

            Assert.AreEqual(expected, actual.Message);
        }

        [Test]
        public void Test9_ChargePhone_CheckIfTheMaximumBatteryCapacityIsReached()
        {
            Shop shop = new Shop(3);

            Smartphone phone1 = new Smartphone("Iphone", 100);
            shop.Add(phone1);
            shop.TestPhone("Iphone", 80);
            shop.ChargePhone("Iphone");


            int expected = 100;
            int actual = phone1.CurrentBateryCharge;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test10_ChargePhone_CheckWhenPhoneDoNotExist()
        {
            Shop shop = new Shop(3);

            Smartphone phone1 = new Smartphone("Iphone", 100);
            shop.Add(phone1);

            string expected = "The phone model Motorola doesn't exist.";
            var actual = Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("Motorola"));

            Assert.AreEqual(expected, actual.Message);
        }
    }
}