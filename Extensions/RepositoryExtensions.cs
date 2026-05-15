

public static class RepositoryExtensions
{
    public static IServiceCollection AddMyRepositoryExtensions(this IServiceCollection services )
    {
        services.AddScoped<FoodRepository>();


        return services;
    }
}