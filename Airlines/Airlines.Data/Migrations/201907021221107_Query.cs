namespace Airlines.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Query : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Queries", new[] { "RaceTeamID" });
            DropIndex("dbo.Queries", new[] { "RaceID" });
            AlterColumn("dbo.Queries", "RaceTeamID", c => c.Int());
            AlterColumn("dbo.Queries", "RaceID", c => c.Int());
            CreateIndex("dbo.Queries", "RaceTeamID");
            CreateIndex("dbo.Queries", "RaceID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Queries", new[] { "RaceID" });
            DropIndex("dbo.Queries", new[] { "RaceTeamID" });
            AlterColumn("dbo.Queries", "RaceID", c => c.Int(nullable: false));
            AlterColumn("dbo.Queries", "RaceTeamID", c => c.Int(nullable: false));
            CreateIndex("dbo.Queries", "RaceID");
            CreateIndex("dbo.Queries", "RaceTeamID");
        }
    }
}
