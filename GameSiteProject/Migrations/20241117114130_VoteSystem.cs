using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameSiteProject.Migrations
{
    /// <inheritdoc />
    public partial class VoteSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForumThreadId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Votes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForumThreadId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Votes");
        }
    }
}
