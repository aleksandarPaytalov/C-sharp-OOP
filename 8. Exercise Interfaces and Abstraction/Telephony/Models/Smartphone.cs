using Telephony.Models.Interfaces;

namespace Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Browse(string url)
        {
            if (!ValidateURL(url))
            {
                throw new ArgumentException("Invalid URL!");
            }

            return $"Browsing: {url}!";
        }

        public string Call(string phoneNumber)
        {
            if (!ValidatePhoneNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid number!");
            }

            return $"Calling... {phoneNumber}";
        }

        private bool ValidatePhoneNumber(string phoneNumber)
        => phoneNumber.All(d => char.IsDigit(d));

        private bool ValidateURL(string url)
        => url.All(u => !char.IsDigit(u));
    }
}
