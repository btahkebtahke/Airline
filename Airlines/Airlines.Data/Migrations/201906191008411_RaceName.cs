namespace Airlines.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RaceName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Races", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Races", "Name", c => c.String());
        }
    }
}
