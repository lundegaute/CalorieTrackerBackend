using System.Text.Json;
using CalorieTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<FoodSummarySql> Foods { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealName> MealNames { get; set; }
        public DbSet<Meal> Meals { get; set; }


        // Detailed Section ------------------------------------
        public DbSet<DetailedMealPlan> DetailedMealPlans { get; set; }
        public DbSet<DetailedMeal> DetailedMeals { get; set; }
        public DbSet<DetailedMealComponent> DetailedMealComponents { get; set; }
        public DbSet<DetailedFood> DetailedFoods { get; set; }
        public DbSet<Nutrient> Nutrients { get; set; }
        public DbSet<FoodConstituent> FoodConstituents { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Quantity).IsRequired();
                entity
                    .HasOne(m => m.MealName)
                    .WithMany(mn => mn.Meals)
                    .OnDelete(DeleteBehavior.Cascade);
                entity
                    .HasOne(m => m.Food)
                    .WithMany(f => f.Meals)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<MealName>(entity =>
            {
                entity.HasKey(mn => mn.Id);
                entity.Property(mn => mn.Name).IsRequired();
                entity.HasOne(mn => mn.MealPlan)
                    .WithMany(mp => mp.MealNames)
                    .OnDelete(DeleteBehavior.Cascade);
                entity
                    .HasOne(mn => mn.User)
                    .WithMany(u => u.MealNames);
            });
            modelBuilder.Entity<MealPlan>(entity =>
            {
                entity.HasKey(mp => mp.Id);
                entity.Property(mp => mp.Name).IsRequired();

                entity.HasOne(mp => mp.User)
                    .WithMany(u => u.MealPlans)
                    .HasForeignKey(mp => mp.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<FoodSummarySql>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.HasIndex(f => f.Name).IsUnique();
                entity.Property(f => f.Name).IsRequired();
                entity.Property(f => f.Protein).IsRequired();
                entity.Property(f => f.Carbohydrates).IsRequired();
                entity.Property(f => f.Fat).IsRequired();
                entity.Property(f => f.Calories).IsRequired();
            });


            // Detailed Section ---------------------------------

            modelBuilder.Entity<DetailedMealPlan>(entity =>
            {
                entity.HasKey(plan => plan.Id);

                // Deleting mealplan will delete related meals and mealComponents but not detailedFood
                entity.HasMany(plan => plan.DetailedMeals)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(plan => plan.User)
                    .WithMany()
                    .HasForeignKey(plan => plan.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DetailedMeal>(entity =>
            {
                entity.HasKey(dm => dm.Id);

                // If a meal is deleted, the related meal components will also be deleted
                entity.HasMany(dm => dm.Components)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<DetailedMealComponent>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Quantity).IsRequired();

                // Deleting a mealComponent, should not delete detailedFoods
                entity.HasOne(x => x.DetailedFood)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
                
            });

            modelBuilder.Entity<DetailedFood>(entity =>
            {
                entity.HasKey(df => df.Id);
                entity.HasIndex(df => df.FoodId).IsUnique();
                entity.HasIndex(df => df.FoodName).IsUnique();

                entity.OwnsOne(df => df.Energy);
                entity.OwnsOne(df => df.Calories);

                entity.HasMany(df => df.FoodConstituents)
                    .WithOne()
                    .HasForeignKey(fc => fc.DetailedFoodId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.Property(df => df.SearchKeywords)
                    .HasColumnType("LONGTEXT") // Stores it as long text in MySQL
                    .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), // C# List -> JSON String
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>() // JSON String -> C# List
                    );
          
            });
            
            modelBuilder.Entity<FoodConstituent>(entity =>
            {
                entity.HasKey(f => f.Id);

                entity.HasOne(f => f.Nutrient)
                    .WithMany()
                    .HasForeignKey(f => f.NutrientId);
            });

            modelBuilder.Entity<Nutrient>(entity =>
            {
                entity.HasKey(n => n.NutrientId);
                entity.Property(n => n.Category)
                    .HasMaxLength(50)
                    .HasDefaultValue("Other");
            });

            // User Section ---------------------------------------------

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Role).IsRequired();
                entity
                    .HasMany(u => u.MealNames)
                    .WithOne(mn => mn.User);
                entity.HasMany(u => u.MealPlans)
                    .WithOne(mp => mp.User);
            });
        }
    }
}