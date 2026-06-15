
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

    
    public async Task<ApiResponse<string>> AddMealComponent(int userID, List<DetailedMealComponentRequest> request)
    {  
        if ( request.Any(item => int.IsNegative(item.DetailedMealId)) )
            throw new ArgumentException($"Detailed Meal ID can not be a negative number"); 
        if ( request.Any( item => int.IsNegative(item.DetailedFoodId)) )
            throw new ArgumentException($"Detailed Food ID can not be a negative number"); 
        if ( request.Any( item => double.IsNaN(item.Quantity)) )
            throw new ArgumentException($"quantity must be a number");
        if ( request.Any( item => double.IsNegative( item.Quantity)) )
            throw new ArgumentException($"Quantity must be a positive number");

        // Check if detailedMealId belongs to user
        var detailedMealIds = await _detailedMealPlanRepository.GetUserMealIds(userID);
        if ( request.Any( item => !detailedMealIds.Contains(item.DetailedMealId)) )
            throw new UnauthorizedAccessException("MealId does not belong to user");

        // Validate detailedFood
        var uniqueFoodIds = request.Select(item => item.DetailedFoodId).Distinct().ToList();
        var detailedFoods = await _foodRepository.GetMultipleDetailedFoodById( uniqueFoodIds );
        if ( detailedFoods.Count != uniqueFoodIds.Count )
            throw new KeyNotFoundException("One or more food items were not found");
        
        var detailedFoodDict = detailedFoods.ToDictionary(
            item => item.Id,
            item => item
        );

        var newFoodComponents = request.Select( item => new DetailedMealComponent
        {
            DetailedMealId = item.DetailedMealId,
            Quantity = item.Quantity,
            DetailedFood = detailedFoodDict[item.DetailedFoodId],
        }).ToList();

        var response = await _detailedMealComponentRepository.AddMealComponents(newFoodComponents);
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