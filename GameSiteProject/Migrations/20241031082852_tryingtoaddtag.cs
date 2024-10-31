using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameSiteProject.Migrations
{
    /// <inheritdoc />
    public partial class tryingtoaddtag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagUser_Tags_TagsTagId",
                table: "TagUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TagUser_Users_UsersUserId",
                table: "TagUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagUser",
                table: "TagUser");

            migrationBuilder.RenameTable(
                name: "TagUser",
                newName: "UserTags");

            migrationBuilder.RenameIndex(
                name: "IX_TagUser_UsersUserId",
                table: "UserTags",
                newName: "IX_UserTags_UsersUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTags",
                table: "UserTags",
                columns: new[] { "TagsTagId", "UsersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserTags_Tags_TagsTagId",
                table: "UserTags",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTags_Users_UsersUserId",
                table: "UserTags",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTags_Tags_TagsTagId",
                table: "UserTags");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTags_Users_UsersUserId",
                table: "UserTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTags",
                table: "UserTags");

            migrationBuilder.RenameTable(
                name: "UserTags",
                newName: "TagUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserTags_UsersUserId",
                table: "TagUser",
                newName: "IX_TagUser_UsersUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagUser",
                table: "TagUser",
                columns: new[] { "TagsTagId", "UsersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TagUser_Tags_TagsTagId",
                table: "TagUser",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagUser_Users_UsersUserId",
                table: "TagUser",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
