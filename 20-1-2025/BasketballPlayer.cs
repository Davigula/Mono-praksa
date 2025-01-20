using System;

public class BasketballPlayer : Player
{
    public BasketballPlayer() { }

    public BasketballPlayer(string name, float  points) { FirstName = name; PointsPerGame = points; Team = "N/A"; }

    public BasketballPlayer(int id, string name, float points, string team) { Team = team; FirstName = name; ID = id; PointsPerGame = points; }

    public override string IsAllStarDeserving()
    {
        return PointsPerGame >= 20 ? "" : " not";
    }

    public override string DisplayData()
    {
        return  $"This player {this.FirstName} - {this.Team} does{IsAllStarDeserving()} deserve All star selection";
    }
    


}
