
namespace FootballTeamGenerator.Models
{
    public class Player
    {
        
        private const int StatsMinValue = 0;
        private const int StatsMaxValue = 100;

        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) //
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameEmptyExceptionMessage));                
                }
                name = value;
            }
        }
        public int Endurance
        {
            get => endurance;
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.StatsOutOfRangeExceptionMessage, nameof(Endurance)));
                }
                endurance = value;
            }
        }
        public int Sprint
        {
            get => sprint;
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.StatsOutOfRangeExceptionMessage, nameof(Sprint)));
                }
                sprint = value;
            }
        }
        public int Dribble
        {
            get => dribble;
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.StatsOutOfRangeExceptionMessage, nameof(Dribble)));
                }
                dribble = value;
            }
        }
        public int Passing
        {
            get => passing;
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.StatsOutOfRangeExceptionMessage, nameof(Passing)));
                }
                passing = value;
            }
        }
        public int Shooting
        {
            get => shooting;
            set
            {
                if (CheckStatValue(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.StatsOutOfRangeExceptionMessage, nameof(Shooting)));
                }
                shooting = value;
            }
        }

        public double Stats => (Endurance + Sprint + Dribble + Passing + Shooting) / 5.0; 

        private bool CheckStatValue(int value) => value < StatsMinValue || value > StatsMaxValue; // ако трябва да го променяме с кейс равно да го променим на едно място а във всички пропъртита
    }
}
