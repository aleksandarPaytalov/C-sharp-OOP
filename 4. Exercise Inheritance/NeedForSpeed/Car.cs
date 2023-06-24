

namespace NeedForSpeed
{
    public class Car : Vehicle
    {
        private double DefaultFuelConsumption  = 3;
        public Car(int horsePower, double fuel) : base(horsePower, fuel)
        {
        }
        public override double FuelConsumption => DefaultFuelConsumption;
    }
}
