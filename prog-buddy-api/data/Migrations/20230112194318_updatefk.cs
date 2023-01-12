using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class updatefk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PasswordHashes_HashedPasswordId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HashedPasswordId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordHashes",
                table: "PasswordHashes");

            migrationBuilder.DropColumn(
                name: "HashedPasswordId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserProjects",
                newName: "UserProjectId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PasswordHashes",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "UserProjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "UserProjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "PasswordHashId",
                table: "PasswordHashes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordHashes",
                table: "PasswordHashes",
                column: "PasswordHashId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordHashes_UserId",
                table: "PasswordHashes",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordHashes_Users_UserId",
                table: "PasswordHashes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordHashes_Users_UserId",
                table: "PasswordHashes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordHashes",
                table: "PasswordHashes");

            migrationBuilder.DropIndex(
                name: "IX_PasswordHashes_UserId",
                table: "PasswordHashes");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "UserProjects");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "UserProjects");

            migrationBuilder.DropColumn(
                name: "PasswordHashId",
                table: "PasswordHashes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserProjectId",
                table: "UserProjects",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PasswordHashes",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "HashedPasswordId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordHashes",
                table: "PasswordHashes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HashedPasswordId",
                table: "Users",
                column: "HashedPasswordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PasswordHashes_HashedPasswordId",
                table: "Users",
                column: "HashedPasswordId",
                principalTable: "PasswordHashes",
                principalColumn: "Id");
        }
    }
}
