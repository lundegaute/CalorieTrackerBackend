using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CalorieTracker.Models;
using CalorieTracker.DTO.Requests;
using CalorieTracker.DTO;

namespace CalorieTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DetailedMealPlanController : ControllerBase
{
    
}