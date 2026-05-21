

namespace CalorieTracker.Routes;

public static class ApiRoutes
{
    public const string Base = "api/";


    public static class Route
    {
        // Detailed MealPlans
        public const string DetailedMealPlan = Base + "/DetailedMealPlan";

        // Detailed Meals
        public const string DetailedMeal = Base + "/DetailedMeal";
    }
}