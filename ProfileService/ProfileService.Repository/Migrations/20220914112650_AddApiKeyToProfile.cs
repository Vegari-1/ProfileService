using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Repository.Migrations
{
    public partial class AddApiKeyToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                schema: "profile",
                table: "Profiles",
                type: "text",
                nullable: true,
                defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                schema: "profile",
                table: "Profiles");
        }
    }
}
