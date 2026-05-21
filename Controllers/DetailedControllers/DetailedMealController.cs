using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CalorieTracker.DTO;
using CalorieTracker.Services;
using CalorieTracker.Extensions;

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


    [AllowAnonymous]
    [HttpGet("{mealPlanId}")]
    public async Task<ActionResult<ApiResponse<List<DetailedMealDTO>>>> GetDetailedMeals([FromRoute] int mealPlanId)
    {
        // This method validates userID
        int userID = User.GetUserId();

        if ( int.IsNegative(mealPlanId))
        {
            return ApiResponse<List<DetailedMealDTO>>.Failure(["MealPlanID can not be a negative number"], ["BadRequest"], 400);
        }

        var response = await _detailedMealService.GetDetailedMeals(userID, mealPlanId);
        return Ok(response);

    }
}