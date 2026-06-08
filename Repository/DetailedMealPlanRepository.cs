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

    public async Task<List<int>> GetUserDetailedMealPlanIdsAsync(int userID)
    {
        var detailedMealPlanIds = await _context.DetailedMealPlans
            .Where(mp => mp.UserId == userID)
            .Select(mp => mp.Id)
            .ToListAsync();
        
        return detailedMealPlanIds;
    }

    /// <summary>
    /// If name exists in the table for this specific user, return true
    /// </summary>
    /// <param name="name"></param>
    /// <param name="userID"></param>
    /// <returns></returns>
    public async Task<bool> IsNameTaken(string name, int userID)
    {
        var isNameTaken = await _context.DetailedMealPlans
            .Where(plan => plan.UserId == userID)
            .AnyAsync(plan => plan.Name == name);

        return isNameTaken;
    }

    public async Task<List<int>> GetUserMealIds(int userID)
    {
        var mealIds = await _context.DetailedMealPlans
            .Include(plan => plan.DetailedMeals)
            .Where(plan => plan.UserId == userID)
            .SelectMany(plan => plan.DetailedMeals
                .Select(meals => meals.Id))
            .ToListAsync();
        
        return mealIds;
    }

}