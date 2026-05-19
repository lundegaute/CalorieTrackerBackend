
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using CalorieTracker.Data;
using CalorieTracker.DTO.Requests;
using CalorieTracker.Models;

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

    public async Task<string> GetDetailedFoodFromMatvareTabellen()
    {
        var response = await _httpClient.GetFromJsonAsync<FoodWrapper>("https://www.matvaretabellen.no/api/nb/foods.json");
        var nutrientNames = await _httpClient.GetFromJsonAsync<Matvaretabellen_Nutrients_Wrapper>("https://www.matvaretabellen.no/api/nb/nutrients.json");
        
        var foods = response.Foods;
        var nutDict = nutrientNames.nutrients.ToDictionary(
            n => n.nutrientId,
            n => n
        );

        var detailedFoodsCounter = 0;
        var NutrientCounter = 0;
        var foodConstituentsCounter = 0;

        var dbNutrients = (await _context.Nutrients.Select(n => n.NutrientId).ToListAsync()).ToHashSet();
        
        try
        {
            
            foreach(var f in foods)
            {
                // If food is already in DB, we skip it
                if ( await _context.DetailedFoods.AnyAsync(df => df.FoodId == f.foodId) )
                {
                    continue;
                }

                var addFood = new DetailedFood
                {
                    FoodId = f.foodId,
                    FoodName = f.foodName,
                    FoodGroupId = f.foodGroupId,
                    
                    Calories = new DbCalories
                    {
                        Quantity = f.calories?.quantity ?? null,
                        Unit = f.calories?.unit ?? null,
                    },
                    Energy = new DbEnergy
                    {
                        Quantity = f.energy?.quantity ?? null,
                        Unit = f.energy?.unit ?? null,
                    },

                    SearchKeywords = f.searchKeywords ?? new List<string>(),
                };

                _context.DetailedFoods.Add(addFood);
                detailedFoodsCounter += 1;
                
                foreach(var nutrient in f.constituents)
                {
                    if ( !dbNutrients.Contains(nutrient.nutrientId))
                    {
                        nutDict.TryGetValue(nutrient.nutrientId, out var nutData);
                        
                        var newNutrient = new Nutrient
                        {
                            NutrientId = nutrient.nutrientId,
                            NutrientName = nutData?.name ?? nutrient.nutrientId,
                            DefaultUnit = nutData?.unit ?? nutrient.unit,
                        };

                        _context.Nutrients.Add(newNutrient);
                        NutrientCounter += 1;

                        dbNutrients.Add(nutrient.nutrientId);
                    }

                    var newFoodConstituent = new FoodConstituent
                    {
                        Food = addFood,
                        NutrientId = nutrient.nutrientId,
                        Quantity = nutrient.quantity ?? null,
                    };

                    _context.FoodConstituents.Add(newFoodConstituent);
                    foodConstituentsCounter += 1;
                }
            }

            await _context.SaveChangesAsync();
            return $"Detailed Foods added: {detailedFoodsCounter} | Nutrients Added: {NutrientCounter} | Food Constituents added: {foodConstituentsCounter}";

        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error adding Matvaretabellen to database. Error: {ex.Message}");
            throw new InvalidOperationException($"Exception: {ex.Message}");
        }


    }

}