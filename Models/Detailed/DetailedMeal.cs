

namespace CalorieTracker.Models;

public class DetailedMeal
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int DetailedMealPlanId { get; set; }

    public List<DetailedMealComponent> Components { get; set; } = new();

}