using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalorieTracker.Migrations
{
    /// <inheritdoc />
    public partial class RefinedDetailedFoodAndFoodConstituentsModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodConstituents_DetailedFoods_DetailedFoodId",
                table: "FoodConstituents");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodConstituents_DetailedFoods_DetailedFoodId",
                table: "FoodConstituents",
                column: "DetailedFoodId",
                principalTable: "DetailedFoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodConstituents_DetailedFoods_DetailedFoodId",
                table: "FoodConstituents");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodConstituents_DetailedFoods_DetailedFoodId",
                table: "FoodConstituents",
                column: "DetailedFoodId",
                principalTable: "DetailedFoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
