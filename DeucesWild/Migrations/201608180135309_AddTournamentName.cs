namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTournamentName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "TournamentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournaments", "TournamentName");
        }
    }
}
