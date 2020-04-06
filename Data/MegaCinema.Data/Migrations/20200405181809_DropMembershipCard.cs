using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaCinema.Data.Migrations
{
    public partial class DropMembershipCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MembershipCards_MembershipCardId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembershipCards",
                table: "MembershipCards");

            migrationBuilder.RenameTable(
                name: "MembershipCards",
                newName: "MembershipCard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembershipCard",
                table: "MembershipCard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MembershipCard_MembershipCardId",
                table: "AspNetUsers",
                column: "MembershipCardId",
                principalTable: "MembershipCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MembershipCard_MembershipCardId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MembershipCard",
                table: "MembershipCard");

            migrationBuilder.RenameTable(
                name: "MembershipCard",
                newName: "MembershipCards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembershipCards",
                table: "MembershipCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MembershipCards_MembershipCardId",
                table: "AspNetUsers",
                column: "MembershipCardId",
                principalTable: "MembershipCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
