using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations;

/// <inheritdoc />
public partial class _20261118221140_AddFilmSessionTable2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_FilmSession_Films_FilmId",
            table: "FilmSession");

        migrationBuilder.DropPrimaryKey(
            name: "PK_FilmSession",
            table: "FilmSession");

        migrationBuilder.RenameTable(
            name: "FilmSession",
            newName: "FilmSessions");

        migrationBuilder.RenameIndex(
            name: "IX_FilmSession_FilmId",
            table: "FilmSessions",
            newName: "IX_FilmSessions_FilmId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_FilmSessions",
            table: "FilmSessions",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_FilmSessions_Films_FilmId",
            table: "FilmSessions",
            column: "FilmId",
            principalTable: "Films",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_FilmSessions_Films_FilmId",
            table: "FilmSessions");

        migrationBuilder.DropPrimaryKey(
            name: "PK_FilmSessions",
            table: "FilmSessions");

        migrationBuilder.RenameTable(
            name: "FilmSessions",
            newName: "FilmSession");

        migrationBuilder.RenameIndex(
            name: "IX_FilmSessions_FilmId",
            table: "FilmSession",
            newName: "IX_FilmSession_FilmId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_FilmSession",
            table: "FilmSession",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_FilmSession_Films_FilmId",
            table: "FilmSession",
            column: "FilmId",
            principalTable: "Films",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
