using CalorieTracker.Services;


public static class ServiceExtensions
{
    public static IServiceCollection AddMyServiceExtensions (this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<MealNameService>();
        services.AddScoped<FoodSqlService>();
        services.AddScoped<MealService>();
        services.AddScoped<MealPlanService>();

        // Detailed Section
        services.AddScoped<DetailedMealPlanService>();
        services.AddScoped<DetailedFoodService>();
        services.AddScoped<DetailedMealService>();
        services.AddScoped<DetailedMealComponentService>();


        return services;
    }
}