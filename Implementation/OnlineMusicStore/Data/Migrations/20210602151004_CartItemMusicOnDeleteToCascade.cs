using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMusicStore.Data.Migrations
{
    public partial class CartItemMusicOnDeleteToCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Musics_MusicId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_MusicId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "MusicId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_MusicId",
                table: "CartItems",
                column: "MusicId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Musics_MusicId",
                table: "CartItems",
                column: "MusicId",
                principalTable: "Musics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Musics_MusicId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_MusicId",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "MusicId",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_MusicId",
                table: "CartItems",
                column: "MusicId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Musics_MusicId",
                table: "CartItems",
                column: "MusicId",
                principalTable: "Musics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
