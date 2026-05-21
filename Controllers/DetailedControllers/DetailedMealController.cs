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


}