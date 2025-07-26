namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutePlanTransport", "TotalStation", c => c.Int());
            AddColumn("dbo.RoutePlanTransport", "TotalDuration", c => c.Int());
            AddColumn("dbo.RoutePlanTransport", "TotalKm", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoutePlanTransport", "TotalKm");
            DropColumn("dbo.RoutePlanTransport", "TotalDuration");
            DropColumn("dbo.RoutePlanTransport", "TotalStation");
        }
    }
}
