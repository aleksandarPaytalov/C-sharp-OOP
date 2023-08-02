using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;

        protected Cocktail(string name, string size, double price)
        {
            Name = name;
            Size = size;
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

        public string Size
        {
            get { return this.size; }

            private set
            {               
                this.size = value;
            }
        }

        public double Price
        {
            get { return this.price; }

            private set
            {
                if (Size == "Small")
                {
                    value /= 3;
                }
                else if (Size == "Middle")
                {
                    value = value * 2 / 3;
                }

                this.price = value;
            }
        }
        public override string ToString()
        {
            return $"{Name} ({Size}) - {Price:f2} lv";
        }
    }
}
