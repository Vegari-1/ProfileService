using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Repository.Migrations
{
    public partial class AddProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Profiles",
                type: "uuid",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ImageId",
                table: "Profiles",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Image_ImageId",
                table: "Profiles",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Image_ImageId",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_ImageId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Profiles");
        }
    }
}
