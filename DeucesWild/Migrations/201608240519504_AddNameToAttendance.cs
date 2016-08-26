namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameToAttendance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendances", "Name");
        }
    }
}
