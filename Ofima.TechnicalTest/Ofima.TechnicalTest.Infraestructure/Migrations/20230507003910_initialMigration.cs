using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ofima.TechnicalTest.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Names = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovePlayerOneId = table.Column<int>(type: "int", nullable: false),
                    MovePlayerTwoId = table.Column<int>(type: "int", nullable: false),
                    Winner = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Tie = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRules_Moves_MovePlayerOneId",
                        column: x => x.MovePlayerOneId,
                        principalTable: "Moves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameRules_Moves_MovePlayerTwoId",
                        column: x => x.MovePlayerTwoId,
                        principalTable: "Moves",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatePlayed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerOneId = table.Column<int>(type: "int", nullable: false),
                    PlayerTwoId = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Players_PlayerOneId",
                        column: x => x.PlayerOneId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Players_PlayerTwoId",
                        column: x => x.PlayerTwoId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Players_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameMoves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    MoveId = table.Column<int>(type: "int", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameMoves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameMoves_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameMoves_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Moves",
                columns: new[] { "Id", "MoveName" },
                values: new object[,]
                {
                    { 1, "Piedra" },
                    { 2, "Papel" },
                    { 3, "Tijera" }
                });

            migrationBuilder.InsertData(
                table: "GameRules",
                columns: new[] { "Id", "MovePlayerOneId", "MovePlayerTwoId", "Tie", "Winner" },
                values: new object[,]
                {
                    { 1, 1, 3, false, "PlayerOne" },
                    { 2, 1, 2, false, "PlayerOne" },
                    { 3, 2, 1, false, "PlayerOne" },
                    { 4, 2, 3, false, "PlayerTwo" },
                    { 5, 3, 1, false, "PlayerTwo" },
                    { 6, 3, 2, false, "PlayerTwo" },
                    { 7, 1, 1, true, "Tie" },
                    { 8, 2, 2, true, "Tie" },
                    { 9, 3, 3, true, "Tie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameMoves_GameId",
                table: "GameMoves",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameMoves_MoveId",
                table: "GameMoves",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_GameMoves_PlayerId",
                table: "GameMoves",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRules_MovePlayerOneId",
                table: "GameRules",
                column: "MovePlayerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRules_MovePlayerTwoId",
                table: "GameRules",
                column: "MovePlayerTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerOneId",
                table: "Games",
                column: "PlayerOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerTwoId",
                table: "Games",
                column: "PlayerTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerId",
                table: "Games",
                column: "WinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameMoves");

            migrationBuilder.DropTable(
                name: "GameRules");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
