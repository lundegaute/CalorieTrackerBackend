using CalorieTracker.Services;
#if HAS_BOOKS
using Books.Services;
#endif

public static class ServiceExtensions
{
    public static IServiceCollection AddMyServiceExtensions (this IServiceCollection services)
    {
        // Simple Section
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

#if HAS_BOOKS
        // Books Section
        services.AddScoped<BookService>();
#endif

        return services;
    }
}