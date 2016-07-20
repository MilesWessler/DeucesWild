namespace DeucesWild.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Id, Name) Values (1, 'Texas Hold Em: No-Limit')");
            Sql("INSERT INTO Categories (Id, Name) Values (2, 'Texas Hold Em: Limit')");
            Sql("INSERT INTO Categories (Id, Name) Values (3, 'Omaha: Hi-Low')");
            Sql("INSERT INTO Categories (Id, Name) Values (4, 'Omaha: Pot-Limit')");
            Sql("INSERT INTO Categories (Id, Name) Values (5, 'Razz')");
        }

        public override void Down()
        {
        }
    }
}
