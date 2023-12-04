using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicShop.Migrations
{
    /// <inheritdoc />
    public partial class ReverseCartID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineUser_ShoppingCart_CartId",
                table: "OnlineUser");

            migrationBuilder.DropIndex(
                name: "IX_OnlineUser_CartId",
                table: "OnlineUser");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_CartId",
                table: "ShoppingCart",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_OnlineUser_CartId",
                table: "ShoppingCart",
                column: "CartId",
                principalTable: "OnlineUser",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_OnlineUser_CartId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_CartId",
                table: "ShoppingCart");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineUser_CartId",
                table: "OnlineUser",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineUser_ShoppingCart_CartId",
                table: "OnlineUser",
                column: "CartId",
                principalTable: "ShoppingCart",
                principalColumn: "RecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
