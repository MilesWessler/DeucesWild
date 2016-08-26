namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendeeToTournament : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournaments", "AttendeeId", c => c.String());
            AddColumn("dbo.Tournaments", "Attendee_TournamentId", c => c.Int());
            AddColumn("dbo.Tournaments", "Attendee_AttendeeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tournaments", new[] { "Attendee_TournamentId", "Attendee_AttendeeId" });
            AddForeignKey("dbo.Tournaments", new[] { "Attendee_TournamentId", "Attendee_AttendeeId" }, "dbo.Attendances", new[] { "TournamentId", "AttendeeId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tournaments", new[] { "Attendee_TournamentId", "Attendee_AttendeeId" }, "dbo.Attendances");
            DropIndex("dbo.Tournaments", new[] { "Attendee_TournamentId", "Attendee_AttendeeId" });
            DropColumn("dbo.Tournaments", "Attendee_AttendeeId");
            DropColumn("dbo.Tournaments", "Attendee_TournamentId");
            DropColumn("dbo.Tournaments", "AttendeeId");
        }
    }
}
