using CalorieTracker.Repositories;
using CalorieTracker.DTO;
using CalorieTracker.Models;
using CalorieTracker.DTO.Requests;



public class DetailedMealService
{
    private readonly DetailedMealRepository _detailedMealRepository;
    private readonly DetailedMealPlanRepository _detailedMealPlanRepository;

    public DetailedMealService(
        DetailedMealRepository detailedMealRepository,
        DetailedMealPlanRepository detailedMealPlanRepository)
    {
        _detailedMealRepository = detailedMealRepository;
        _detailedMealPlanRepository = detailedMealPlanRepository;
    }

    public async Task<ApiResponse<string>> AddDetailedMeal(int userID, DetailedMealRequest request)
    {
        if ( string.IsNullOrWhiteSpace(request.Name))
            return ApiResponse<string>.Failure(["Name can not be empty"],["Bad request"], 400);
        if ( request.DetailedMealId < 1 ) 
            throw new ArgumentException("Invalid Meal Id");
        
        // If MealPlanId does not belong to user
        var userMealPlanIds = await _detailedMealPlanRepository.GetUserDetailedMealPlanIdsAsync(userID);
        if ( !userMealPlanIds.Contains(request.DetailedMealId) )
            throw new UnauthorizedAccessException("Invalid MealPlanID");

        var newDetailedMeal = new DetailedMeal
        {
            Name = request.Name,
            DetailedMealPlanId = request.DetailedMealId
        };

        var response = await _detailedMealRepository.AddDetailedMeal(newDetailedMeal);
        var apiResponse = ApiResponse<string>.Success(response, 200);
        
        return apiResponse;
    }

}


