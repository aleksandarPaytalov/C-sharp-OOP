
namespace ClassBoxData.Models
{
    public class Box
    {
        private const string ZeroOrNegativeExceptionMessage = "{0} cannot be zero or negative.";
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public double Length
        {
            get => length;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ZeroOrNegativeExceptionMessage, nameof(Length)));
                }

                length = value;
            }
        }
        public double Width
        {
            get => width;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ZeroOrNegativeExceptionMessage, nameof(Width)));
                }

                width = value;
            }
        }
        public double Height
        {
            get => height;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ZeroOrNegativeExceptionMessage, nameof(Height)));
                }

                height = value;
            }
        }

        public double Volume()
          => Length * Width * Height;
        public double LateralSurfaceArea()
            => 2 * Length * Height + 2 * Width * Height;
        public double SurfaceArea()
        => 2 * Length * Width + LateralSurfaceArea();
    }


}
