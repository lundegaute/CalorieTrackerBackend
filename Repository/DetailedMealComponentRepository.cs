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


}