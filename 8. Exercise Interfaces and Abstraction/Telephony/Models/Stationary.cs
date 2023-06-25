using Telephony.Models.Interfaces;

namespace Telephony.Models
{
    public class Stationary : ICallable
    {
        public string Call(string phoneNumber)
        {
            if (!ValidatePhoneNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid number!");
            }

            return $"Dialing... {phoneNumber}";
        }

        private bool ValidatePhoneNumber(string phoneNumber)
        => phoneNumber.All(d => char.IsDigit(d));
    }
}
