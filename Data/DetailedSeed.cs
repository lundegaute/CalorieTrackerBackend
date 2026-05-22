using Microsoft.EntityFrameworkCore;
using CalorieTracker.Data;
using CalorieTracker.Models;

namespace CalorieTracker.Seed;
public static class DetailedSeed
{
    public async static Task SeedDevelopmentData(DataContext _context)
    {
        if ( await _context.DetailedMealPlans.AnyAsync())
        {
            Console.WriteLine("Database already initialized with required data"); 
            return;
        }

        var user = await _context.Users.FirstOrDefaultAsync();
        if ( user is null ) 
            throw new KeyNotFoundException("User database is empty. Create a user before restarting application");

        var havregryn = await _context.DetailedFoods.FirstOrDefaultAsync(x => x.Id == 582);
        var melk = await _context.DetailedFoods.FirstOrDefaultAsync(x => x.Id == 1749);
        if ( havregryn is null ) 
            throw new KeyNotFoundException("Food not found in database. Make sure to run the correct endpoint to fill DB with detailedFood data");

        var mealPlan1 = new DetailedMealPlan
        {
            Name = "MealPlan#1",
            UserId = user.Id,
            DetailedMeals = new List<DetailedMeal>
            {
                new DetailedMeal
                {
                    Name = "Frokost",
                    Components = new List<DetailedMealComponent>
                    {
                        new DetailedMealComponent
                        {
                            Quantity = 100,
                            DetailedFood = havregryn
                        },
                        new DetailedMealComponent
                        {
                            Quantity = 225,
                            DetailedFood = melk
                        }
                    }
                },
                new DetailedMeal
                {
                    Name = "Middag",
                    Components = new List<DetailedMealComponent>
                    {
                        new DetailedMealComponent
                        {
                            Quantity = 120,
                            DetailedFood = havregryn
                        },
                        new DetailedMealComponent
                        {
                            Quantity = 270,
                            DetailedFood = melk
                        }
                    }
                }
            }
        };

        _context.DetailedMealPlans.Add(mealPlan1);
        await _context.SaveChangesAsync();

    }
}