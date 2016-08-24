namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlayerIdToMyTournament : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MyTournaments", name: "Player_Id", newName: "PlayerId");
            RenameIndex(table: "dbo.MyTournaments", name: "IX_Player_Id", newName: "IX_PlayerId");
            DropColumn("dbo.MyTournaments", "MemberId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyTournaments", "MemberId", c => c.String());
            RenameIndex(table: "dbo.MyTournaments", name: "IX_PlayerId", newName: "IX_Player_Id");
            RenameColumn(table: "dbo.MyTournaments", name: "PlayerId", newName: "Player_Id");
        }
    }
}
