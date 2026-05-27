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
    
    public async Task<string> AddDetailedMeal(DetailedMeal newDetailedMeal)
    {
        _context.DetailedMeals.Add(newDetailedMeal);
        await _context.SaveChangesAsync();

        return "Meal added successfully";
    }

}