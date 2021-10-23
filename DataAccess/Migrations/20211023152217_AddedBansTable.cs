using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddedBansTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DjImageTag_Images_DjImageID",
                table: "DjImageTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DjImageTag",
                table: "DjImageTag");

            migrationBuilder.RenameTable(
                name: "DjImageTag",
                newName: "ImageTags");

            migrationBuilder.RenameIndex(
                name: "IX_DjImageTag_DjImageID",
                table: "ImageTags",
                newName: "IX_ImageTags_DjImageID");

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "DumpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageTags",
                table: "ImageTags",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "UserBans",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", rowVersion: true, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserBans_DumpUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "DumpUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBans_UserID",
                table: "UserBans",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTags_Images_DjImageID",
                table: "ImageTags",
                column: "DjImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageTags_Images_DjImageID",
                table: "ImageTags");

            migrationBuilder.DropTable(
                name: "UserBans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageTags",
                table: "ImageTags");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "DumpUsers");

            migrationBuilder.RenameTable(
                name: "ImageTags",
                newName: "DjImageTag");

            migrationBuilder.RenameIndex(
                name: "IX_ImageTags_DjImageID",
                table: "DjImageTag",
                newName: "IX_DjImageTag_DjImageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DjImageTag",
                table: "DjImageTag",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DjImageTag_Images_DjImageID",
                table: "DjImageTag",
                column: "DjImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
