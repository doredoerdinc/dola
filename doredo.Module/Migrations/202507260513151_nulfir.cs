namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nulfir : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkingTime", "DayOfWeek", c => c.Int());
            AlterColumn("dbo.WorkingTime", "StartTime", c => c.Time(precision: 7));
            AlterColumn("dbo.WorkingTime", "FinishTime", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkingTime", "FinishTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.WorkingTime", "StartTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.WorkingTime", "DayOfWeek", c => c.Int(nullable: false));
        }
    }
}
