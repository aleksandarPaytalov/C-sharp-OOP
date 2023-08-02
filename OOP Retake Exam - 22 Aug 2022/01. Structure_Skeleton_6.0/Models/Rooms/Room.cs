using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        private int bedCapacity;
        private double pricePerNight;

        protected Room(int bedCapacity)
        {
            this.bedCapacity = bedCapacity;
            this.pricePerNight = 0;
        }

        public int BedCapacity { get => this.bedCapacity; private set => this.bedCapacity = value; }

        public double PricePerNight
        {
            get => this.pricePerNight;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.PricePerNightNegative);
                }

                this.pricePerNight = value;
            }        
        }

        public void SetPrice(double price) => PricePerNight = price;
    }
}
