using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CalorieTracker.Migrations
{
    /// <inheritdoc />
    public partial class DebugedNewIdForDetailedFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodConstituents_DetailedFoods_Id",
                table: "FoodConstituents");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "FoodConstituents");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FoodConstituents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "DetailedFoodId",
                table: "FoodConstituents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FoodConstituents_DetailedFoodId",
                table: "FoodConstituents",
                column: "DetailedFoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodConstituents_DetailedFoods_DetailedFoodId",
                table: "FoodConstituents",
                column: "DetailedFoodId",
                principalTable: "DetailedFoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodConstituents_DetailedFoods_DetailedFoodId",
                table: "FoodConstituents");

            migrationBuilder.DropIndex(
                name: "IX_FoodConstituents_DetailedFoodId",
                table: "FoodConstituents");

            migrationBuilder.DropColumn(
                name: "DetailedFoodId",
                table: "FoodConstituents");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FoodConstituents",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "FoodId",
                table: "FoodConstituents",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodConstituents_DetailedFoods_Id",
                table: "FoodConstituents",
                column: "Id",
                principalTable: "DetailedFoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
