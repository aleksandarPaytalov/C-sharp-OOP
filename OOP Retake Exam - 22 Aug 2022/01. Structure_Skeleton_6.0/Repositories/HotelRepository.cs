using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class HotelRepository : IRepository<IHotel>
    {
        private List<IHotel> hotels;

        public HotelRepository()
        {
            this.hotels = new List<IHotel>();
        }

        public void AddNew(IHotel hotel) => hotels.Add(hotel);

        public IReadOnlyCollection<IHotel> All() => this.hotels;

        public IHotel Select(string name) => hotels.FirstOrDefault(h => h.FullName == name);
    }
}
