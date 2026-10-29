using Books.DTO.Requests;
using CalorieTracker.DTO.Requests.DetailedRequests;
using Swashbuckle.AspNetCore.Filters;

namespace CalorieTracker.SwaggerExamples;

public class DetailedFoodExample : IExamplesProvider<AddCustomFoodToDetailedFoodsRequest>
{
    public AddCustomFoodToDetailedFoodsRequest GetExamples()
    {
        return new AddCustomFoodToDetailedFoodsRequest
        {
            FoodName = "Whey 80, Star Nutrition, Protein Pulver",
            Calories = new DbCaloriesRequest
            {
                Quantity = 380
            },
            Constituents = new List<FoodConstituentsRequest>
            {
                new FoodConstituentsRequest { NutrientId = "Protein", Quantity = 73.4},
                new FoodConstituentsRequest { NutrientId = "Karbo", Quantity = 5.4},
                new FoodConstituentsRequest { NutrientId = "Fett", Quantity = 6.0}
            }

        };
    }
}
