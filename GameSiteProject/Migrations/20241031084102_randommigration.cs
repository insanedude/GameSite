using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameSiteProject.Migrations
{
    /// <inheritdoc />
    public partial class randommigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumThreads_Tags_TagId",
                table: "ForumThreads");

            migrationBuilder.DropTable(
                name: "UserTags");

            migrationBuilder.DropIndex(
                name: "IX_ForumThreads_TagId",
                table: "ForumThreads");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "ForumThreads");

            migrationBuilder.AddColumn<int>(
                name: "ForumThreadId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ForumThreadId",
                table: "Tags",
                column: "ForumThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_GameId",
                table: "Tags",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_ForumThreads_ForumThreadId",
                table: "Tags",
                column: "ForumThreadId",
                principalTable: "ForumThreads",
                principalColumn: "ForumThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Games_GameId",
                table: "Tags",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_ForumThreads_ForumThreadId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Games_GameId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ForumThreadId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_GameId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ForumThreadId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "ForumThreads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserTags",
                columns: table => new
                {
                    TagsTagId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTags", x => new { x.TagsTagId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_UserTags_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTags_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumThreads_TagId",
                table: "ForumThreads",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTags_UsersUserId",
                table: "UserTags",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumThreads_Tags_TagId",
                table: "ForumThreads",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId");
        }
    }
}
