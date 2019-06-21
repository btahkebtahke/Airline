namespace Airlines.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationProcess : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Navigators", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Pilots", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RadioMen", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Stuardesses", "FirstName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stuardesses", "FirstName", c => c.String());
            AlterColumn("dbo.RadioMen", "FirstName", c => c.String());
            AlterColumn("dbo.Pilots", "FirstName", c => c.String());
            AlterColumn("dbo.Navigators", "FirstName", c => c.String());
        }
    }
}
