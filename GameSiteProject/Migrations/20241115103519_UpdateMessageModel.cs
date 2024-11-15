using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameSiteProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMessageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForumThreadId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ForumThreadId",
                table: "Messages",
                column: "ForumThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ForumThreads_ForumThreadId",
                table: "Messages",
                column: "ForumThreadId",
                principalTable: "ForumThreads",
                principalColumn: "ForumThreadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ForumThreads_ForumThreadId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ForumThreadId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ForumThreadId",
                table: "Messages");
        }
    }
}
