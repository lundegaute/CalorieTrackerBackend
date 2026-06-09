
using CalorieTracker.DTO;
using CalorieTracker.DTO.Requests;
using CalorieTracker.Models;
using CalorieTracker.Repositories;


namespace CalorieTracker.Services;

public class DetailedMealComponentService
{
    private readonly DetailedMealComponentRepository _detailedMealComponentRepository;
    private readonly DetailedMealRepository _detailedMealRepository;
    private readonly DetailedMealPlanRepository _detailedMealPlanRepository;
    private readonly FoodRepository _foodRepository;


    public DetailedMealComponentService(
        DetailedMealComponentRepository detailedMealComponentRepository,
        DetailedMealRepository detailedMealRepository,
        DetailedMealPlanRepository detailedMealPlanRepository,
        FoodRepository foodRepository)
    {
        _detailedMealComponentRepository = detailedMealComponentRepository;
        _detailedMealRepository = detailedMealRepository;
        _detailedMealPlanRepository = detailedMealPlanRepository;
        _foodRepository = foodRepository;
    }

    
    public async Task<ApiResponse<string>> AddMealComponent(int userID, DetailedMealComponentRequest request)
    {  
        if ( int.IsNegative(request.DetailedMealId) )
            throw new ArgumentException($"{nameof(request.DetailedMealId)} can not be a negative number"); 
        if ( int.IsNegative(request.DetailedFoodId) )
            throw new ArgumentException($"{nameof(request.DetailedFoodId)} can not be a negative number"); 
        if ( double.IsNaN(request.Quantity))
            throw new ArgumentException($"{nameof(request.Quantity)} must be a number");
        if ( double.IsNegative(request.Quantity) )
            throw new ArgumentException($"{nameof(request.Quantity)} must be a positive number");

        // Check if detailedMealId belongs to user
        var detailedMealIds = await _detailedMealPlanRepository.GetUserMealIds(userID);
        if ( !detailedMealIds.Contains(request.DetailedMealId))
            throw new UnauthorizedAccessException("MealId does not belong to user");

        // Validate detailedFood
        var detailedFood = await _foodRepository.GetDetailedFoodById(request.DetailedFoodId);
        if ( detailedFood is null ) 
            throw new KeyNotFoundException($"FoodItem not found with ID: {request.DetailedFoodId}");


        var newFoodComponent = new DetailedMealComponent
        {
            DetailedMealId = request.DetailedMealId,
            Quantity = request.Quantity,
            DetailedFood = detailedFood
        };

        var response = await _detailedMealComponentRepository.AddMealComponent(newFoodComponent);
        var apiResponse = ApiResponse<string>.Success(response, 200);
        return apiResponse;
    }

    public async Task<ApiResponse<string>> DeleteMealComponent( int userID, int mealComponentID)
    {
        if (int.IsNegative(mealComponentID))
            throw new ArgumentException("mealComponentID can not be a negative number");
        
        var mealComponentToDelete = await _detailedMealPlanRepository.GetUserMealComponent(userID, mealComponentID);
        if ( mealComponentToDelete is null )
            throw new KeyNotFoundException("Meal Component not found");
        
        var response = await _detailedMealComponentRepository.DeleteMealComponent(mealComponentToDelete);

        return ApiResponse<string>.Success(response, 200);
    }
}