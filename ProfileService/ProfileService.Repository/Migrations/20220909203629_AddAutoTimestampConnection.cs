using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Repository.Migrations
{
    public partial class AddAutoTimestampConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_Profiles_RequestedProfileId",
                table: "ConnectionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_Profiles_RequestingProfileId",
                table: "ConnectionRequests");

            migrationBuilder.DropIndex(
                name: "IX_ConnectionRequests_RequestedProfileId",
                table: "ConnectionRequests");

            migrationBuilder.DropIndex(
                name: "IX_ConnectionRequests_RequestingProfileId",
                table: "ConnectionRequests");

            migrationBuilder.RenameColumn(
                name: "RequestingProfileId",
                table: "ConnectionRequests",
                newName: "Profile2");

            migrationBuilder.RenameColumn(
                name: "RequestedProfileId",
                table: "ConnectionRequests",
                newName: "Profile1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Connections",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "current_timestamp at time zone 'utc'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "ConnectionRequests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "current_timestamp at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Connections");

            migrationBuilder.RenameColumn(
                name: "Profile2",
                table: "ConnectionRequests",
                newName: "RequestingProfileId");

            migrationBuilder.RenameColumn(
                name: "Profile1",
                table: "ConnectionRequests",
                newName: "RequestedProfileId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "ConnectionRequests",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequests_RequestedProfileId",
                table: "ConnectionRequests",
                column: "RequestedProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequests_RequestingProfileId",
                table: "ConnectionRequests",
                column: "RequestingProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_Profiles_RequestedProfileId",
                table: "ConnectionRequests",
                column: "RequestedProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_Profiles_RequestingProfileId",
                table: "ConnectionRequests",
                column: "RequestingProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
