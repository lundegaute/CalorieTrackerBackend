using MongoDB.Driver;
using Microsoft.Extensions.Options;
using CalorieTracker.Data;
using CalorieTracker.Models;
using Microsoft.EntityFrameworkCore;
using CalorieTracker.Services;
using CalorieTracker.HelperMethods;
using CalorieTracker.DTO;

namespace CalorieTracker.Services
{
    public class FoodService
    {
        private readonly IMongoCollection<FoodSummary> _foodsCollection;
        private readonly IMongoCollection<Food> _detailedFoodCollection;
        private readonly DataContext _context;
        private readonly FoodSqlService _foodSqlService;

        public FoodService(DataContext context, IMongoClient mongoClient, IOptions<MongoDbSettingsClass> mongoDbSettings, FoodSqlService foodSqlService)
        {
            var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _foodsCollection = database.GetCollection<FoodSummary>("Food");
            _detailedFoodCollection = database.GetCollection<Food>("DetailedFoods");
            _foodSqlService = foodSqlService;
            _context = context;

            // Ensure unique index on Name
            var indexKeys = Builders<FoodSummary>.IndexKeys.Ascending(f => f.Name);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<FoodSummary>(indexKeys, indexOptions);
            _foodsCollection.Indexes.CreateOne(indexModel);
        }


        // ------------------------------------------ Methods
        public async Task<IEnumerable<FoodSummary>> GetFoodsAsync()
        {
            var foods = await _foodsCollection.Find(_ => true).ToListAsync();
            return foods;
        }

        public async Task<IEnumerable<ResponseFoodDTO>> Search(string search) // Change search to go to MySql instead of mongoDB
        {
            search = search.Trim().ToLower();
            if (string.IsNullOrEmpty(search)) return Enumerable.Empty<ResponseFoodDTO>();
            var searchWords = search.Split(" ");
            if (searchWords.Length == 0) return Enumerable.Empty<ResponseFoodDTO>();

            var allFoods = await _context.Foods.ToListAsync();

            var searchedFoods = allFoods
                .Where(food => searchWords
                .All(word => food.Name
                .Contains(word, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            
            var response = ResponseBuilder.Foods(searchedFoods);
            return response;
        }

        public async Task<string> LoadIntoDetailedFood(IEnumerable<Food> foods)
        {
            var isNotEmpty = await _detailedFoodCollection.Find(_ => true).AnyAsync();
            if (isNotEmpty)
            {
                return "Database Already Initialized";
            }
            foreach (var food in foods)
            {
                var filter = Builders<Food>.Filter.Eq(f => f.foodName, food.foodName);
                await _detailedFoodCollection.ReplaceOneAsync(filter, food, new ReplaceOptions { IsUpsert = true });
            }
            return "Database Initialized successfully";
        }

        public async Task<string> LoadMongoDbWithMatvaretabellen(IEnumerable<FoodSummary> foods)
        {
            var isNotEmpty = await _foodsCollection.Find(_ => true).AnyAsync();
            if (isNotEmpty)
            {
                return "Database Already initialized with data";
            }

            //var filter = Builders<FoodSummary>.Filter.Eq(f => f.Name, food.Name);
            await _foodsCollection.InsertManyAsync(foods);

            return "Database initialized successfully";
        }
    }
}