
namespace CalorieTracker.DTO.Requests;

public class DetailedMealComponentRequest
{
    public int DetailedMealId { get; set; }
    public double Quantity { get; set; }
    public int DetailedFoodId { get; set; }
}