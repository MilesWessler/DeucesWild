namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBankrollClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bankrolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.String(maxLength: 128),
                        BankrollAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PlayerId)
                .Index(t => t.PlayerId);
            
            AddColumn("dbo.MyTournaments", "Bankroll_Id", c => c.Int());
            CreateIndex("dbo.MyTournaments", "Bankroll_Id");
            AddForeignKey("dbo.MyTournaments", "Bankroll_Id", "dbo.Bankrolls", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyTournaments", "Bankroll_Id", "dbo.Bankrolls");
            DropForeignKey("dbo.Bankrolls", "PlayerId", "dbo.AspNetUsers");
            DropIndex("dbo.Bankrolls", new[] { "PlayerId" });
            DropIndex("dbo.MyTournaments", new[] { "Bankroll_Id" });
            DropColumn("dbo.MyTournaments", "Bankroll_Id");
            DropTable("dbo.Bankrolls");
        }
    }
}
