using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Repository.Migrations
{
    public partial class RemoveProfileId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Profiles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Connections",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "current_timestamp at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "ConnectionRequests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "current_timestamp at time zone 'utc'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "getutcdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Profiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Connections",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "current_timestamp at time zone 'utc'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "ConnectionRequests",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "current_timestamp at time zone 'utc'");
        }
    }
}
