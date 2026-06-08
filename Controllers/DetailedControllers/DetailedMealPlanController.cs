using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CalorieTracker.Models;
using CalorieTracker.DTO.Requests;
using CalorieTracker.DTO;
using CalorieTracker.Services;

namespace CalorieTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DetailedMealPlanController : ControllerBase
{
    private readonly DetailedMealPlanService _detailedMealPlanService;

    public DetailedMealPlanController(DetailedMealPlanService detailedMealPlanService)
    {
        _detailedMealPlanService = detailedMealPlanService;
    }

    /// <summary>
    /// Adds a single MealPlan to the current user: MealPlan#1, MealPlan#2, BulkPlan, WeightLossPlan
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task<ActionResult<ApiResponse<string>>> AddDetailedMealPlan([FromBody] DetailedMealPlanRequest request)
    {
        var userID = User.GetUserId();
        
        var response = await _detailedMealPlanService.AddDetailedMealPlan(userID, request);

        if ( response.IsSuccess )
            return Ok(response);
        else
            return BadRequest(response);
    }

    [HttpGet("overview")]
    public async Task<ActionResult<ApiResponse<List<DetailedCompleteOverviewDTO>>>> GetEntireDetailedMealPlan()
    {
        var userID = User.GetUserId();

        var response = await _detailedMealPlanService.GetEntireDetailedMealPlan(userID);

        return Ok(response);

    }

    
    
}