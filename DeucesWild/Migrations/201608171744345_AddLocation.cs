namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        State = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tournaments", "EntryFee", c => c.Double(nullable: false));
            AddColumn("dbo.Tournaments", "PrizePool", c => c.Double(nullable: false));
            AddColumn("dbo.Tournaments", "NumberOfEntries", c => c.Int(nullable: false));
            AddColumn("dbo.Tournaments", "Image", c => c.Binary());
            AddColumn("dbo.Tournaments", "LocationId", c => c.Int(nullable: false));
            AddColumn("dbo.Tournaments", "PlayerOfTheYearPoints", c => c.Double(nullable: false));
            CreateIndex("dbo.Tournaments", "LocationId");
            AddForeignKey("dbo.Tournaments", "LocationId", "dbo.Locations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tournaments", "LocationId", "dbo.Locations");
            DropIndex("dbo.Tournaments", new[] { "LocationId" });
            DropColumn("dbo.Tournaments", "PlayerOfTheYearPoints");
            DropColumn("dbo.Tournaments", "LocationId");
            DropColumn("dbo.Tournaments", "Image");
            DropColumn("dbo.Tournaments", "NumberOfEntries");
            DropColumn("dbo.Tournaments", "PrizePool");
            DropColumn("dbo.Tournaments", "EntryFee");
            DropTable("dbo.Locations");
        }
    }
}
