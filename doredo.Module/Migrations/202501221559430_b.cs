namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressRouteMatrices", "DictanceMeters", c => c.Double());
            AddColumn("dbo.AddressRouteMatrices", "Duration", c => c.String());
            AddColumn("dbo.AddressRouteMatrices", "StaticDuration", c => c.String());
            AddColumn("dbo.AddressRouteMatrices", "EncodedPolyline", c => c.String(unicode: false));
            AddColumn("dbo.AddressRouteMatrices", "FromAddress_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.AddressRouteMatrices", "ToAddressCode_SysCode", c => c.String(maxLength: 100));
            CreateIndex("dbo.AddressRouteMatrices", "FromAddress_SysCode");
            CreateIndex("dbo.AddressRouteMatrices", "ToAddressCode_SysCode");
            AddForeignKey("dbo.AddressRouteMatrices", "FromAddress_SysCode", "dbo.Address", "SysCode");
            AddForeignKey("dbo.AddressRouteMatrices", "ToAddressCode_SysCode", "dbo.Address", "SysCode");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddressRouteMatrices", "ToAddressCode_SysCode", "dbo.Address");
            DropForeignKey("dbo.AddressRouteMatrices", "FromAddress_SysCode", "dbo.Address");
            DropIndex("dbo.AddressRouteMatrices", new[] { "ToAddressCode_SysCode" });
            DropIndex("dbo.AddressRouteMatrices", new[] { "FromAddress_SysCode" });
            DropColumn("dbo.AddressRouteMatrices", "ToAddressCode_SysCode");
            DropColumn("dbo.AddressRouteMatrices", "FromAddress_SysCode");
            DropColumn("dbo.AddressRouteMatrices", "EncodedPolyline");
            DropColumn("dbo.AddressRouteMatrices", "StaticDuration");
            DropColumn("dbo.AddressRouteMatrices", "Duration");
            DropColumn("dbo.AddressRouteMatrices", "DictanceMeters");
        }
    }
}
