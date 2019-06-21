namespace Airlines.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Navigators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamID = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RaceTeams", t => t.TeamID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.RaceTeams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsAccepted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pilots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamID = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RaceTeams", t => t.TeamID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.Stuardesses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamID = c.Int(),
                        Form = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RaceTeams", t => t.TeamID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.Queries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsAccepted = c.Boolean(),
                        Race_ID = c.Int(),
                        RaceTeam_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Races", t => t.Race_ID)
                .ForeignKey("dbo.RaceTeams", t => t.RaceTeam_ID)
                .Index(t => t.Race_ID)
                .Index(t => t.RaceTeam_ID);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Departure = c.String(),
                        Destinaton = c.String(),
                        Date = c.DateTime(nullable: false),
                        IsDeparted = c.Boolean(),
                        RaceTeamID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RaceTeams", t => t.RaceTeamID)
                .Index(t => t.RaceTeamID);
            
            CreateTable(
                "dbo.RadioMen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamID = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RaceTeams", t => t.TeamID)
                .Index(t => t.TeamID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RadioMen", "TeamID", "dbo.RaceTeams");
            DropForeignKey("dbo.Queries", "RaceTeam_ID", "dbo.RaceTeams");
            DropForeignKey("dbo.Queries", "Race_ID", "dbo.Races");
            DropForeignKey("dbo.Races", "RaceTeamID", "dbo.RaceTeams");
            DropForeignKey("dbo.Navigators", "TeamID", "dbo.RaceTeams");
            DropForeignKey("dbo.Stuardesses", "TeamID", "dbo.RaceTeams");
            DropForeignKey("dbo.Pilots", "TeamID", "dbo.RaceTeams");
            DropIndex("dbo.RadioMen", new[] { "TeamID" });
            DropIndex("dbo.Races", new[] { "RaceTeamID" });
            DropIndex("dbo.Queries", new[] { "RaceTeam_ID" });
            DropIndex("dbo.Queries", new[] { "Race_ID" });
            DropIndex("dbo.Stuardesses", new[] { "TeamID" });
            DropIndex("dbo.Pilots", new[] { "TeamID" });
            DropIndex("dbo.Navigators", new[] { "TeamID" });
            DropTable("dbo.RadioMen");
            DropTable("dbo.Races");
            DropTable("dbo.Queries");
            DropTable("dbo.Stuardesses");
            DropTable("dbo.Pilots");
            DropTable("dbo.RaceTeams");
            DropTable("dbo.Navigators");
        }
    }
}
