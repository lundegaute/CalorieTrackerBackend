
using CalorieTracker.DTO;
using CalorieTracker.Models;
using CalorieTracker.Repositories;


namespace CalorieTracker.Services;

public class DetailedMealComponentService
{
    private readonly DetailedMealComponentRepository _detailedMealComponentRepository;

    public DetailedMealComponentService(DetailedMealComponentRepository detailedMealComponentRepository)
    {
        _detailedMealComponentRepository = detailedMealComponentRepository;
    }

    

}