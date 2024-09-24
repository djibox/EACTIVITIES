using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eActivities.Migrations
{
    /// <inheritdoc />
    public partial class Genre1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activityitem_Genre_GenreId",
                table: "Activityitem");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Activityitem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Activityitem_Genre_GenreId",
                table: "Activityitem",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activityitem_Genre_GenreId",
                table: "Activityitem");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Activityitem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activityitem_Genre_GenreId",
                table: "Activityitem",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
