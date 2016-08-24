namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBankrollToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Bankroll", c => c.Double(nullable: false));
            DropColumn("dbo.MyTournaments", "Bankroll");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyTournaments", "Bankroll", c => c.Double(nullable: false));
            DropColumn("dbo.AspNetUsers", "Bankroll");
        }
    }
}
