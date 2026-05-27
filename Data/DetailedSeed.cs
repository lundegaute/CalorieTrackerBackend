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

        var foodItem1 = await _context.DetailedFoods.FirstOrDefaultAsync(x => x.Id == 758);
        var foodItem2 = await _context.DetailedFoods.FirstOrDefaultAsync(x => x.Id == 1514);
        if ( foodItem1 is null ) 
            throw new KeyNotFoundException("Food not found in database. Make sure to run the correct endpoint to fill DB with detailedFood data");
        if ( foodItem2 is null ) 
            throw new KeyNotFoundException("Food not found in database. Make sure to run the correct endpoint to fill DB with detailedFood data");

        var mealPlan2 = new DetailedMealPlan
        {
            Name = "MealPlan#2",
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
                            DetailedFood = foodItem1
                        },
                        new DetailedMealComponent
                        {
                            Quantity = 50,
                            DetailedFood = foodItem2
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
                            DetailedFood = foodItem1
                        },
                        new DetailedMealComponent
                        {
                            Quantity = 270,
                            DetailedFood = foodItem2
                        }
                    }
                }
            }
        };

        _context.DetailedMealPlans.Add(mealPlan2);
        await _context.SaveChangesAsync();

    }
}