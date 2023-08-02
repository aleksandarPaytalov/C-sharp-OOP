using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Net.WebSockets;
using System.Windows.Markup;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private string drivingLicenseNumber;
        private double rating;
        private bool isBlocked;

        public User(string firstName, string lastName, string drivingLicenseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DrivingLicenseNumber = drivingLicenseNumber;
            this.rating = 0;
            this.isBlocked = false;
        }

        public string FirstName
        {
            get => this.firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.FirstNameNull));
                }

                this.firstName = value;
            }

        }

        public string LastName
        {
            get => this.lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.LastNameNull));
                }

                this.lastName = value;
            }

        }

        public double Rating => this.rating;

        public string DrivingLicenseNumber
        {
            get => this.drivingLicenseNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.DrivingLicenseRequired));
                }

                this.drivingLicenseNumber = value;
            }

        }

        public bool IsBlocked => this.isBlocked;


        public void DecreaseRating()
        {
            if (this.rating < 2)
            {
                this.rating = 0.0;
                this.isBlocked = true;
            }
            else
            {
                this.rating -= 2;
            }
        }

        public void IncreaseRating()
        {
            if (this.rating < 10)
            {
                this.rating += 0.5;
            }
            else
            {
                this.rating = 10;
            }
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} Driving license: {this.drivingLicenseNumber} Rating: {this.rating}";
        }
    }
}
