using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations;

/// <inheritdoc />
public partial class _20261118084739_AddFilmSessions : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "FilmSession",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                HallName = table.Column<string>(type: "TEXT", nullable: false),
                Price = table.Column<decimal>(type: "TEXT", nullable: false),
                AvailableSeats = table.Column<int>(type: "INTEGER", nullable: false),
                StartsAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                FilmId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FilmSession", x => x.Id);
                table.ForeignKey(
                    name: "FK_FilmSession_Films_FilmId",
                    column: x => x.FilmId,
                    principalTable: "Films",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_FilmSession_FilmId",
            table: "FilmSession",
            column: "FilmId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "FilmSession");
    }
}
