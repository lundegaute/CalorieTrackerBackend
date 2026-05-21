using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CalorieTracker.DTO;
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

    
}