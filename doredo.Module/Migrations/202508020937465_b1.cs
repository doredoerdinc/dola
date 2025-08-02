namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkingTime", "RoutePlanedArivedTime", c => c.Time(precision: 7));
            AddColumn("dbo.WorkingTime", "RoutePlanedArrivedDay", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkingTime", "RoutePlanedArrivedDay");
            DropColumn("dbo.WorkingTime", "RoutePlanedArivedTime");
        }
    }
}
