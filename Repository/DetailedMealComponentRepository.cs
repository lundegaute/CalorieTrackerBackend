using Microsoft.EntityFrameworkCore;
using CalorieTracker.Data;
using CalorieTracker.Models;

namespace CalorieTracker.Repositories;


public class DetailedMealComponentRepository
{
    private readonly DataContext _context;
    private readonly ILogger<DetailedMealComponentRepository> _logger;


    public DetailedMealComponentRepository(DataContext context, ILogger<DetailedMealComponentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<string> AddMealComponents(List<DetailedMealComponent> newMealComponents)
    {
        _context.DetailedMealComponents.AddRange(newMealComponents);
        await _context.SaveChangesAsync();

        return "Meal Added";
    }

    public async Task UpdateComponentQuantity(double quantity, int componentID)
    {
        try
        {
            var rowsUpdated = await _context.DetailedMealComponents
                .Where(x => x.Id == componentID)
                .ExecuteUpdateAsync(x => x.SetProperty(x => x.Quantity, quantity));

            _logger.LogInformation("MealComponentID: {Component}, updated successfully", componentID);
        } catch (Exception ex) 
        {
            _logger.LogError(ex, "Error when updating quantity of meal componentID: {ComponentID}. Quantity: {Quantity}", componentID, quantity);
            throw;
        }
        
    }

    public async Task<string> DeleteMealComponent(DetailedMealComponent detailedMealComponent)
    {
        _context.Remove(detailedMealComponent);
        await _context.SaveChangesAsync();
        return "Delete Successfull";
    }

}