namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMyTournaments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyTournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Buyin = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Notes = c.String(),
                        Place = c.Int(nullable: false),
                        AmountWon = c.Double(nullable: false),
                        Player_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Player_Id)
                .Index(t => t.Player_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyTournaments", "Player_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MyTournaments", new[] { "Player_Id" });
            DropTable("dbo.MyTournaments");
        }
    }
}
