

namespace CalorieTracker.DTO.Constituents;


public class MicroNutrientSummary
{
    public string NutrientId { get; set; } // VitA
    public string NutrientName { get; set; } // Vitamin A
    public double TotalQuantity { get; set; }
    public string? Unit { get; set; } // mg, g
    public required string Category { get; set; }

}