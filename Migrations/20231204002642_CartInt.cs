using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicShop.Migrations
{
    /// <inheritdoc />
    public partial class CartInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "ShoppingCart",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_SongId",
                table: "ShoppingCart",
                column: "SongId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Song_SongId",
                table: "ShoppingCart",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "SongId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineUser_ShoppingCart_CartId",
                table: "OnlineUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Song_SongId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_SongId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_OnlineUser_CartId",
                table: "OnlineUser");

            migrationBuilder.AlterColumn<string>(
                name: "CartId",
                table: "ShoppingCart",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
