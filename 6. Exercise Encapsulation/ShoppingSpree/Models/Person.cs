﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSpree.Models
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> products;

        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            products = new List<Product>();
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameEmpty));
                }
                name = value;
            }
        }
        public decimal Money
        {
            get => money;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.MoneyNegative));
                }
                money = value;
            }
        }

        public string AddProduct(Product product)
        {
            if (Money < product.Cost)
            {
                return $"{Name} can't afford {product.Name}";
            
            }
            products.Add(product);
            Money -= product.Cost;
            return $"{Name} bought {product.Name}";        
        }

        public override string ToString()
        {
            string boughtProducts = products.Any()
                ? string.Join(", ", products.Select(p=> p.Name))
                : "Nothing bought";

            return $"{Name} - {boughtProducts}";
        }

    }
}
