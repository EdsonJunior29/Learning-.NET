using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEmDotNet.Migrations
{
    /// <inheritdoc />
    public partial class RequiredCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_userId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Categories",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_userId",
                table: "Categories",
                newName: "IX_Categories_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Categories",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                newName: "IX_Categories_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_userId",
                table: "Categories",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
