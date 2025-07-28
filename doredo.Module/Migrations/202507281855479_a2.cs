namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoutePlanTransport", "PalletCapacity", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "Width", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "Length", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "Height", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "Volume", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "PalleteCapacity", c => c.Double());
            AddColumn("dbo.RoutePlanTransport", "VehicleType_SysCode", c => c.String(maxLength: 100));
            CreateIndex("dbo.RoutePlanTransport", "VehicleType_SysCode");
            AddForeignKey("dbo.RoutePlanTransport", "VehicleType_SysCode", "dbo.VehicleType", "SysCode");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoutePlanTransport", "VehicleType_SysCode", "dbo.VehicleType");
            DropIndex("dbo.RoutePlanTransport", new[] { "VehicleType_SysCode" });
            DropColumn("dbo.RoutePlanTransport", "VehicleType_SysCode");
            DropColumn("dbo.RoutePlanTransport", "PalleteCapacity");
            DropColumn("dbo.RoutePlanTransport", "Volume");
            DropColumn("dbo.RoutePlanTransport", "Height");
            DropColumn("dbo.RoutePlanTransport", "Length");
            DropColumn("dbo.RoutePlanTransport", "Width");
            DropColumn("dbo.RoutePlanTransport", "PalletCapacity");
        }
    }
}
