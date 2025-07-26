namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WorkingTimes", newName: "WorkingTime");
            DropForeignKey("dbo.RoutePlan", "CreatedBy_ID", "dbo.User");
            DropForeignKey("dbo.RoutePlan", "StartAddress_SysCode", "dbo.Address");
            DropForeignKey("dbo.RoutePlan", "State_SysCode", "dbo.State");
            DropForeignKey("dbo.Trip", "RoutePlan_SysCode", "dbo.RoutePlan");
            DropForeignKey("dbo.RoutePlan", "UpdateBy_ID", "dbo.User");
            DropIndex("dbo.Trip", new[] { "RoutePlan_SysCode" });
            DropIndex("dbo.RoutePlan", new[] { "CreatedBy_ID" });
            DropIndex("dbo.RoutePlan", new[] { "StartAddress_SysCode" });
            DropIndex("dbo.RoutePlan", new[] { "State_SysCode" });
            DropIndex("dbo.RoutePlan", new[] { "UpdateBy_ID" });
            CreateTable(
                "dbo.RoutePlanTransport",
                c => new
                    {
                        SysCode = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        CreatedBy_ID = c.Int(),
                        Owner_SysCode = c.String(maxLength: 100),
                        StartAddress_SysCode = c.String(maxLength: 100),
                        State_SysCode = c.String(maxLength: 100),
                        UpdateBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.SysCode)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.Owner", t => t.Owner_SysCode)
                .ForeignKey("dbo.Address", t => t.StartAddress_SysCode)
                .ForeignKey("dbo.State", t => t.State_SysCode)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.Owner_SysCode)
                .Index(t => t.StartAddress_SysCode)
                .Index(t => t.State_SysCode)
                .Index(t => t.UpdateBy_ID);
            
            AddColumn("dbo.WorkingTime", "RoutePlanTransport_SysCode", c => c.String(maxLength: 100));
            CreateIndex("dbo.WorkingTime", "RoutePlanTransport_SysCode");
            AddForeignKey("dbo.WorkingTime", "RoutePlanTransport_SysCode", "dbo.RoutePlanTransport", "SysCode");
            DropColumn("dbo.Trip", "RoutePlan_SysCode");
            DropTable("dbo.RoutePlan");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoutePlan",
                c => new
                    {
                        SysCode = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        PlannedStartTime = c.DateTime(),
                        PlannedFinishedTime = c.DateTime(),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        CreatedBy_ID = c.Int(),
                        StartAddress_SysCode = c.String(maxLength: 100),
                        State_SysCode = c.String(maxLength: 100),
                        UpdateBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.SysCode);
            
            AddColumn("dbo.Trip", "RoutePlan_SysCode", c => c.String(maxLength: 100));
            DropForeignKey("dbo.WorkingTime", "RoutePlanTransport_SysCode", "dbo.RoutePlanTransport");
            DropForeignKey("dbo.RoutePlanTransport", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.RoutePlanTransport", "State_SysCode", "dbo.State");
            DropForeignKey("dbo.RoutePlanTransport", "StartAddress_SysCode", "dbo.Address");
            DropForeignKey("dbo.RoutePlanTransport", "Owner_SysCode", "dbo.Owner");
            DropForeignKey("dbo.RoutePlanTransport", "CreatedBy_ID", "dbo.User");
            DropIndex("dbo.RoutePlanTransport", new[] { "UpdateBy_ID" });
            DropIndex("dbo.RoutePlanTransport", new[] { "State_SysCode" });
            DropIndex("dbo.RoutePlanTransport", new[] { "StartAddress_SysCode" });
            DropIndex("dbo.RoutePlanTransport", new[] { "Owner_SysCode" });
            DropIndex("dbo.RoutePlanTransport", new[] { "CreatedBy_ID" });
            DropIndex("dbo.WorkingTime", new[] { "RoutePlanTransport_SysCode" });
            DropColumn("dbo.WorkingTime", "RoutePlanTransport_SysCode");
            DropTable("dbo.RoutePlanTransport");
            CreateIndex("dbo.RoutePlan", "UpdateBy_ID");
            CreateIndex("dbo.RoutePlan", "State_SysCode");
            CreateIndex("dbo.RoutePlan", "StartAddress_SysCode");
            CreateIndex("dbo.RoutePlan", "CreatedBy_ID");
            CreateIndex("dbo.Trip", "RoutePlan_SysCode");
            AddForeignKey("dbo.RoutePlan", "UpdateBy_ID", "dbo.User", "ID");
            AddForeignKey("dbo.Trip", "RoutePlan_SysCode", "dbo.RoutePlan", "SysCode");
            AddForeignKey("dbo.RoutePlan", "State_SysCode", "dbo.State", "SysCode");
            AddForeignKey("dbo.RoutePlan", "StartAddress_SysCode", "dbo.Address", "SysCode");
            AddForeignKey("dbo.RoutePlan", "CreatedBy_ID", "dbo.User", "ID");
            RenameTable(name: "dbo.WorkingTime", newName: "WorkingTimes");
        }
    }
}
