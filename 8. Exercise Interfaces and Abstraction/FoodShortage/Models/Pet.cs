using FoodShortage.Models.Interfaces;

namespace FoodShortage.Models
{
    public class Pet : IBirthdate, INameable
    {
        public Pet(string name, string birthday)
        {
            Name = name;
            Birthday = birthday;
        }

        public string Name { get; private set; }
        public string Birthday { get; private set; }        
    }
}
