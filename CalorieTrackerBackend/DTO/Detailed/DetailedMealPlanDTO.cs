

namespace CalorieTracker.DTO;

public class DetailedMealPlanDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int UserId { get; set; }

    //public List<DetailedMealDTO> DetailedMeals { get; set; } = new();
    //public User? User { get; set; }
}