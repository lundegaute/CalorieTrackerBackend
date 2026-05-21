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
    
}