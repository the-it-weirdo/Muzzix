using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMusicStore.Data.Migrations
{
    public partial class AddsImageUrlToMusicAndAlbumTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Albums");
        }
    }
}
