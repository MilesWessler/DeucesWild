namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedNullTypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bankrolls", "BankrollStartAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Bankrolls", "BankrollCurrentAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Bankrolls", "WinAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Bankrolls", "LoseAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bankrolls", "LoseAmount", c => c.Double());
            AlterColumn("dbo.Bankrolls", "WinAmount", c => c.Double());
            AlterColumn("dbo.Bankrolls", "BankrollCurrentAmount", c => c.Double());
            AlterColumn("dbo.Bankrolls", "BankrollStartAmount", c => c.Double());
        }
    }
}
