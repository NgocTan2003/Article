using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Article.Data.Migrations
{
    public partial class udTBRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decripstion",
                table: "AspNetRoles",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "AspNetRoles",
                newName: "Decripstion");
        }
    }
}
