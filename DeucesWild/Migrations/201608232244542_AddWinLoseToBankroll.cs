namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWinLoseToBankroll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bankrolls", "WinAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Bankrolls", "LoseAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bankrolls", "LoseAmount");
            DropColumn("dbo.Bankrolls", "WinAmount");
        }
    }
}
