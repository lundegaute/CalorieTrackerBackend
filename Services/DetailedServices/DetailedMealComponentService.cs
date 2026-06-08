
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
        // Check if detailedMealId belongs to user
        var detailedMealIds = await _detailedMealPlanRepository.GetUserMealIds(userID);
        // If detailedMealId does not exist on current user, throw error
        if ( !detailedMealIds.Contains(request.DetailedMealId))
            throw new UnauthorizedAccessException("MealPlanId does not belong to user");

        var detailedFood = await _foodRepository.GetDetailedFoodById(request.DetailedFoodId);

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
}