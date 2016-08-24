namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullBankRollTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bankrolls", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bankrolls", "BankrollStartAmount", c => c.Double());
            AlterColumn("dbo.Bankrolls", "BankrollCurrentAmount", c => c.Double());
            AlterColumn("dbo.Bankrolls", "WinAmount", c => c.Double());
            AlterColumn("dbo.Bankrolls", "LoseAmount", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bankrolls", "LoseAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Bankrolls", "WinAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Bankrolls", "BankrollCurrentAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Bankrolls", "BankrollStartAmount", c => c.Double(nullable: false));
            DropColumn("dbo.Bankrolls", "Date");
        }
    }
}
