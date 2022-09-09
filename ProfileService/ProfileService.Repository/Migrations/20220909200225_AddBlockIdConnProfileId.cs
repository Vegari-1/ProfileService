using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Repository.Migrations
{
    public partial class AddBlockIdConnProfileId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Profiles_Profile1Id",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Profiles_Profile2Id",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_Profile1Id",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_Profile2Id",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blocks",
                table: "Blocks");

            migrationBuilder.RenameColumn(
                name: "Profile2Id",
                table: "Connections",
                newName: "Profile2");

            migrationBuilder.RenameColumn(
                name: "Profile1Id",
                table: "Connections",
                newName: "Profile1");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Blocks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blocks",
                table: "Blocks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockerId",
                table: "Blocks",
                column: "BlockerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Blocks",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_BlockerId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Blocks");

            migrationBuilder.RenameColumn(
                name: "Profile2",
                table: "Connections",
                newName: "Profile2Id");

            migrationBuilder.RenameColumn(
                name: "Profile1",
                table: "Connections",
                newName: "Profile1Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blocks",
                table: "Blocks",
                columns: new[] { "BlockerId", "BlockedId" });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_Profile1Id",
                table: "Connections",
                column: "Profile1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_Profile2Id",
                table: "Connections",
                column: "Profile2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Profiles_Profile1Id",
                table: "Connections",
                column: "Profile1Id",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Profiles_Profile2Id",
                table: "Connections",
                column: "Profile2Id",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
