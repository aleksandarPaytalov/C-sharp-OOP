using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private HotelRepository hotels;
        private BookingRepository bookings;
        private RoomRepository rooms;

        public Controller()
        {
            this.hotels = new HotelRepository();
            this.bookings = new BookingRepository();
            this.rooms = new RoomRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            IHotel hotel = hotels.Select(hotelName);

            if (hotel != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }
            
            hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }
        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            
            if (hotels.Select(hotelName) == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel hotel = hotels.Select(hotelName);
            if (hotel.Rooms.Select(roomTypeName) != null)
            {
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);
            }

            if (roomTypeName != nameof(Apartment) && roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            IRoom room;
            if (roomTypeName == nameof(Apartment))
            {
                room = new Apartment();
            }
            else if (roomTypeName == nameof(DoubleBed))
            {
                room = new DoubleBed();
            }
            else
            {
                room = new Studio();
            }
            
            hotel.Rooms.AddNew(room);

            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }
        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {

            IHotel hotel = hotels.Select(hotelName);
            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            if (roomTypeName != nameof(Apartment) && roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            var hotelRoomAlreadyCreated = hotel.Rooms.Select(roomTypeName);
            if (hotelRoomAlreadyCreated == null)
            {
                return string.Format(OutputMessages.RoomTypeNotCreated);
            }

            if (hotelRoomAlreadyCreated.PricePerNight != 0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
            }

            hotelRoomAlreadyCreated.SetPrice(price);

            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {         
            int totalGuest = adults + children;
            var checkCategory = hotels.All().FirstOrDefault(h => h.Category == category);

            if (checkCategory == null)
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }

            var hotelsOrdered = hotels.All().Where(h => h.Category == category).OrderBy(h => h.FullName);
            foreach (var hotel in hotelsOrdered)
            {
                var roomToSelect = hotel.Rooms.All().Where(r => r.PricePerNight > 0)
                    .OrderBy(b => b.BedCapacity)
                    .Where(r => r.BedCapacity >= totalGuest).FirstOrDefault();

                if (roomToSelect != null)
                {
                    int bookingNumber = hotel.Bookings.All().Count + 1;
                    IBooking booking = new Booking(roomToSelect, duration, adults, children, bookingNumber);
                    hotel.Bookings.AddNew(booking);
                    return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotel.FullName);
                }
            }
           
            return string.Format(OutputMessages.RoomNotAppropriate);
        }

        public string HotelReport(string hotelName)
        {
            StringBuilder sb = new StringBuilder();

            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            else
            {
                sb.AppendLine($"Hotel name: {hotelName}");
                sb.AppendLine($"--{hotels.All().FirstOrDefault(h => h.FullName == hotelName).Category} star hotel");
                sb.AppendLine($"--Turnover: {hotels.All().FirstOrDefault(h => h.FullName == hotelName).Turnover:F2} $");
                sb.AppendLine("--Bookings:");

                var bookings = hotels.All().FirstOrDefault(h => h.FullName == hotelName).Bookings;
                if (!bookings.All().Any())
                {
                    sb.AppendLine();
                    sb.AppendLine("none");
                }
                else
                {
                    foreach (var booking in bookings.All())
                    {
                        sb.AppendLine();
                        sb.AppendLine(booking.BookingSummary());
                    }
                }                

                return sb.ToString().TrimEnd();
            }
        }
    }
}
