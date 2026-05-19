

namespace CalorieTracker.Models;

public class DetailedMealPlan
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<DetailedMeal> DetailedMeals { get; set; } = new();

    public int UserId { get; set; }
    public User? User { get; set; }

}