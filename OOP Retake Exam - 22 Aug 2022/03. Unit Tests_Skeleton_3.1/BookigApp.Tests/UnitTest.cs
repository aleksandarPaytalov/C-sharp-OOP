using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1_CheckTheConstructor()
        {
            string expectedName = "Palas";
            int expectedCategory = 5;
            double expecterTurnOver = 0;

            Hotel hotel = new Hotel("Palas", 5);

            string actualName = hotel.FullName;
            int actualCategory = hotel.Category;
            double actualTurnOver = hotel.Turnover;

            Room room1 = new Room(4, 100);


            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedCategory, actualCategory);
            Assert.AreEqual(expecterTurnOver, actualTurnOver);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test2_CheckTheName_IvalidPameters(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(name, 5));
        }

        [TestCase(-1)]
        [TestCase(6)]
        public void Test3_CheckTheCategory_InvalidPameters(int category)
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Palas", category)); 
        }
        [Test]
        public void Test4_CheckIReadOnlyCollectionRoom_IsItReturnRightNumberOfRooms()
        {            
            Hotel hotel = new Hotel("Palas", 5);

            Room room1 = new Room(4, 100);
            Room room2 = new Room(3, 80);

            hotel.AddRoom(room1);
            hotel.AddRoom(room2);

            int expected = 2;
            int actual = hotel.Rooms.Count;

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Test5_CheckIReadOnlyCollectionBooking_IsItReturnRightNumberOfBookings()
        {
            Hotel hotel = new Hotel("Palas", 5);

            Room room1 = new Room(4, 100);
            Room room2 = new Room(3, 80);

            hotel.AddRoom(room1);

            Booking booking = new Booking(1, room1, 3);
            hotel.BookRoom(2, 2, 3, 350);
            int expected = 1;
            int actual = hotel.Bookings.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test6_CheckBookRoomMethod_InvalidAdultNumber()
        {
            Hotel hotel = new Hotel("Palas", 5);

            Room room1 = new Room(4, 100);
            hotel.AddRoom(room1);

            Booking booking = new Booking(1, room1, 3);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(-1, 2, 3, 350));  
        }

        [Test]
        public void Test7_CheckBookRoomMethod_InvalidChildrenNumber()
        {
            Hotel hotel = new Hotel("Palas", 5);

            Room room1 = new Room(4, 100);
            hotel.AddRoom(room1);

            Booking booking = new Booking(1, room1, 3);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, -1, 3, 350));
        }

        [Test]
        public void Test8_CheckBookRoomMethod_InvalidresidenceDuration()
        {
            Hotel hotel = new Hotel("Palas", 5);

            Room room1 = new Room(4, 100);
            hotel.AddRoom(room1);

            Booking booking = new Booking(1, room1, 3);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, 1, 0, 350));
        }

        [Test]
        public void Test9_CheckBookRoomMethod_WithValidInput()
        {
            Hotel hotel = new Hotel("Palas", 5);

            Room room1 = new Room(4, 100);
            hotel.AddRoom(room1);

            Booking booking = new Booking(hotel.Bookings.Count + 1, room1, 3);
            hotel.BookRoom(2, 1, 3, 350);
            double expectedTurnOver = 3 * 100;
            double actualTurnOver = booking.ResidenceDuration * room1.PricePerNight;

            Assert.AreEqual(expectedTurnOver, actualTurnOver);
        }

        [Test]
        public void Test10_CheckBedCapacity_WithBedCapacityNegative()
        {
            Hotel hotel = new Hotel("Palas", 5);

            Assert.Throws<ArgumentException>(() => new Room(-1, 100)); 
        }

        [Test]
        public void Test10_CheckPricePerNight_WithPriceNegative()
        {
            Hotel hotel = new Hotel("Palas", 5);

            Assert.Throws<ArgumentException>(() => new Room(2, -100));
        }

        [Test]
        public void Test11_CheckBookRoomMethod_IfBudgetIsToLow()
        {
            Hotel hotel = new Hotel("Palas", 5);

            Room room1 = new Room(4, 200);
            hotel.AddRoom(room1);

            Booking booking = new Booking(hotel.Bookings.Count + 1, room1, 3);
            hotel.BookRoom(2, 1, 3, 100);

            int expectedBookingNumber = 0;
            double actualBooking = hotel.Bookings.Count;

            double expectedTurnOver = 0;
            double actualTurnOver = hotel.Turnover;

            Assert.AreEqual(expectedBookingNumber, actualBooking);
            Assert.AreEqual(expectedTurnOver, actualTurnOver);
        }

        [Test]
        public void Test12_CheckBookRoomMethod_IfBedCapacityIsToLow()
        {
            Hotel hotel = new Hotel("Palas", 5);

            Room room1 = new Room(1, 200);
            hotel.AddRoom(room1);

            Booking booking = new Booking(hotel.Bookings.Count + 1, room1, 3);
            hotel.BookRoom(2, 1, 3, 600);

            int expectedBookingNumber = 0;
            double actualBooking = hotel.Bookings.Count;

            double expectedTurnOver = 0;
            double actualTurnOver = hotel.Turnover;

            Assert.AreEqual(expectedBookingNumber, actualBooking);
            Assert.AreEqual(expectedTurnOver, actualTurnOver);
        }
    }
}