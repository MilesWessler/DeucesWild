namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowings : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Attendances", name: "Tournament_Id", newName: "TournamentId");
            RenameIndex(table: "dbo.Attendances", name: "IX_Tournament_Id", newName: "IX_TournamentId");
            DropPrimaryKey("dbo.Attendances");
            AddPrimaryKey("dbo.Attendances", new[] { "TournamentId", "AttendeeId" });
            DropColumn("dbo.Attendances", "GigId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendances", "GigId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Attendances");
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendeeId" });
            RenameIndex(table: "dbo.Attendances", name: "IX_TournamentId", newName: "IX_Tournament_Id");
            RenameColumn(table: "dbo.Attendances", name: "TournamentId", newName: "Tournament_Id");
        }
    }
}
