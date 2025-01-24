namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressRouteMatrices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        CreatedBy_ID = c.Int(),
                        UpdateBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.UpdateBy_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddressRouteMatrices", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.AddressRouteMatrices", "CreatedBy_ID", "dbo.User");
            DropIndex("dbo.AddressRouteMatrices", new[] { "UpdateBy_ID" });
            DropIndex("dbo.AddressRouteMatrices", new[] { "CreatedBy_ID" });
            DropTable("dbo.AddressRouteMatrices");
        }
    }
}
