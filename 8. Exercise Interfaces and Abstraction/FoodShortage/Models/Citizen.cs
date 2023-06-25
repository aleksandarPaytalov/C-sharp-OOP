using FoodShortage.Models.Interfaces;

namespace FoodShortage.Models
{
    public class Citizen : IIdentible, IBirthdate, IAged, IBuyer
    {
        private const int DefaultFoodIncrese = 10;
        public Citizen(string id, string name, int age, string birthday)
        {
            Id = id;
            Name = name;
            Age = age;
            Birthday = birthday;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Birthday { get; private set; }
        public int Food { get; private set; }

        public void BuyFood()
        {
            Food += DefaultFoodIncrese;
        }
    }
}
