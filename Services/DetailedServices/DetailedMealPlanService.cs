using CalorieTracker.DTO;
using CalorieTracker.Repositories;



namespace CalorieTracker.Services;

public class DetailedMealPlanService
{
    private readonly DetailedMealPlanRepository _detailedMealPlanRepository;

    public DetailedMealPlanService(DetailedMealPlanRepository detailedMealPlanRepository)
    {
        _detailedMealPlanRepository = detailedMealPlanRepository;
    }

    // Gets the entire MealPlans -> meals -> mealcomponents -> foods, for current user
    public async Task<ApiResponse<List<DetailedMealPlanDTO>>> GetEntireDetailedMealPlan(int userID)
    {
        var mealPlans = await _detailedMealPlanRepository.GetEntireDetailedMealPlan(userID);

        var mealPlanDTOs = mealPlans.Select(plan => new DetailedMealPlanDTO
        {
            Id = plan.Id,
            Name = plan.Name,
            UserId = plan.UserId
        });

        var response = ApiResponse<List<DetailedMealPlanDTO>>.Success(mealPlanDtos, 200);

        return respons;
    }


    public async Task<ApiResponse<List<DetailedMealPlanDTO>>> GetDetailedMealPlans(int userID)
    {
        var mealPlans = await _detailedMealPlanRepository.GetDetailedMealPlansAsync(userID);

        var mealPlansDTO = mealPlans.Select(mp => new DetailedMealPlanDTO
        {
            Id = mp.Id,
            Name = mp.Name,
            UserId = mp.UserId,
        }).ToList();

        var apiResponse = ApiResponse<List<DetailedMealPlanDTO>>.Success(mealPlansDTO, 200);

        return apiResponse;
        
    }
}