using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_token_users_UserId",
                table: "user_token");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_token",
                table: "user_token");

            migrationBuilder.RenameTable(
                name: "user_token",
                newName: "user_tokens");

            migrationBuilder.RenameIndex(
                name: "IX_user_token_UserId",
                table: "user_tokens",
                newName: "IX_user_tokens_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RevokedAt",
                table: "user_tokens",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expiry",
                table: "user_tokens",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_tokens_users_UserId",
                table: "user_tokens",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_tokens_users_UserId",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens");

            migrationBuilder.RenameTable(
                name: "user_tokens",
                newName: "user_token");

            migrationBuilder.RenameIndex(
                name: "IX_user_tokens_UserId",
                table: "user_token",
                newName: "IX_user_token_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RevokedAt",
                table: "user_token",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expiry",
                table: "user_token",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_token",
                table: "user_token",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_token_users_UserId",
                table: "user_token",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
