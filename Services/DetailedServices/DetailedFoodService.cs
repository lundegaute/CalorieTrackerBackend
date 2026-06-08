
using CalorieTracker.DTO;
using CalorieTracker.Repositories;
using CalorieTracker.DTO.Requests;
using CalorieTracker.Models;

namespace CalorieTracker.Services;
public class DetailedFoodService
{
    private readonly FoodRepository _foodRepo;

    public DetailedFoodService(
        FoodRepository foodRepo )
    {
        _foodRepo = foodRepo;
    }


    public async Task<ApiResponse<string>> AddDetailedFromMatvaretabellen()
    {
        var result = await _foodRepo.GetDetailedFoodFromMatvareTabellen();


        return ApiResponse<string>.Success(result, 200);
    }

    public async Task<ApiResponse<List<DetailedFoodDTO>>> DetailedFoodSearch(string search)
    {
        // Clean up the search
        search = search.Trim().ToLower();
        if ( string.IsNullOrEmpty(search))
            return ApiResponse<List<DetailedFoodDTO>>.Success([], 200);

        // Split into a list of each word seperated by space [jasmin, ris]
        var searchWords = search.Split(" ").ToList();

        var foods = await _foodRepo.DetailedFoodSearch(searchWords);

        // DO THIS PROPERLY: GET CORRECT DATA FROM DATABASE BASED ON SEARCH

        var detailedFoodDTOs = foods.Select( food => new DetailedFoodDTO
                    {
                        Id = food.Id,
                        FoodName = food.FoodName,
                        FoodGroupId = food.FoodGroupId,
                        Calories = food.Calories?.Quantity,
                        Energy = food.Energy?.Quantity,
                        Constituents = food.FoodConstituents.Select(fc => new FoodConstituentDTO
                        {
                            Id = fc.Id,
                            NutrientId = fc.NutrientId,
                            Nutrient = new NutrientDTO
                            {
                                NutrientId = fc.NutrientId,
                                NutrientName = fc.Nutrient.NutrientName,
                                DefaultUnit = fc.Nutrient.DefaultUnit,
                                Category = fc.Nutrient.Category,
                            },
                            Quantity = fc.Quantity ?? 0,
                        }).ToList(),
                    }).ToList();

        var apiResponse = ApiResponse<List<DetailedFoodDTO>>.Success(detailedFoodDTOs, 200);

        return apiResponse;
    }

}