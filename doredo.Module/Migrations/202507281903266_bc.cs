namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bc : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RoutePlanTransport", "PalletCapacity");
            DropColumn("dbo.RoutePlanTransport", "Width");
            DropColumn("dbo.RoutePlanTransport", "Length");
            DropColumn("dbo.RoutePlanTransport", "Height");
            DropColumn("dbo.RoutePlanTransport", "Volume");
            DropColumn("dbo.RoutePlanTransport", "PalleteCapacity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoutePlanTransport", "PalleteCapacity", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "Volume", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "Height", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "Length", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "Width", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "PalletCapacity", c => c.Double());
        }
    }
}
