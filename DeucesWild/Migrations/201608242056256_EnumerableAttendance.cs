namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnumerableAttendance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tournaments", new[] { "Attendee_TournamentId", "Attendee_AttendeeId" }, "dbo.Attendances");
            DropIndex("dbo.Tournaments", new[] { "Attendee_TournamentId", "Attendee_AttendeeId" });
            DropColumn("dbo.Tournaments", "Attendee_TournamentId");
            DropColumn("dbo.Tournaments", "Attendee_AttendeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tournaments", "Attendee_AttendeeId", c => c.String(maxLength: 128));
            AddColumn("dbo.Tournaments", "Attendee_TournamentId", c => c.Int());
            CreateIndex("dbo.Tournaments", new[] { "Attendee_TournamentId", "Attendee_AttendeeId" });
            AddForeignKey("dbo.Tournaments", new[] { "Attendee_TournamentId", "Attendee_AttendeeId" }, "dbo.Attendances", new[] { "TournamentId", "AttendeeId" });
        }
    }
}
