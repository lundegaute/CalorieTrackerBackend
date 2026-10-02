using CalorieTracker.Models;

namespace CalorieTracker.DTO.Requests.DetailedRequests;

public class AddCustomFoodToDetailedFoodsRequest
{
    public required string FoodName { get; set; }
    public required DbCaloriesRequest Calories {  get; set; }
    public List<FoodConstituentsRequest> Constituents { get; set; } = [];
}

public class DbCaloriesRequest
{
    public required int Quantity { get; set; }
}

public class FoodConstituentsRequest
{
    public required string NutrientId { get; set; }
    public double? Quantity { get; set; } = null;
}

