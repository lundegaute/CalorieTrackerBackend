using CalorieTracker.Repositories;

public static class RepositoryExtensions
{
    public static IServiceCollection AddMyRepositoryExtensions(this IServiceCollection services )
    {

        // DetailedSection
        services.AddScoped<FoodRepository>();
        services.AddScoped<DetailedMealPlanRepository>();
        services.AddScoped<DetailedMealRepository>();
        services.AddScoped<DetailedMealComponentRepository>();

        return services;
    }
}