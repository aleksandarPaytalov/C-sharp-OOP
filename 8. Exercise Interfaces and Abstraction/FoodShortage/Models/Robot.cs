using FoodShortage.Models.Interfaces;

namespace FoodShortage.Models
{
    internal class Robot : IIdentible
    {
        public Robot(string id, string model)
        {
            Id = id;
            Model = model;
        }

        public string Id { get; private set; }
        public string Model { get; set; }
    }
}
