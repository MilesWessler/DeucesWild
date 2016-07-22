namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EagerLoadTournament : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tournaments", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Tournaments", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Tournaments", name: "Category_Id", newName: "CategoryId");
            RenameColumn(table: "dbo.Tournaments", name: "ArtistId", newName: "UserId");
            RenameIndex(table: "dbo.Tournaments", name: "IX_ArtistId", newName: "IX_UserId");
            AddColumn("dbo.Tournaments", "Casino", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Tournaments", "CategoryId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Tournaments", "CategoryId");
            AddForeignKey("dbo.Tournaments", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            DropColumn("dbo.Tournaments", "Venue");
            DropColumn("dbo.Tournaments", "GenreId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tournaments", "GenreId", c => c.Byte(nullable: false));
            AddColumn("dbo.Tournaments", "Venue", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.Tournaments", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Tournaments", new[] { "CategoryId" });
            AlterColumn("dbo.Tournaments", "CategoryId", c => c.Byte());
            DropColumn("dbo.Tournaments", "Casino");
            RenameIndex(table: "dbo.Tournaments", name: "IX_UserId", newName: "IX_ArtistId");
            RenameColumn(table: "dbo.Tournaments", name: "UserId", newName: "ArtistId");
            RenameColumn(table: "dbo.Tournaments", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.Tournaments", "Category_Id");
            AddForeignKey("dbo.Tournaments", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
