using CalorieTracker.Repositories;
using CalorieTracker.DTO;
using CalorieTracker.Models;



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

    public async Task<ApiResponse<List<DetailedMealDTO>>> GetDetailedMeals(int userID, int detailedMealPlanID)
    {
        var detailedMealPlanIds = await _detailedMealPlanRepository.GetUserDetailedMealPlanIdsAsync(userID);

        // If user does not have any mealplans
        if ( !detailedMealPlanIds.Any() )
            throw new KeyNotFoundException("User must create a mealplan first");
        // If no mealplanId does not belong to current user
        if (!detailedMealPlanIds.Contains(detailedMealPlanID))
            throw new ArgumentException("Meal does not belong to current user");

        var detailedMeals = await _detailedMealRepository.GetDetailedMealsAsync(detailedMealPlanID);

        var detailedMealsDTO = detailedMeals.Select(dm => new DetailedMealDTO
        {
            Id = dm.Id,
            Name = dm.Name,
            DetailedMealPlanId = dm.DetailedMealPlanId
        }).ToList();

        var apiResponse = ApiResponse<List<DetailedMealDTO>>.Success(detailedMealsDTO, 200);

        return apiResponse;
    }
}


