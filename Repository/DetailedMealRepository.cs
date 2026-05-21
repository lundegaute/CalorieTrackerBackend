using Microsoft.EntityFrameworkCore;
using CalorieTracker.Models;
using CalorieTracker.Data;


public class DetailedMealRepository
{
    private readonly DataContext _context;

    public DetailedMealRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<DetailedMeal>> GetDetailedMealsAsync(int mealPlanID)
    { 

        var detailedMeals = await _context.DetailedMeals
            .Where(dm => dm.DetailedMealPlanId == mealPlanID)
            .ToListAsync();

        return detailedMeals;
    }
    
}