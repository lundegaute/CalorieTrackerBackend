using CalorieTracker.DTO;
using CalorieTracker.DTO.Requests;
using CalorieTracker.Repositories;
using CalorieTracker.Models;
using CalorieTracker.Services;


namespace CalorieTracker.Services;

public class DetailedMealPlanService
{
    private readonly DetailedMealPlanRepository _detailedMealPlanRepository;
    private readonly AuthService _authService;

    public DetailedMealPlanService(
        DetailedMealPlanRepository detailedMealPlanRepository,
        AuthService authService
        )
    {
        _detailedMealPlanRepository = detailedMealPlanRepository;
        _authService = authService;
    }

    public async Task<ApiResponse<string>> AddDetailedMealPlan(int userID, DetailedMealPlanRequest request)
    {
        var user = await _authService.GetUser(userID);

        var newPlan = new DetailedMealPlan
        {
            Name = request.Name,
            User = user
        } ;
        var response = await _detailedMealPlanRepository.AddDetailedMealPlan(newPlan);

        var apiResponse = ApiResponse<string>.Success(response, 200);

        return apiResponse;
    }


    // Gets the entire DetailedMealPlans -> DetailedMeals -> DetailedMealcomponents -> DetailedFoods -> FoodConstituents -> Nutrients
    public async Task<ApiResponse<List<DetailedCompleteOverviewDTO>>> GetEntireDetailedMealPlan(int userID)
    {
        var mealPlans = await _detailedMealPlanRepository.GetEntireDetailedMealPlan(userID);

        // Create MealPlanDTO here
        var mealPlanDTOs = mealPlans.Select(plan => new DetailedCompleteOverviewDTO
        {
            Id = plan.Id,
            Name = plan.Name,
            DetailedMeals = plan.DetailedMeals.Select(meal => new DetailedMealDTO
            {
                Id = meal.Id,
                Name = meal.Name,
                // TotalCalories property is automatically calculated
                Components = meal.Components.Select(food => new DetailedMealComponentDTO
                {
                    Id = food.Id,
                    Quantity = food.Quantity,
                    // TotalCalories property is automatically calculated
                    DetailedFood = new DetailedFoodDTO
                    {
                        Id = food.DetailedFood.Id,
                        FoodName = food.DetailedFood.FoodName,
                        FoodGroupId = food.DetailedFood.FoodGroupId,
                        Calories = food.DetailedFood.Calories?.Quantity,
                        Energy = food.DetailedFood.Energy?.Quantity,
                        Constituents = food.DetailedFood.FoodConstituents.Select(fc => new FoodConstituentDTO
                        {
                            Id = fc.Id,
                            NutrientId = fc.NutrientId,
                            Nutrient = new NutrientDTO
                            {
                                NutrientId = fc.NutrientId,
                                NutrientName = fc.Nutrient.NutrientName,
                                DefaultUnit = fc.Nutrient.DefaultUnit,
                            },
                            Quantity = fc.Quantity,
                        }).ToList(),
                    }
                }).ToList()
            }).ToList(),
        }).ToList();

        var response = ApiResponse<List<DetailedCompleteOverviewDTO>>.Success(mealPlanDTOs, 200);

        return response;
    }


}