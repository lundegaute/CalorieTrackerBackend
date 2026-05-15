

public static class HttpExtensions
{
    public static IServiceCollection AddMyHttpExtensions (this IServiceCollection services )
    {
        services.AddHttpClient<FoodRepository>(client =>
        {
           client.BaseAddress = new Uri("https://www.matvaretabellen.no/");
            // api/nb/foods.json
        });

        return services;
    }
}