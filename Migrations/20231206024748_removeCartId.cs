using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicShop.Migrations
{
    /// <inheritdoc />
    public partial class removeCartId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_OnlineUser_CartId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "OnlineUser");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "ShoppingCart",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_CartId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_OnlineUser_UserId",
                table: "ShoppingCart",
                column: "UserId",
                principalTable: "OnlineUser",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_OnlineUser_UserId",
                table: "ShoppingCart");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ShoppingCart",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_UserId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_CartId");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "OnlineUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_OnlineUser_CartId",
                table: "ShoppingCart",
                column: "CartId",
                principalTable: "OnlineUser",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
