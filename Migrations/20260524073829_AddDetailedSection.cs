using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CalorieTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailedSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DetailedFoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FoodId = table.Column<string>(type: "varchar(255)", nullable: false),
                    FoodName = table.Column<string>(type: "varchar(255)", nullable: false),
                    FoodGroupId = table.Column<string>(type: "longtext", nullable: false),
                    Calories_Quantity = table.Column<int>(type: "int", nullable: true),
                    Calories_Unit = table.Column<string>(type: "longtext", nullable: true),
                    Energy_Quantity = table.Column<double>(type: "double", nullable: true),
                    Energy_Unit = table.Column<string>(type: "longtext", nullable: true),
                    SearchKeywords = table.Column<string>(type: "LONGTEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedFoods", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DetailedMealPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedMealPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailedMealPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Nutrients",
                columns: table => new
                {
                    NutrientId = table.Column<string>(type: "varchar(255)", nullable: false),
                    NutrientName = table.Column<string>(type: "longtext", nullable: false),
                    DefaultUnit = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrients", x => x.NutrientId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DetailedMeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    DetailedMealPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailedMeals_DetailedMealPlans_DetailedMealPlanId",
                        column: x => x.DetailedMealPlanId,
                        principalTable: "DetailedMealPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FoodConstituents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DetailedFoodId = table.Column<int>(type: "int", nullable: false),
                    NutrientId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Quantity = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodConstituents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodConstituents_DetailedFoods_DetailedFoodId",
                        column: x => x.DetailedFoodId,
                        principalTable: "DetailedFoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodConstituents_Nutrients_NutrientId",
                        column: x => x.NutrientId,
                        principalTable: "Nutrients",
                        principalColumn: "NutrientId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DetailedMealComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DetailedMealId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    DetailedFoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailedMealComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailedMealComponents_DetailedFoods_DetailedFoodId",
                        column: x => x.DetailedFoodId,
                        principalTable: "DetailedFoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetailedMealComponents_DetailedMeals_DetailedMealId",
                        column: x => x.DetailedMealId,
                        principalTable: "DetailedMeals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DetailedFoods_FoodId",
                table: "DetailedFoods",
                column: "FoodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailedFoods_FoodName",
                table: "DetailedFoods",
                column: "FoodName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailedMealComponents_DetailedFoodId",
                table: "DetailedMealComponents",
                column: "DetailedFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailedMealComponents_DetailedMealId",
                table: "DetailedMealComponents",
                column: "DetailedMealId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailedMealPlans_UserId",
                table: "DetailedMealPlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailedMeals_DetailedMealPlanId",
                table: "DetailedMeals",
                column: "DetailedMealPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodConstituents_DetailedFoodId",
                table: "FoodConstituents",
                column: "DetailedFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodConstituents_NutrientId",
                table: "FoodConstituents",
                column: "NutrientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailedMealComponents");

            migrationBuilder.DropTable(
                name: "FoodConstituents");

            migrationBuilder.DropTable(
                name: "DetailedMeals");

            migrationBuilder.DropTable(
                name: "DetailedFoods");

            migrationBuilder.DropTable(
                name: "Nutrients");

            migrationBuilder.DropTable(
                name: "DetailedMealPlans");
        }
    }
}
