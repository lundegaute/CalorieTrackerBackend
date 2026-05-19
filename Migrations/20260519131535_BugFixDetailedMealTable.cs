using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalorieTracker.Migrations
{
    /// <inheritdoc />
    public partial class BugFixDetailedMealTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealPlanId",
                table: "DetailedMeals");

            migrationBuilder.AlterColumn<int>(
                name: "DetailedMealPlanId",
                table: "DetailedMeals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DetailedMealPlanId",
                table: "DetailedMeals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MealPlanId",
                table: "DetailedMeals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
