

namespace CalorieTracker.Models;


public class DetailedMealComponent
{
    public int Id { get; set; }
    public int DetailedMealId { get; set; }
    public double Quantity { get; set;}

    
    public int DetailedFoodId { get; set; }
    public required DetailedFood DetailedFood { get; set; }


}