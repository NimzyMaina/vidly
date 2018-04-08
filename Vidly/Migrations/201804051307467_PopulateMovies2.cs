namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovies2 : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, InStock, GenreId) VALUES ('Hangover','2/20/2012','8/13/2016',5,2)");
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, InStock, GenreId) VALUES ('Die Hard','5/20/2015','8/13/2016',5,1)");
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, InStock, GenreId) VALUES ('Terminator','7/20/2016','8/13/2016',5,4)");
            Sql("INSERT INTO Movies (Name, ReleaseDate, DateAdded, InStock, GenreId) VALUES ('Toy Story 2','5/20/2017','8/13/2017',5,3)");
        }
        
        public override void Down()
        {
        }
    }
}
