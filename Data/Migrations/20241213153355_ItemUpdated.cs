using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ItemUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "Restaurants",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_ItemId",
                table: "Restaurants",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Items_ItemId",
                table: "Restaurants",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Items_ItemId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_ItemId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Restaurants");
        }
    }
}
