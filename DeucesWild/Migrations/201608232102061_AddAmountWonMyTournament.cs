namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAmountWonMyTournament : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyTournaments", "AmountLost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyTournaments", "AmountLost");
        }
    }
}
