using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations;

/// <inheritdoc />
public partial class _20261120205908_AddNewUserFields : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "City",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "FavouriteGenre",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "City",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "FavouriteGenre",
            table: "AspNetUsers");
    }
}
