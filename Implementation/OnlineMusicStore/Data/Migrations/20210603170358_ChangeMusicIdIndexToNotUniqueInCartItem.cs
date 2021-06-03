using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMusicStore.Data.Migrations
{
    public partial class ChangeMusicIdIndexToNotUniqueInCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartItems_MusicId",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_MusicId",
                table: "CartItems",
                column: "MusicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartItems_MusicId",
                table: "CartItems");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_MusicId",
                table: "CartItems",
                column: "MusicId",
                unique: true);
        }
    }
}
