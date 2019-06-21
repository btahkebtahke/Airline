namespace Airlines.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pilots", "TeamID", "dbo.RaceTeams");
            AddForeignKey("dbo.Pilots", "TeamID", "dbo.RaceTeams", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pilots", "TeamID", "dbo.RaceTeams");
            AddForeignKey("dbo.Pilots", "TeamID", "dbo.RaceTeams", "ID", cascadeDelete: true);
        }
    }
}
