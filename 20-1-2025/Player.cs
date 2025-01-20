using System;

public abstract class Player : IPerson
{
    public string? FirstName { get; set; }
    public int ID { get; set; }
    public string? Team { get; set; }

    public float? PointsPerGame { get; set; } = 0;    

    
    public abstract string IsAllStarDeserving();


    public abstract string DisplayData();

 



}
