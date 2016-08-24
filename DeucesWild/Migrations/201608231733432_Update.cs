namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyTournaments", "MemberId", c => c.String());
            AddColumn("dbo.MyTournaments", "CategoryId", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyTournaments", "CategoryId");
            DropColumn("dbo.MyTournaments", "MemberId");
        }
    }
}
