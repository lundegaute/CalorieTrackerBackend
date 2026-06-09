using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CalorieTracker.DTO;
using CalorieTracker.DTO.Requests;
using CalorieTracker.Services;
using CalorieTracker.Extensions;

namespace CalorieTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DetailedMealComponentController : ControllerBase
{
    private readonly DetailedMealComponentService _detailedMealComponentService;

    public DetailedMealComponentController(DetailedMealComponentService detailedMealComponentService)
    {
        _detailedMealComponentService = detailedMealComponentService;
    }

    /// <summary>
    /// Adds a single new foodItem from detailedFoods to the currently selected meal
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public async Task<ActionResult<ApiResponse<string>>> AddMealComponent([FromBody] DetailedMealComponentRequest request)
    {
        var userID = User.GetUserId();
        var response = await _detailedMealComponentService.AddMealComponent(userID, request);

        if ( response.IsSuccess) 
            return Ok(response);
        else
            return BadRequest(response);
        
    }
    
    [HttpDelete("delete/{mealComponentID}")]
    public async Task<ActionResult<ApiResponse<string>>> DeleteDetailedMealComponent([FromRoute] int mealComponentID)
    {
        var userID = User.GetUserId();
        var response = await _detailedMealComponentService.DeleteMealComponent(userID, mealComponentID);

        if ( response.IsSuccess )
            return Ok(response);
        else
            return BadRequest(response);
    }
}