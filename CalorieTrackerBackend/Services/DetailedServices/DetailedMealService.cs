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
        // Validating new mealName
        if ( string.IsNullOrWhiteSpace(request.Name))
            return ApiResponse<string>.Failure(["Name can not be empty"],["Bad request"], 400);

        // Validating mealPlanId
        if ( request.DetailedMealPlanId < 1 ) 
            throw new ArgumentException("Invalid mealPlan Id");

        // If MealPlanId does not belong to user
        var userMealPlanIds = await _detailedMealPlanRepository.GetUserDetailedMealPlanIdsAsync(userID);
        if ( !userMealPlanIds.Contains(request.DetailedMealPlanId) )
            throw new UnauthorizedAccessException("Invalid MealPlanID");

        // Checking for duplicate names
        var userMealNames = await _detailedMealPlanRepository.GetUserMealNames(userID, request.DetailedMealPlanId);
        if ( userMealNames.Contains(request.Name))
            return ApiResponse<string>.Failure([$"The name {request.Name}, already exists in this meal plan"], ["Duplicate Error"], 400);

        var newDetailedMeal = new DetailedMeal
        {
            Name = request.Name,
            DetailedMealPlanId = request.DetailedMealPlanId
        };

        var response = await _detailedMealRepository.AddDetailedMeal(newDetailedMeal);
        var apiResponse = ApiResponse<string>.Success(response, 200);
        
        return apiResponse;
    }

    public async Task<ApiResponse<string>> DeleteMeal(int userID, int mealID)
    {
        if ( int.IsNegative(mealID))
            throw new ArgumentException("MealID can not be a negative number");
        var mealToDelete = await _detailedMealPlanRepository.GetDetailedMealById(mealID, userID);
        if ( mealToDelete is null ) 
            throw new KeyNotFoundException("Meal not found");
        
        var response = await _detailedMealRepository.DeleteMeal(mealToDelete);
        return ApiResponse<string>.Success(response, 200);
    }

}


