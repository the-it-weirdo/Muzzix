using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMusicStore.Data.Migrations
{
    public partial class FixMusicAlbumFKPart1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Albums_AlbumId",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_AlbumId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Musics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Musics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musics_AlbumId",
                table: "Musics",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Albums_AlbumId",
                table: "Musics",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
