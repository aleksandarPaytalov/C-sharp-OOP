
namespace FootballTeamGenerator.Models
{
    public class Team
    {
        private string name;
        //private double rating; не ни филд за рейтинга, защото ще го взимаме от листа с играчи
        private List<Player> players;

        public Team(string name)
        {
            Name = name;
            players = new();
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) //
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameEmptyExceptionMessage, Name));
                }
                name = value;
            }
        }
        public double Rating
        {
            get
            {
                if (players.Any())
                { 
                return players.Average(p => p.Stats);
                }

                return 0;
            }
            // няма сетър за да не може да се променя от външния свят
        }

        public void AddPlayer(Player player) => players.Add(player);
        public void RemovePlayer(string playerName)
        {
            Player player = players.FirstOrDefault(p => p.Name == playerName);
            if (player is null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.PlayerNameMissingMessage, playerName, Name));
            }
            players.Remove(player);
        }
        
    }
}
 