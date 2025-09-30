using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    MaxLoginAttempts = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)3),
                    FailedLoginAttempts = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Balance = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    BalanceCcy = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false, defaultValue: "USD")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Expiry = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_tokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "BalanceCcy", "Login", "MaxLoginAttempts", "PasswordHash" },
                values: new object[,]
                {
                    { 1, 8m, "USD", "User 1", (short)5, "AQAAAAIAAYagAAAAEPbRfMBiYwm+9MONMvIoRP7A0hYRdfe+yTcttw9VBNRIoOvgy6ddy6duQEiZv2RF4Q==" },
                    { 2, 8m, "USD", "User 2", (short)5, "AQAAAAIAAYagAAAAECoIOO2jYvViIx1T/msKb7dD6jvMp6RoPr631yV8iQHORYjcNzlJSr3rhBuKB6Y0+A==" },
                    { 3, 8m, "USD", "User 3", (short)5, "AQAAAAIAAYagAAAAEBQqVyF/C2q5tJdQfFjQakwEGU0dmTXWLLE20ULWdAcr522uBdO/8epQ/HuHff/tJQ==" },
                    { 4, 8m, "USD", "User 4", (short)5, "AQAAAAIAAYagAAAAECq3e2ltZC0mPfLglCJjeUmq6a4CRzOrstJn1gf1NaOs9jHJSJZslgVaXkorLQJvNg==" },
                    { 5, 8m, "USD", "User 5", (short)5, "AQAAAAIAAYagAAAAEEDAW4Y3xVZY1hFPspXX/PF+gANWffip1fmv+07fO8Xf2Tll/XfAoYveGXd2wiaUWw==" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_tokens_UserId",
                table: "User_tokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "User_tokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
