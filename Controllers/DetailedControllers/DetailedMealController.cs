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

    [HttpPost]
    public async Task<ActionResult<ApiResponse<string>>> AddDetailedMeal([FromBody] DetailedMealRequest request)
    {
        var userID = User.GetUserId();

        var response = await _detailedMealService.AddDetailedMeal(userID, request);

        return Ok(response);
    }
}