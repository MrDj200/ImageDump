using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VRCUsers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsernameID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DjVRCImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VRCUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DjVRCUsername",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DjVRCUsername", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DjVRCUsername_VRCUsers_ID",
                        column: x => x.ID,
                        principalTable: "VRCUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VRCWorlds",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AuthorID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VRCWorlds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VRCWorlds_VRCUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "VRCUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DumpUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VRCIdentityID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DiscordID = table.Column<long>(type: "bigint", nullable: false),
                    DjDumpGroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DjImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DumpUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DumpUsers_VRCUsers_VRCIdentityID",
                        column: x => x.VRCIdentityID,
                        principalTable: "VRCUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DjWebhook",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DjDumpUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DjWebhook", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DjWebhook_DumpUsers_DjDumpUserID",
                        column: x => x.DjDumpUserID,
                        principalTable: "DumpUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UploaderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorldID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AuthorID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Images_DumpUsers_UploaderID",
                        column: x => x.UploaderID,
                        principalTable: "DumpUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_VRCUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "VRCUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_VRCWorlds_WorldID",
                        column: x => x.WorldID,
                        principalTable: "VRCWorlds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DumpGroups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DjImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DumpGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DumpGroups_Images_DjImageID",
                        column: x => x.DjImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DjWebhook_DjDumpUserID",
                table: "DjWebhook",
                column: "DjDumpUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DumpGroups_DjImageID",
                table: "DumpGroups",
                column: "DjImageID");

            migrationBuilder.CreateIndex(
                name: "IX_DumpUsers_DjDumpGroupID",
                table: "DumpUsers",
                column: "DjDumpGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_DumpUsers_DjImageID",
                table: "DumpUsers",
                column: "DjImageID");

            migrationBuilder.CreateIndex(
                name: "IX_DumpUsers_VRCIdentityID",
                table: "DumpUsers",
                column: "VRCIdentityID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorID",
                table: "Images",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UploaderID",
                table: "Images",
                column: "UploaderID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_WorldID",
                table: "Images",
                column: "WorldID");

            migrationBuilder.CreateIndex(
                name: "IX_VRCUsers_DjVRCImageID",
                table: "VRCUsers",
                column: "DjVRCImageID");

            migrationBuilder.CreateIndex(
                name: "IX_VRCUsers_UsernameID",
                table: "VRCUsers",
                column: "UsernameID");

            migrationBuilder.CreateIndex(
                name: "IX_VRCWorlds_AuthorID",
                table: "VRCWorlds",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_VRCUsers_DjVRCUsername_UsernameID",
                table: "VRCUsers",
                column: "UsernameID",
                principalTable: "DjVRCUsername",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VRCUsers_Images_DjVRCImageID",
                table: "VRCUsers",
                column: "DjVRCImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DumpUsers_DumpGroups_DjDumpGroupID",
                table: "DumpUsers",
                column: "DjDumpGroupID",
                principalTable: "DumpGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DumpUsers_Images_DjImageID",
                table: "DumpUsers",
                column: "DjImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DjVRCUsername_VRCUsers_ID",
                table: "DjVRCUsername");

            migrationBuilder.DropForeignKey(
                name: "FK_DumpUsers_VRCUsers_VRCIdentityID",
                table: "DumpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_VRCUsers_AuthorID",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_VRCWorlds_VRCUsers_AuthorID",
                table: "VRCWorlds");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_DumpUsers_UploaderID",
                table: "Images");

            migrationBuilder.DropTable(
                name: "DjWebhook");

            migrationBuilder.DropTable(
                name: "VRCUsers");

            migrationBuilder.DropTable(
                name: "DjVRCUsername");

            migrationBuilder.DropTable(
                name: "DumpUsers");

            migrationBuilder.DropTable(
                name: "DumpGroups");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "VRCWorlds");
        }
    }
}
