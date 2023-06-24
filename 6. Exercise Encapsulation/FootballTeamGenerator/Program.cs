using FootballTeamGenerator;
using FootballTeamGenerator.Models;
using System;

List<Team> teams = new();

string input = string.Empty;
while ((input = Console.ReadLine()) != "END")
{
    string[] info = input.Split(";", StringSplitOptions.RemoveEmptyEntries);
    string command = info[0];

    try
    {
        switch (command)
        {
            case "Team":
                AddTeam(info[1], teams);
                break;
            case "Add":
                AddPlayer(info[1],
                info[2],
                int.Parse(info[3]),
                int.Parse(info[4]),
                int.Parse(info[5]),
                int.Parse(info[6]),
                int.Parse(info[7]),
                teams);
                break;
            case "Remove":
                RemovePlayer(info[1], info[2], teams);
                break;
            case "Rating":
                PrintTheRating(info[1], teams);
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void AddTeam(string name, List<Team> teams) 
    => teams.Add(new Team(name));

static void AddPlayer (string teamName,
    string playerName, 
    int endurance, 
    int sprint, 
    int passing,
    int dribble,
    int shooting, 
    List<Team> teams)
{
    Team team = teams.FirstOrDefault(t => t.Name == teamName);
    if (team is null)
    {
        throw new ArgumentException(string.Format(ExceptionMessages.TeamDosntExistMessage, teamName));     
    }
    Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);

    team.AddPlayer(player);
}

static void RemovePlayer(string teamName, string playerName, List<Team> teams)
{
    Team team = teams.FirstOrDefault(t => t.Name == teamName);
    if (team is null)
    {
        throw new ArgumentException(string.Format(ExceptionMessages.TeamDosntExistMessage, teamName));
    }
    team.RemovePlayer(playerName);
}
static void PrintTheRating(string teamName, List<Team> teams)
{
    Team team = teams.FirstOrDefault(t => t.Name == teamName);
    if (team is null)
    {
        throw new ArgumentException(string.Format(ExceptionMessages.TeamDosntExistMessage, teamName));
    }

    Console.WriteLine($"{teamName} - {team.Rating:f0}");
}