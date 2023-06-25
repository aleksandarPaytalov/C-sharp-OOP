using BorderControl.Models.Interfaces;

namespace BorderControl.Models
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
