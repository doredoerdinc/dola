namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AddressRouteMatrices", newName: "AddressRouteMatrix");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AddressRouteMatrix", newName: "AddressRouteMatrices");
        }
    }
}
