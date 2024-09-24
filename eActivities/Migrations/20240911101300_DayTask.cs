using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eActivities.Migrations
{
    /// <inheritdoc />
    public partial class DayTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResponsibleManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsibleManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusTrackers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusTrackerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTrackers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleManagerId = table.Column<int>(type: "int", nullable: false),
                    StatusTrackerId = table.Column<int>(type: "int", nullable: false),
                    InitialTargetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevisedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayTask_ResponsibleManagers_ResponsibleManagerId",
                        column: x => x.ResponsibleManagerId,
                        principalTable: "ResponsibleManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayTask_StatusTrackers_StatusTrackerId",
                        column: x => x.StatusTrackerId,
                        principalTable: "StatusTrackers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayTask_ResponsibleManagerId",
                table: "DayTask",
                column: "ResponsibleManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_DayTask_StatusTrackerId",
                table: "DayTask",
                column: "StatusTrackerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayTask");

            migrationBuilder.DropTable(
                name: "ResponsibleManagers");

            migrationBuilder.DropTable(
                name: "StatusTrackers");
        }
    }
}
