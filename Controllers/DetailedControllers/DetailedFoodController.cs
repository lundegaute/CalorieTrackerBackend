using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CalorieTracker.DTO;
using CalorieTracker.DTO.Requests;
using CalorieTracker.Models;
using CalorieTracker.Services;

namespace CalorieTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DetailedFoodController : ControllerBase
{
    private readonly DetailedFoodService _detailedFoodService;

    public DetailedFoodController(
        DetailedFoodService detailedFoodService)
    {
        _detailedFoodService = detailedFoodService;
    }

    [HttpPost("search")]
    public async Task<ActionResult<ApiResponse<List<DetailedFoodDTO>>>> DetailedFoodSearch([FromBody] string search)
    {
        var apiResponse = await _detailedFoodService.DetailedFoodSearch(search);
        
        return Ok(apiResponse);
    }




    /// <summary>
    /// Fills database with detailed data from matvaretabellen, returning a string with how many rows added to each table
    /// </summary>
    /// <response code="200">Returns an ApiResponse with metadata.</response>
    /// <response code="500">If there is an internal server error.</response>
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<ApiResponse<string>>> GetDetailedFoodsAsync()
    {

        var response = await _detailedFoodService.AddDetailedFromMatvaretabellen();

        return Ok(response);
    }

}