using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class BookingRepository : IRepository<IBooking>
    {
        private List<IBooking> bookings;

        public BookingRepository()
        {
            this.bookings = new List<IBooking>();
        }

        public void AddNew(IBooking booking) => bookings.Add(booking);

        public IReadOnlyCollection<IBooking> All() => this.bookings;

        public IBooking Select(string bookingNumber) => bookings.FirstOrDefault(b => b.BookingNumber.ToString() == bookingNumber);
    }
}
