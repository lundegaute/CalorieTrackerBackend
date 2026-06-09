using CalorieTracker.DTO;
using CalorieTracker.DTO.Constituents;
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
        if ( string.IsNullOrWhiteSpace(request.Name) )
            return ApiResponse<string>.Failure(["Mealplan name can not be empty"], ["User error"], 400);
        // Test if the request name already exists for the current user
        if ( await _detailedMealPlanRepository.IsNameTaken(request.Name, user.Id))
            return ApiResponse<string>.Failure(["Mealplan name already exists"], ["User error"], 400);


        var newPlan = new DetailedMealPlan
        {
            Name = request.Name,
            User = user
        };
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
                                Category = fc.Nutrient.Category,
                            },
                            Quantity = fc.Quantity ?? 0,
                        }).ToList(),
                    }
                }).ToList()
            }).ToList(),
        }).ToList();

        

        var response = ApiResponse<List<DetailedCompleteOverviewDTO>>.Success(mealPlanDTOs, 200);

        return response;
    }

    public async Task<ApiResponse<string>> DeleteMealPlanById(int userID, int mealPlanID)
    {
        if ( int.IsNegative(mealPlanID))
            throw new ArgumentException($"{nameof(mealPlanID)} can not be a negative number");
        var userMealPlan = await _detailedMealPlanRepository.GetUserMealPlan(userID, mealPlanID);
        if ( userMealPlan is null ) 
            throw new KeyNotFoundException($"MealPlan not found");


        var response = await _detailedMealPlanRepository.DeleteMealPlanById(userMealPlan);
        return ApiResponse<string>.Success(response, 200);
    }

}