using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Repository.Migrations
{
    public partial class AddSchemaNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Profiles_ProfileId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Image_ImageId",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.EnsureSchema(
                name: "profile");

            migrationBuilder.RenameTable(
                name: "WorkExperiences",
                newName: "WorkExperiences",
                newSchema: "profile");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Skills",
                newSchema: "profile");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "Profiles",
                newSchema: "profile");

            migrationBuilder.RenameTable(
                name: "Connections",
                newName: "Connections",
                newSchema: "profile");

            migrationBuilder.RenameTable(
                name: "ConnectionRequests",
                newName: "ConnectionRequests",
                newSchema: "profile");

            migrationBuilder.RenameTable(
                name: "Blocks",
                newName: "Blocks",
                newSchema: "profile");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images",
                newSchema: "profile");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations",
                newSchema: "profile");

            migrationBuilder.RenameIndex(
                name: "IX_Education_ProfileId",
                schema: "profile",
                table: "Educations",
                newName: "IX_Educations_ProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                schema: "profile",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                schema: "profile",
                table: "Educations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Profiles_ProfileId",
                schema: "profile",
                table: "Educations",
                column: "ProfileId",
                principalSchema: "profile",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Images_ImageId",
                schema: "profile",
                table: "Profiles",
                column: "ImageId",
                principalSchema: "profile",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Profiles_ProfileId",
                schema: "profile",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Images_ImageId",
                schema: "profile",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                schema: "profile",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                schema: "profile",
                table: "Educations");

            migrationBuilder.RenameTable(
                name: "WorkExperiences",
                schema: "profile",
                newName: "WorkExperiences");

            migrationBuilder.RenameTable(
                name: "Skills",
                schema: "profile",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "Profiles",
                schema: "profile",
                newName: "Profiles");

            migrationBuilder.RenameTable(
                name: "Connections",
                schema: "profile",
                newName: "Connections");

            migrationBuilder.RenameTable(
                name: "ConnectionRequests",
                schema: "profile",
                newName: "ConnectionRequests");

            migrationBuilder.RenameTable(
                name: "Blocks",
                schema: "profile",
                newName: "Blocks");

            migrationBuilder.RenameTable(
                name: "Images",
                schema: "profile",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "Educations",
                schema: "profile",
                newName: "Education");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_ProfileId",
                table: "Education",
                newName: "IX_Education_ProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Profiles_ProfileId",
                table: "Education",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Image_ImageId",
                table: "Profiles",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id");
        }
    }
}
