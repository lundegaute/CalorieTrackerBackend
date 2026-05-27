

namespace CalorieTracker.DTO;

public static class MacroId 
{
    public const string Protein = "Protein";
    public const string Carb = "Karbo";
    public const string Fat = "Fett";
}

public class DetailedCompleteOverviewDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double totalCalories => Math.Round( DetailedMeals.Sum(meal => meal.TotalCalories) ,1);
    public double totalProtein => Math.Round( DetailedMeals.Sum(meal => meal.TotalProtein) ,1);
    public double totalCarbs => Math.Round( DetailedMeals.Sum(meal => meal.TotalCarbs) ,1);
    public double totalFats => Math.Round( DetailedMeals.Sum(meal => meal.TotalFats) ,1);

    public List<DetailedMealDTO> DetailedMeals { get; set; } = new();
}

public class DetailedMealDTO
{
    public int Id { get; set; }
    public required string Name { get; set; } // Frokost
    public double TotalCalories => Math.Round(Components.Sum(component => component.TotalCalories), 1);
    public double TotalProtein => Math.Round(Components.Sum(component => component.TotalProtein), 1);
    public double TotalCarbs => Math.Round(Components.Sum(component => component.TotalCarbs), 1);
    public double TotalFats => Math.Round(Components.Sum(component => component.TotalFats), 1);

    public List<DetailedMealComponentDTO> Components { get; set; } = new(); // [Havregryn, Melk]


}

public class DetailedMealComponentDTO
{
    public int Id { get; set; } // Havregryn
    public double Quantity { get; set;} // 120g
    public double TotalCalories => Math.Round((Quantity / 100.0) * (DetailedFood.Calories ?? 0),1);
    public double TotalProtein => Math.Round((Quantity / 100.0) * (DetailedFood.Constituents.FirstOrDefault(c => c.NutrientId == MacroId.Protein)?.Quantity ?? 0), 1);
    public double TotalCarbs => Math.Round((Quantity / 100.0) * (DetailedFood.Constituents.FirstOrDefault(c => c.NutrientId == MacroId.Carb)?.Quantity ?? 0), 1);
    public double TotalFats => Math.Round((Quantity / 100.0) * (DetailedFood.Constituents.FirstOrDefault(c => c.NutrientId == MacroId.Fat)?.Quantity ?? 0), 1);
    public required DetailedFoodDTO DetailedFood { get; set; } // Havregryn from Matvaretabellen

}

public class DetailedFoodDTO
{
    public int Id { get; set; } // 582
    public required string FoodName { get; set;} // Havregryn
    public string FoodGroupId { get; set;}
    
    public int? Calories { get; set;} // 369Kcals per 100g
    public double? Energy { get; set; }

    public List<FoodConstituentDTO> Constituents { get; set; } = new();

}

public class FoodConstituentDTO
{
    public int Id { get; set; }
    public string NutrientId { get; set; } // Vit A
    public NutrientDTO Nutrient { get; set; } // Vitamin A
    public double? Quantity { get; set; }
}

public class NutrientDTO
{
    public string NutrientId { get; set; }
    public string NutrientName { get; set; }
    public string? DefaultUnit { get; set;}
}