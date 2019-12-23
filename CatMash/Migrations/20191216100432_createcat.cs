using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatMash.Migrations
{
    public partial class createcat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cat_Name = table.Column<string>(nullable: true),
                    Date_Cre = table.Column<DateTime>(nullable: false,defaultValueSql:"getdate()"),
                    Cat_picture = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cat");
        }
    }
}
