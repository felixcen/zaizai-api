using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZaizaiDate.Database.Migrations
{
    public partial class Add_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Gender = table.Column<string>(maxLength: 40, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    KnownAs = table.Column<string>(nullable: true),
                    Introduction = table.Column<string>(maxLength: 2000, nullable: true),
                    LookingFor = table.Column<string>(maxLength: 2000, nullable: true),
                    Interests = table.Column<string>(maxLength: 2000, nullable: true),
                    City = table.Column<string>(maxLength: 200, nullable: true),
                    Country = table.Column<string>(maxLength: 200, nullable: true),
                    LastActive = table.Column<DateTimeOffset>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(maxLength: 2000, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
