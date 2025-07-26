namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoutePlanTransport", "StartAddress_SysCode", "dbo.Address");
            DropIndex("dbo.RoutePlanTransport", new[] { "StartAddress_SysCode" });
            AddColumn("dbo.WorkingTime", "FinishTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.WorkingTime", "StartTime", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.WorkingTime", "EndTime");
            DropColumn("dbo.RoutePlanTransport", "StartAddress_SysCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoutePlanTransport", "StartAddress_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.WorkingTime", "EndTime", c => c.DateTime());
            AlterColumn("dbo.WorkingTime", "StartTime", c => c.DateTime());
            DropColumn("dbo.WorkingTime", "FinishTime");
            CreateIndex("dbo.RoutePlanTransport", "StartAddress_SysCode");
            AddForeignKey("dbo.RoutePlanTransport", "StartAddress_SysCode", "dbo.Address", "SysCode");
        }
    }
}
