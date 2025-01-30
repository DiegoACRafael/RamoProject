using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.EF.Migrations
{
    /// <inheritdoc />
    public partial class ProposalsUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Proposals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_UserId",
                table: "Proposals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_AspNetUsers_UserId",
                table: "Proposals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_AspNetUsers_UserId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_UserId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Proposals");
        }
    }
}
