using CalorieTracker.DTO.Requests;
using CalorieTracker.Helper.MatvaretabellenImport;
using CalorieTracker.Models;

namespace CalorieTracker.Helper.Mapping;

public static class Mapper
{

    public static DetailedFood MapFoodToDetailedFood (Food f)
    {
        return new DetailedFood
        {
            FoodId = f.foodId,
            FoodName = f.foodName,
            FoodGroupId = f.foodGroupId,

            Calories = new DbCalories
            {
                Quantity = f.calories?.quantity,
                Unit = f.calories?.unit,
            },
            Energy = new DbEnergy
            {
                Quantity = f.energy?.quantity,
                Unit = f.energy?.unit,
            },
            SearchKeywords = f.searchKeywords ?? [],
        };
    }

    public static void MapUpdateExistingFoodFromFood(Food source, DetailedFood existing)
    {
        existing.FoodId = source.foodId;
        existing.FoodName = source.foodName;
        existing.FoodGroupId = source.foodGroupId;
        existing.Calories = new DbCalories
        {
            Quantity = source.calories?.quantity ?? null,
            Unit = source.calories?.unit ?? null,
        };
        existing.Energy = new DbEnergy
        {
            Quantity = source.energy?.quantity ?? null,
            Unit = source.energy?.unit ?? null,
        };
        existing.SearchKeywords = source.searchKeywords ?? [];
    }

    public static Nutrient MapConstituentToNutrient (Constituent nutrient, Matvaretabellen_Nutrients? nutData)
    {
        return new Nutrient
        {
            NutrientId = nutrient.nutrientId,
            NutrientName = nutData?.name ?? nutrient.nutrientId,
            DefaultUnit = nutData?.unit ?? nutrient.unit,
            Category = GetNutrientCategory.GetCategory(nutrient.nutrientId).ToString(),
        };
    }
    public static void ApplyNutrientChanges(Nutrient existingNutrient, Constituent nutrient, Matvaretabellen_Nutrients nutData)
    {
        existingNutrient.NutrientName = nutData.name;
        existingNutrient.DefaultUnit = nutData.unit;
        existingNutrient.Category = GetNutrientCategory.GetCategory(nutrient.nutrientId).ToString();
    }
}