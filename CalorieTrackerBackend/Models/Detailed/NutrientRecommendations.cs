namespace CalorieTracker.Models.Detailed;

public class NutrientRecommendation
{
    public required string NutrientId { get; set; }
    public required int MinAge { get; set; }
    public required int MaxAge { get; set; }
    public required string Gender { get; set; }
    public required double Quantity { get; set; }
    public required string Unit { get; set; }
}
