using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CalorieTracker.DTO;
using CalorieTracker.Services;
using CalorieTracker.Extensions;
using CalorieTracker.DTO.Requests;

namespace CalorieTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DetailedMealController : ControllerBase
{
    private readonly DetailedMealService _detailedMealService;

    public DetailedMealController(DetailedMealService detailedMealService)
    {
        _detailedMealService = detailedMealService;
    }

    /// <summary>
    /// Add a single meal to your MealPlan: Frokost, Middag, Kveldsmat
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task<ActionResult<ApiResponse<string>>> AddDetailedMeal([FromBody] DetailedMealRequest request)
    {
        // If userID is not found, this throws an error
        var userID = User.GetUserId();

        var response = await _detailedMealService.AddDetailedMeal(userID, request);

        if ( response.IsSuccess) 
            return Ok(response);
        else 
            return BadRequest(response);
    }
}