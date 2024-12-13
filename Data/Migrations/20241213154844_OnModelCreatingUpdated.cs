using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class OnModelCreatingUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ItemRestaurant",
                columns: table => new
                {
                    MenuId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    RestaurantsId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemRestaurant", x => new { x.MenuId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_ItemRestaurant_Items_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemRestaurant_Restaurants_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemRestaurant_RestaurantsId",
                table: "ItemRestaurant",
                column: "RestaurantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemRestaurant");

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
    }
}
