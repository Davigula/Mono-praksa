namespace Recipe.API.Controllers
{
    public class Recipe
    {

        public string? RecipeName{ get; set; }

        public int? DurationOfCooking { get; set; }

        //public <string>? Ingredians

        public string DisplayData()
        {
            return $"The recipe for {this.RecipeName} is meant to last {this.DurationOfCooking}.";
        }

    }
}
