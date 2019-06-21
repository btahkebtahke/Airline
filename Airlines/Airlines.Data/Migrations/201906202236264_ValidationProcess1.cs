namespace Airlines.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationProcess1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Navigators", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Pilots", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Races", "Departure", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Races", "Destinaton", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RadioMen", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Stuardesses", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stuardesses", "LastName", c => c.String());
            AlterColumn("dbo.RadioMen", "LastName", c => c.String());
            AlterColumn("dbo.Races", "Destinaton", c => c.String());
            AlterColumn("dbo.Races", "Departure", c => c.String());
            AlterColumn("dbo.Pilots", "LastName", c => c.String());
            AlterColumn("dbo.Navigators", "LastName", c => c.String());
        }
    }
}
