

namespace CalorieTracker.Models;

public class DetailedMealPlan
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int UserId { get; set; }

    public List<DetailedMeal> DetailedMeals { get; set; } = new();
    public User? User { get; set; }

}