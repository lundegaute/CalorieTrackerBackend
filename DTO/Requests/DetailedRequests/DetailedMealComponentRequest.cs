
namespace CalorieTracker.DTO.Requests;

public class DetailedMealComponentRequest
{
    public int DetailedMealId { get; set; }
    public double Quantity { get; set; }
    public int DetailedFoodId { get; set; }
}

public class UpdateDetailedMealComponentRequest
{
    public required int DetailedMealComponentId { get; set; }
    public required int DetailedMealId { get; set; }
    public required double Quantity { get; set; }
}