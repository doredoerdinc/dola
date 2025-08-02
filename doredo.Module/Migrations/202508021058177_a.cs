namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkingTime", "RouteRestTime", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkingTime", "RouteRestTime");
        }
    }
}
