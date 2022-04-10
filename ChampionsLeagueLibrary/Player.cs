namespace ChampionsLeagueLibrary;

// TODO: Vytvořte třídu Player

public class Player {
    // TODO: Vytvořte vlastnosti
    // - string Name
    // - FootballClub Club
    // - int GoalCount
    public string Name { get; set; }
    public FootballClub Club { get; set; }
    public int GoalCount { get; set; }
    // TODO: Vytvořte parametrický konstruktor (name, club, goalCount)
    public Player(string name, FootballClub club, int goalCount)
    {
        Name = name;
        Club = club;
        GoalCount = goalCount;
    }



    public override string? ToString()
    {
        return "Jméno: " + Name + ", Klub: " + FootballClubInfo.GetName(Club) + ", Góly: " + GoalCount;
    }

    public override bool Equals(object? obj)
    {
        return obj is Player player &&
               Name == player.Name &&
               Club == player.Club &&
               GoalCount == player.GoalCount;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Club, GoalCount);
    }
    //  Konec třídy Player
}

