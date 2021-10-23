using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddedSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DjWebhook_DumpUsers_DjDumpUserID",
                table: "DjWebhook");

            migrationBuilder.DropIndex(
                name: "IX_DjWebhook_DjDumpUserID",
                table: "DjWebhook");

            migrationBuilder.DropColumn(
                name: "ShowNSFW",
                table: "DumpUsers");

            migrationBuilder.DropColumn(
                name: "DjDumpUserID",
                table: "DjWebhook");

            migrationBuilder.AddColumn<int>(
                name: "SettingsID",
                table: "DumpUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DjUserSettingsID",
                table: "DjWebhook",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DjUserSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowNSFW = table.Column<bool>(type: "bit", nullable: false),
                    Visibility = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DjUserSettings", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DumpUsers_SettingsID",
                table: "DumpUsers",
                column: "SettingsID");

            migrationBuilder.CreateIndex(
                name: "IX_DjWebhook_DjUserSettingsID",
                table: "DjWebhook",
                column: "DjUserSettingsID");

            migrationBuilder.AddForeignKey(
                name: "FK_DjWebhook_DjUserSettings_DjUserSettingsID",
                table: "DjWebhook",
                column: "DjUserSettingsID",
                principalTable: "DjUserSettings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DumpUsers_DjUserSettings_SettingsID",
                table: "DumpUsers",
                column: "SettingsID",
                principalTable: "DjUserSettings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DjWebhook_DjUserSettings_DjUserSettingsID",
                table: "DjWebhook");

            migrationBuilder.DropForeignKey(
                name: "FK_DumpUsers_DjUserSettings_SettingsID",
                table: "DumpUsers");

            migrationBuilder.DropTable(
                name: "DjUserSettings");

            migrationBuilder.DropIndex(
                name: "IX_DumpUsers_SettingsID",
                table: "DumpUsers");

            migrationBuilder.DropIndex(
                name: "IX_DjWebhook_DjUserSettingsID",
                table: "DjWebhook");

            migrationBuilder.DropColumn(
                name: "SettingsID",
                table: "DumpUsers");

            migrationBuilder.DropColumn(
                name: "DjUserSettingsID",
                table: "DjWebhook");

            migrationBuilder.AddColumn<bool>(
                name: "ShowNSFW",
                table: "DumpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DjDumpUserID",
                table: "DjWebhook",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DjWebhook_DjDumpUserID",
                table: "DjWebhook",
                column: "DjDumpUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_DjWebhook_DumpUsers_DjDumpUserID",
                table: "DjWebhook",
                column: "DjDumpUserID",
                principalTable: "DumpUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
