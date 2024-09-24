using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eActivities.Migrations
{
    /// <inheritdoc />
    public partial class JMig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUser",
                table: "Activityitem",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUser",
                table: "Activityitem");
        }
    }
}
