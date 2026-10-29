
using CalorieTracker.Data;
using CalorieTracker.DTO.Requests;
using CalorieTracker.Helper.Mapping;
using CalorieTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Repositories;
public class FoodRepository
{
    private readonly DataContext _context;
    private readonly HttpClient _httpClient;

    public FoodRepository(
        DataContext context,
        HttpClient httpClient
        )
    {
        _context = context;
        _httpClient = httpClient;
    }

     public async Task<DetailedFood?> GetDetailedFoodById(int foodId)
    {
        var food = await _context.DetailedFoods.FindAsync(foodId);

        return food;
    }
    public async Task<List<DetailedFood>> GetMultipleDetailedFoodById(List<int> foodIds)
    {
        var foods = await _context.DetailedFoods
            .Where(food => foodIds.Contains(food.Id) )
            .ToListAsync();

        return foods;
    }

    public async Task<bool> IsFoodNameTaken(string foodName)
    {
        var isTaken = await _context.DetailedFoods.AnyAsync(x => x.FoodName == foodName);
        return isTaken;
    }

    public async Task<List<DetailedFood>> DetailedFoodSearch(List<string> searchWords)
    {
        IQueryable<DetailedFood> query = _context.DetailedFoods
            .Include(food => food.FoodConstituents)
            .ThenInclude(con => con.Nutrient);

        foreach(var word in searchWords)
        {
            query = query.Where(food => food.FoodName.Contains(word));
        }

        var foodsFromSearch = await query.ToListAsync();
        
        return foodsFromSearch;
    }

    public async Task AddCustomFoodToDetailedFood(DetailedFood customFood)
    {
        _context.DetailedFoods.Add(customFood);
        await _context.SaveChangesAsync();
    }

    public async Task<string> GetDetailedFoodFromMatvareTabellen()
    {
        var response = await _httpClient.GetFromJsonAsync<FoodWrapper>("https://www.matvaretabellen.no/api/nb/foods.json");
        var nutrientNames = await _httpClient.GetFromJsonAsync<Matvaretabellen_Nutrients_Wrapper>("https://www.matvaretabellen.no/api/nb/nutrients.json");
        
        var foods = response.Foods;
        var nutDict = nutrientNames.nutrients.ToDictionary(
            n => n.nutrientId,
            n => n
        );

        var dbNutrients = await _context.Nutrients.ToDictionaryAsync(x => x.NutrientId);
        var existingFoods = await _context.DetailedFoods
            .Include(x => x.FoodConstituents)
            .ThenInclude(x => x.Nutrient)
            .ToDictionaryAsync(x => x.FoodId);
        
        try
        {
            
            foreach(var f in foods)
            {
                if ( existingFoods.TryGetValue(f.foodId, out var existingFood))
                {
                    Mapper.MapUpdateExistingFoodFromFood(f, existingFood);
                    var existingConstituents = existingFood.FoodConstituents
                        .ToDictionary(x => x.NutrientId);
                    foreach(var nutrient in f.constituents)
                    {
                        AddOrUpdateNutrientData(dbNutrients, nutDict, nutrient);

                        if ( existingConstituents.TryGetValue( nutrient.nutrientId, out var existingConst))
                        {
                            existingConst.Quantity = nutrient.quantity;
                        } else
                        {
                            existingFood.FoodConstituents.Add(new FoodConstituent
                            {
                                NutrientId = nutrient.nutrientId,
                                Quantity = nutrient.quantity,
                            });
                        }
                    }

                } else
                {
                    var addFood = Mapper.MapFoodToDetailedFood(f);

                
                    foreach(var nutrient in f.constituents)
                    {
                        AddOrUpdateNutrientData(dbNutrients, nutDict, nutrient);

                        addFood.FoodConstituents.Add(new FoodConstituent
                        {
                            NutrientId = nutrient.nutrientId,
                            Quantity = nutrient.quantity,
                        });
                    }

                    _context.DetailedFoods.Add(addFood);
                }

            }

            var entries = _context.ChangeTracker.Entries().ToList();

            int Count<T>(EntityState state) => 
                entries.Count(e => e.Entity is T && e.State == state);

            var foodsAdded = Count<DetailedFood>(EntityState.Added);
            var foodsUpdated = Count<DetailedFood>(EntityState.Modified);
            var foodsUnchanged = Count<DetailedFood>(EntityState.Unchanged);

            var nutrientsAdded = Count<Nutrient>(EntityState.Added);
            var nutrientsUpdated = Count<Nutrient>(EntityState.Modified);
            var nutrientsUnchanged = Count<Nutrient>(EntityState.Unchanged);

            var foodConstAdded = Count<FoodConstituent>(EntityState.Added);
            var foodConstUpdated = Count<FoodConstituent>(EntityState.Modified);
            var foodConstUnchanged = Count<FoodConstituent>(EntityState.Unchanged);

            var knownIds = _context.ChangeTracker.Entries<Nutrient>()
                .Select(e => e.Entity.NutrientId)
                .ToHashSet();

            // New constituents whose nutrient is neither in the DB nor being added
            var missing = _context.ChangeTracker.Entries<FoodConstituent>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity.NutrientId)
                .Where(id => !knownIds.Contains(id))
                .Distinct()
                .ToList();


            Console.WriteLine($"Constituents with unknown nutrient: {string.Join(", ", missing)}");
            if (missing.Count > 0)
                throw new InvalidOperationException(
                    $"Constituents reference unknown nutrients: {string.Join(", ", missing)}");

            await _context.SaveChangesAsync();

            return $"Detailed foods added: {foodsAdded} | Detailed foods modified: {foodsUpdated} | Detailed foods unchanged: {foodsUnchanged}" +
                $" | Nutrients Added: {nutrientsAdded} | Nutrients modified: {nutrientsUpdated} | Nutrients unchanged: {nutrientsUnchanged}" +
                $" | Food Constituents added: {foodConstAdded} | Food Constituents modified: {foodConstUpdated} | Food Constituents unchanged: {foodConstUnchanged}";

        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error adding Matvaretabellen to database. Error: {ex.Message}");
            throw new InvalidOperationException($"Exception: {ex.Message}");
        }
    }

    // Private methods
    private void AddOrUpdateNutrientData(
        Dictionary<string, Nutrient> dbNutrients,
        Dictionary<string, Matvaretabellen_Nutrients> nutDict,
        Constituent nutrient)
    {
        nutDict.TryGetValue(nutrient.nutrientId, out var nutData);
        
        if (dbNutrients.TryGetValue(nutrient.nutrientId, out var existing))
        {
            if (nutData is not null)
                Mapper.ApplyNutrientChanges(existing, nutrient, nutData);
        }
        else
        {
            var newNutrient = Mapper.MapConstituentToNutrient(nutrient, nutData);
            _context.Nutrients.Add(newNutrient);
            dbNutrients[newNutrient.NutrientId] = newNutrient;
        }
    }
}