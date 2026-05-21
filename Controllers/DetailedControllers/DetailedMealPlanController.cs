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


    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<DetailedMealPlanDTO>>>> GetEntireDetailedMealPlan()
    {
        var userID = User.GetUserId();

        var response = await _detailedMealPlanService.GetEntireDetailedMealPlan(userID);

        return Ok(response);

    }

    [HttpGet("mealplans")]
    public async Task<ActionResult<ApiResponse<List<DetailedMealPlanDTO>>>> GetDetailedMealPlans()
    {
        int userID = User.GetUserId();
        
        var apiResponse = await _detailedMealPlanService.GetDetailedMealPlans( userID );

        return Ok( apiResponse );
    }

    
    
}