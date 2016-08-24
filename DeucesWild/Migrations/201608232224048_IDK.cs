namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bankrolls", "BankrollStartAmount", c => c.Double(nullable: false));
            AddColumn("dbo.Bankrolls", "BankrollCurrentAmount", c => c.Double(nullable: false));
            DropColumn("dbo.Bankrolls", "BankrollAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bankrolls", "BankrollAmount", c => c.Double(nullable: false));
            DropColumn("dbo.Bankrolls", "BankrollCurrentAmount");
            DropColumn("dbo.Bankrolls", "BankrollStartAmount");
        }
    }
}
