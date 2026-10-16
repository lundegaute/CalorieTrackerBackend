

namespace CalorieTracker.DTO.Requests;


public class DetailedMealRequest
{
    public required string Name { get; set; }
    public int DetailedMealPlanId { get; set; }

}