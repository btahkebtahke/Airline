namespace Airlines.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RaceName1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Races", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Races", "Name");
        }
    }
}
