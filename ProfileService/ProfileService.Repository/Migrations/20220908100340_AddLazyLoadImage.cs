using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Repository.Migrations
{
    public partial class AddLazyLoadImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Image_ImageId",
                table: "Profiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Profiles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Image_ImageId",
                table: "Profiles",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Image_ImageId",
                table: "Profiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Image_ImageId",
                table: "Profiles",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
