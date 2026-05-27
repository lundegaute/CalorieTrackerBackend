using Microsoft.EntityFrameworkCore;
using CalorieTracker.Data;
using CalorieTracker.Models;


namespace CalorieTracker.Repositories;

public class DetailedMealPlanRepository
{
    private readonly DataContext _context;

    public DetailedMealPlanRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<string> AddDetailedMealPlan(DetailedMealPlan newDetailedMealPlan)
    {
        _context.DetailedMealPlans.Add(newDetailedMealPlan);
        await _context.SaveChangesAsync();

        return $"New mealplan added successfully";
    }

    // Gets the entire MealPlans -> meals -> mealcomponents -> foods, for current user
    public async Task<List<DetailedMealPlan>> GetEntireDetailedMealPlan(int userID)
    {
        var detailedMealPlans = await _context.DetailedMealPlans
            .AsNoTracking()
            .Include(mp => mp.DetailedMeals)
                .ThenInclude(m => m.Components)
                    .ThenInclude(c => c.DetailedFood)
                        .ThenInclude(f => f.FoodConstituents)
                            .ThenInclude(fc => fc.Nutrient)
            .Where(mp => mp.UserId == userID)
            .ToListAsync();
        
        return detailedMealPlans;

    }

    public async Task<List<DetailedMealPlan>> GetDetailedMealPlansAsync(int userID)
    {
        var mealPlans = await _context.DetailedMealPlans
            .Where(mp => mp.UserId == userID)
            .ToListAsync();

        return mealPlans;
    }

    public async Task<List<int>> GetUserDetailedMealPlanIdsAsync(int userID)
    {
        var detailedMealPlanIds = await _context.DetailedMealPlans
            .Where(mp => mp.UserId == userID)
            .Select(mp => mp.Id)
            .ToListAsync();
        
        return detailedMealPlanIds;
    }

}