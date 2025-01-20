using System;

public interface IPerson
{
    public string? FirstName { get; set; }
    public int ID {  get; set; }
    public string? Team {  get; set; }

    public string DisplayData();
    //public string FirstName;

    //public Surname(){}

    //public Dob(){}

    //public ID(){}


}
