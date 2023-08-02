using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private double maxMileage;
        private string licensePlateNumber;
        private int batteryLevel;
        private bool isDamaged;

        protected Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            Brand = brand;
            Model = model;
            this.maxMileage = maxMileage;
            LicensePlateNumber = licensePlateNumber;
            this.batteryLevel = 100;
            this.isDamaged = false;
        }

        public string Brand
        {
            get => this.brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BrandNull);
                }

                this.brand = value;
            }
        }

        public string Model
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNull);
                }

                this.model = value;
            }
        }

        public double MaxMileage => this.maxMileage;

        public string LicensePlateNumber
        {
            get => this.licensePlateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }

                this.licensePlateNumber = value;
            }
        }

        public int BatteryLevel => this.batteryLevel;

        public bool IsDamaged => this.isDamaged;

        public void ChangeStatus()
        {
            if (this.isDamaged == false)
            {
                this.isDamaged = true;
            }
            else
            {
                this.isDamaged = false;
            }

        }

        public void Drive(double mileage)
        {
            double percentage = Math.Round((mileage / this.maxMileage) * 100);
            this.batteryLevel -= (int)percentage;

            if (this.GetType().Name == nameof(CargoVan))
            {
                this.batteryLevel -= 5;
            }

        }

        public void Recharge() => this.batteryLevel = 100;

        public override string ToString()
        {
            string status = string.Empty;
            if (isDamaged == true)
            {
                status = "damaged";
            }
            else
            {
                status = "OK";
            }
            return $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {status}";
        }
    }
}
