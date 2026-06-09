using Microsoft.EntityFrameworkCore;
using CalorieTracker.Data;
using CalorieTracker.Models;

namespace CalorieTracker.Repositories;


public class DetailedMealComponentRepository
{
    private readonly DataContext _context;


    public DetailedMealComponentRepository(DataContext context )
    {
        _context = context;
    }

    public async Task<string> AddMealComponent(DetailedMealComponent newMealComponent)
    {
        _context.DetailedMealComponents.Add(newMealComponent);
        await _context.SaveChangesAsync();

        return "Meal Added";
    }

    public async Task<string> DeleteMealComponent(DetailedMealComponent detailedMealComponent)
    {
        _context.Remove(detailedMealComponent);
        await _context.SaveChangesAsync();
        return "Delete Successfull";
    }

}