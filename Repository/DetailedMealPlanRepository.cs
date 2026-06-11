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

    public async Task<int> AddDetailedMealPlan(DetailedMealPlan newDetailedMealPlan)
    {
        _context.DetailedMealPlans.Add(newDetailedMealPlan);
        await _context.SaveChangesAsync();

        return newDetailedMealPlan.Id;
    }

    /// <summary> 
    /// Gets the entire MealPlans -> meals -> mealcomponents -> foods, for current user
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns a detailedMealPlan for the logged i user
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="mealPlanID"></param>
    /// <returns></returns>
    public async Task<DetailedMealPlan?> GetUserMealPlan(int userID, int mealPlanID)
    {
        var mealPlan = await _context.DetailedMealPlans
            .Where(plan => plan.UserId == userID)
            .FirstOrDefaultAsync(plan => plan.Id == mealPlanID);
        
        return mealPlan;
    }


    /// <summary>
    /// Returns a list of the mealplanIDs belonging to the logged in user
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public async Task<List<int>> GetUserDetailedMealPlanIdsAsync(int userID)
    {
        var detailedMealPlanIds = await _context.DetailedMealPlans
            .Where(mp => mp.UserId == userID)
            .Select(mp => mp.Id)
            .ToListAsync();
        
        return detailedMealPlanIds;
    }

    public async Task<DetailedMealComponent?> GetUserMealComponent(int userID, int mealComponentID)
    {
        var mealComponent = await _context.DetailedMealPlans
            .Include(plan => plan.DetailedMeals)
                .ThenInclude(meal => meal.Components)
            .Where(plan => plan.UserId == userID)
            .SelectMany(plan => plan.DetailedMeals)
                .SelectMany(meal => meal.Components)
                    .FirstOrDefaultAsync(comp => comp.Id == mealComponentID);

        return mealComponent;
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

    /// <summary>
    /// Returns a detailedMeal belonging to the logged in user
    /// </summary>
    /// <param name="mealID"></param>
    /// <param name="userID"></param>
    /// <returns></returns>
    public async Task<DetailedMeal?> GetDetailedMealById(int mealID, int userID)
    {
        var meal = await _context.DetailedMealPlans
            .Where(plan => plan.UserId == userID)
            .SelectMany(plan => plan.DetailedMeals)
                .FirstOrDefaultAsync(meal => meal.Id == mealID);
        
        return meal;
    }

    /// <summary>
    /// Returns a list of the users mealIDs. Usefull to check if mealId from requests belong to current user
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns a list of meal names for the logged in user
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public async Task<List<string>> GetUserMealNames(int userID, int mealPlanID)
    {
        var mealNames = await _context.DetailedMealPlans
            .Include(plan => plan.DetailedMeals)
            .Where(plan => plan.UserId == userID && 
                plan.Id == mealPlanID)
            .SelectMany(plan => plan.DetailedMeals)
                .Select(meal => meal.Name)
            .ToListAsync();
        
        return mealNames;
    }

    public async Task<string> DeleteMealPlanById(DetailedMealPlan detailedMealPlan)
    {
        _context.DetailedMealPlans.Remove(detailedMealPlan);
        await _context.SaveChangesAsync();
        return "Delete Successful";
    } 

}