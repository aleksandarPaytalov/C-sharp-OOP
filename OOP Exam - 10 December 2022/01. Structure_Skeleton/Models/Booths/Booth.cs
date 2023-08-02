using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private readonly IRepository<IDelicacy> delicacies;
        private readonly IRepository<ICocktail> cocktails;

        private double currentBill;
        private double turnover;
        private bool isReserved;

        public Booth(int boothId, int capacity)
        {
            this.boothId = boothId;
            Capacity = capacity;

            this.delicacies = new DelicacyRepository();
            this.cocktails = new CocktailRepository();

            this.currentBill = 0;
            this.turnover = 0;
            this.isReserved = false;
        }

        public int BoothId { get => this.boothId; private set => this.boothId = value; }

        public int Capacity
        {
            get { return this.capacity; }
            private set 
            {
                if (value <= 0)  
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CapacityLessThanOne));
                }

                this.capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => this.delicacies;

        public IRepository<ICocktail> CocktailMenu => this.cocktails;

        public double CurrentBill => this.currentBill;

        public double Turnover => this.turnover;

        public bool IsReserved => this.isReserved;

        public void ChangeStatus()
        {
            if (this.isReserved)
            {
                this.isReserved = false;
            }
            else
            {
                this.isReserved = true;
            }            
        }

        public void Charge()
        {           
            this.turnover += currentBill; 
            this.currentBill = 0;
        }

        public void UpdateCurrentBill(double amount) => this.currentBill += amount;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {this.turnover:F2} lv");
            sb.AppendLine("-Cocktail menu:");

            foreach (var coctail in cocktails.Models)
            {
                sb.AppendLine($"--{coctail}");
            }

            sb.AppendLine($"-Delicacy menu:");

            foreach (var delicacy in delicacies.Models)
            {
                sb.AppendLine($"--{delicacy}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
