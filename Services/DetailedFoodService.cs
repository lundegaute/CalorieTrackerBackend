
using CalorieTracker.DTO;
using CalorieTracker.DTO.Requests;
using CalorieTracker.Models;

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

}