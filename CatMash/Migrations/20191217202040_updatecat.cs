using Microsoft.EntityFrameworkCore.Migrations;

namespace CatMash.Migrations
{
    public partial class updatecat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture_Path",
                table: "Cat",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture_Path",
                table: "Cat");
        }
    }
}
