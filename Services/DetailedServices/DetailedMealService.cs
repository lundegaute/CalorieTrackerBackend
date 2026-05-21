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

}


