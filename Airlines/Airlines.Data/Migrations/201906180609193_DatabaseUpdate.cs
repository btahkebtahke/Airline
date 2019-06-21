namespace Airlines.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pilots", "TeamID", "dbo.RaceTeams");
            DropForeignKey("dbo.Navigators", "TeamID", "dbo.RaceTeams");
            DropForeignKey("dbo.RadioMen", "TeamID", "dbo.RaceTeams");
            DropIndex("dbo.Navigators", new[] { "TeamID" });
            DropIndex("dbo.Pilots", new[] { "TeamID" });
            DropIndex("dbo.Queries", new[] { "Race_ID" });
            DropIndex("dbo.Queries", new[] { "RaceTeam_ID" });
            DropIndex("dbo.RadioMen", new[] { "TeamID" });
            RenameColumn(table: "dbo.Queries", name: "Race_ID", newName: "RaceID");
            RenameColumn(table: "dbo.Queries", name: "RaceTeam_ID", newName: "RaceTeamID");
            AddColumn("dbo.RaceTeams", "PilotID", c => c.Int());
            AddColumn("dbo.RaceTeams", "NavigatorID", c => c.Int());
            AddColumn("dbo.RaceTeams", "RadioManID", c => c.Int());
            AlterColumn("dbo.Queries", "RaceID", c => c.Int(nullable: false));
            AlterColumn("dbo.Queries", "RaceTeamID", c => c.Int(nullable: false));
            CreateIndex("dbo.Queries", "RaceTeamID");
            CreateIndex("dbo.Queries", "RaceID");
            CreateIndex("dbo.RaceTeams", "PilotID");
            CreateIndex("dbo.RaceTeams", "NavigatorID");
            CreateIndex("dbo.RaceTeams", "RadioManID");
            AddForeignKey("dbo.RaceTeams", "NavigatorID", "dbo.Navigators", "ID");
            AddForeignKey("dbo.RaceTeams", "PilotID", "dbo.Pilots", "ID");
            AddForeignKey("dbo.RaceTeams", "RadioManID", "dbo.RadioMen", "ID");
            DropColumn("dbo.Navigators", "TeamID");
            DropColumn("dbo.Pilots", "TeamID");
            DropColumn("dbo.RadioMen", "TeamID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RadioMen", "TeamID", c => c.Int());
            AddColumn("dbo.Pilots", "TeamID", c => c.Int());
            AddColumn("dbo.Navigators", "TeamID", c => c.Int());
            DropForeignKey("dbo.RaceTeams", "RadioManID", "dbo.RadioMen");
            DropForeignKey("dbo.RaceTeams", "PilotID", "dbo.Pilots");
            DropForeignKey("dbo.RaceTeams", "NavigatorID", "dbo.Navigators");
            DropIndex("dbo.RaceTeams", new[] { "RadioManID" });
            DropIndex("dbo.RaceTeams", new[] { "NavigatorID" });
            DropIndex("dbo.RaceTeams", new[] { "PilotID" });
            DropIndex("dbo.Queries", new[] { "RaceID" });
            DropIndex("dbo.Queries", new[] { "RaceTeamID" });
            AlterColumn("dbo.Queries", "RaceTeamID", c => c.Int());
            AlterColumn("dbo.Queries", "RaceID", c => c.Int());
            DropColumn("dbo.RaceTeams", "RadioManID");
            DropColumn("dbo.RaceTeams", "NavigatorID");
            DropColumn("dbo.RaceTeams", "PilotID");
            RenameColumn(table: "dbo.Queries", name: "RaceTeamID", newName: "RaceTeam_ID");
            RenameColumn(table: "dbo.Queries", name: "RaceID", newName: "Race_ID");
            CreateIndex("dbo.RadioMen", "TeamID");
            CreateIndex("dbo.Queries", "RaceTeam_ID");
            CreateIndex("dbo.Queries", "Race_ID");
            CreateIndex("dbo.Pilots", "TeamID");
            CreateIndex("dbo.Navigators", "TeamID");
            AddForeignKey("dbo.RadioMen", "TeamID", "dbo.RaceTeams", "ID");
            AddForeignKey("dbo.Navigators", "TeamID", "dbo.RaceTeams", "ID");
            AddForeignKey("dbo.Pilots", "TeamID", "dbo.RaceTeams", "ID");
        }
    }
}
