using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy : IDelicacy
    {
        private string name;
        private double price;

        protected Delicacy(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name
        { 
            get { return this.name; }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }

                this.name = value;
            }
        }

        public double Price
        { 
            get { return this.price; }
            private set { this.price = value;  }
        }
        public override string ToString()
        {
            return $"{Name} - {Price:f2} lv";
        }
    }
}
