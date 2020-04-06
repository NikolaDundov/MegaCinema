using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaCinema.Data.Migrations
{
    public partial class DropMembership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MembershipCard_MembershipCardId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MembershipCard");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MembershipCardId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MembershipCardId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MembershipCardId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MembershipCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    CardType = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipCard", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MembershipCardId",
                table: "AspNetUsers",
                column: "MembershipCardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MembershipCard_MembershipCardId",
                table: "AspNetUsers",
                column: "MembershipCardId",
                principalTable: "MembershipCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
