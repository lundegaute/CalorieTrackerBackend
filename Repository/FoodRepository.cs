
using System.Net.Http.Json;
using CalorieTracker.Data;
using CalorieTracker.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> GetDetailedFoodFromMatvareTabellen()
    {
        var response = await _httpClient.GetFromJsonAsync<FoodWrapper>("api/nb/foods.json");


        return false;
    }

}