using Microsoft.EntityFrameworkCore.Migrations;

namespace CatMash.Migrations
{
    public partial class updatecatscore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "Cat",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Cat");
        }
    }
}
