

namespace CalorieTracker.DTO;

public class DetailedCompleteOverviewDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<DetailedMealDTO> DetailedMeals { get; set; }
}

public class DetailedMealDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    

    public List<DetailedMealComponentDTO> Components { get; set; } = new();

}

public class DetailedMealComponentDTO
{
    public int Id { get; set; } // Havregryn
    public double Quantity { get; set;} // 100g
    
    public required DetailedFoodDTO DetailedFood { get; set; } // Havregryn from Matvaretabellen

}

public class DetailedFoodDTO
{
    public int Id { get; set; } // 582
    public required string FoodName { get; set;} // Havregryn
    public string FoodGroupId { get; set;}
    
    public int Calories { get; set;} // 369Kcals
    public double Energy { get; set; }

    public List<FoodConstituentDTO> Constituents { get; set; }

}

public class FoodConstituentDTO
{
    public int Id { get; set; }
    public string NutrientId { get; set; }
    public string Name { get; set; }
    public double? Quantity { get; set; }
}
