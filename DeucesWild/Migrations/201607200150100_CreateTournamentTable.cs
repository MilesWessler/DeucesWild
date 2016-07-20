namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTournamentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsCanceled = c.Boolean(nullable: false),
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                        Venue = c.String(nullable: false, maxLength: 255),
                        GenreId = c.Byte(nullable: false),
                        Category_Id = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.ArtistId)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tournaments", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Tournaments", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Tournaments", new[] { "Category_Id" });
            DropIndex("dbo.Tournaments", new[] { "ArtistId" });
            DropTable("dbo.Tournaments");
            DropTable("dbo.Categories");
        }
    }
}
