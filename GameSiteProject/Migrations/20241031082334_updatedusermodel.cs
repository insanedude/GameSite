using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameSiteProject.Migrations
{
    /// <inheritdoc />
    public partial class updatedusermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tags_TagId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TagId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "TagUser",
                columns: table => new
                {
                    TagsTagId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagUser", x => new { x.TagsTagId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_TagUser_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagUser_UsersUserId",
                table: "TagUser",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagUser");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TagId",
                table: "Users",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tags_TagId",
                table: "Users",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId");
        }
    }
}
