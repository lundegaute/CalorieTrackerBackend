// Food myDeserializedClass = JsonConvert.DeserializeObject<Food>(myJsonResponse);
// This file is to fetch advanced data from Matvaretabellen API
namespace CalorieTracker.Models;


public class DetailedFood
{
    public int Id { get; set; }
    public string FoodId { get; set;}
    public string FoodName { get; set;}
    public string FoodGroupId { get; set;}
    
    public DbCalories? Calories { get; set;}
    public DbEnergy? Energy { get; set; }

    public List<string> SearchKeywords { get; set; }
    public List<FoodConstituent> FoodConstituents { get; set; }

}

public class Nutrient
{
    public string NutrientId { get; set; }
    public string NutrientName { get; set; }
    public string? DefaultUnit { get; set;}
}

public class DbCalories
{
    public int? Quantity { get; set; }
    public string? Unit { get; set; }

}
public class DbEnergy
{
    public double? Quantity { get; set; }
    public string? Unit { get; set; }
}

public class FoodConstituent
{
    public int Id { get; set; }
    
    public int DetailedFoodId { get; set; }
    //public DetailedFood Food { get; set; } This is not needed. But maybe is

    public string NutrientId { get; set; }
    public Nutrient Nutrient { get; set; }


    public double? Quantity { get; set; }
}


