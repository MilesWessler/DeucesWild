namespace DeucesWild.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommenterId = c.String(maxLength: 128),
                        TournamentId = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CommenterId)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.CommenterId)
                .Index(t => t.TournamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Comments", "CommenterId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "TournamentId" });
            DropIndex("dbo.Comments", new[] { "CommenterId" });
            DropTable("dbo.Comments");
        }
    }
}
