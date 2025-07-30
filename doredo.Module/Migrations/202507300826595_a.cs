namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "CalculateRouteRequest", c => c.Int());
            AddColumn("dbo.Address", "CalculateRouteAddress", c => c.Int());
            AddColumn("dbo.Address", "UnCalculateRouteAddress", c => c.Int());
            DropColumn("dbo.Address", "DistanceMaptoAddressQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Address", "DistanceMaptoAddressQuantity", c => c.Double());
            DropColumn("dbo.Address", "UnCalculateRouteAddress");
            DropColumn("dbo.Address", "CalculateRouteAddress");
            DropColumn("dbo.Address", "CalculateRouteRequest");
        }
    }
}
