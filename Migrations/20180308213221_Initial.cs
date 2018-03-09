using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Triplann.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TripType",
                columns: table => new
                {
                    TripTypeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivityType = table.Column<string>(nullable: true),
                    TravelMethod = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    WeatherType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripType", x => x.TripTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItem",
                columns: table => new
                {
                    ChecklistItemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChecklistAction = table.Column<string>(nullable: false),
                    TripTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItem", x => x.ChecklistItemId);
                    table.ForeignKey(
                        name: "FK_ChecklistItem_TripType_TripTypeId",
                        column: x => x.TripTypeId,
                        principalTable: "TripType",
                        principalColumn: "TripTypeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationUserId = table.Column<int>(nullable: false),
                    Duration = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    TripTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trip_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Trip_TripType_TripTypeId",
                        column: x => x.TripTypeId,
                        principalTable: "TripType",
                        principalColumn: "TripTypeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_TripTypeId",
                table: "ChecklistItem",
                column: "TripTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_ApplicationUserId",
                table: "Trip",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TripTypeId",
                table: "Trip",
                column: "TripTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistItem");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "TripType");
        }
    }
}
