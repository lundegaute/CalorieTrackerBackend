
using CalorieTracker.Data;
using CalorieTracker.Models;


namespace CalorieTracker.Repository;

public class DetailedMealPlanRepository
{
    private readonly DataContext _context;

    public DetailedMealPlanRepository(DataContext context)
    {
        _context = context;
    }

    
}