using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class RoomRepository : IRepository<IRoom>
    {
        private List<IRoom> rooms;

        public RoomRepository()
        {
            this.rooms = new List<IRoom>();
        }

        public void AddNew(IRoom room) => rooms.Add(room);        

        public IReadOnlyCollection<IRoom> All() => this.rooms;

        public IRoom Select(string roomTypeName) => rooms.FirstOrDefault(r => r.GetType().Name == roomTypeName);
    }
}
