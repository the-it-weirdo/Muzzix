using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineMusicStore.Data.Migrations
{
    public partial class PrePopulateGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Genres (Name) 
            VALUES ('Rock'), 
            ('Pop Music'),
            ('Classical Music'),
            ('Jazz Music')");
            // migrationBuilder.Sql(@"INSERT INTO Genres VALUES(2, 'Pop Music')");
            // migrationBuilder.Sql(@"INSERT INTO Genres VALUES(3, 'Classical Music')");
            // migrationBuilder.Sql(@"INSERT INTO Genres VALUES(4, 'Jazz Music')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Genres WHERE Id BETWEEN 1 AND 4");
        }
    }
}
