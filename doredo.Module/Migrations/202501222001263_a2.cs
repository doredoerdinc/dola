namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "DistanceMaptoAddressQuantity", c => c.Double());
            DropColumn("dbo.Address", "DistanceMaptoAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Address", "DistanceMaptoAddress", c => c.Double());
            DropColumn("dbo.Address", "DistanceMaptoAddressQuantity");
        }
    }
}
