using CalorieTracker.Repositories;
using Books.Services;

#if HAS_BOOKS
using Books.Repository;
#endif
public static class RepositoryExtensions
{
    public static IServiceCollection AddMyRepositoryExtensions(this IServiceCollection services )
    {

        // DetailedSection
        services.AddScoped<FoodRepository>();
        services.AddScoped<DetailedMealPlanRepository>();
        services.AddScoped<DetailedMealRepository>();
        services.AddScoped<DetailedMealComponentRepository>();


        // Books Section
        #if HAS_BOOKS
                services.AddScoped<BookRepository>();
                services.AddScoped<AuthorRepository>();
                services.AddScoped<GenreRepository>();
        #endif


        return services;
    }
}