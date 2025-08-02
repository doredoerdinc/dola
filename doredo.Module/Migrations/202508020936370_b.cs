namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
        //    DropColumn("dbo.WorkingTime", "RoutePlanedArivedTime");
        //    DropColumn("dbo.WorkingTime", "RoutePlanedArrivedDay");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.WorkingTime", "RoutePlanedArrivedDay", c => c.Double());
            //AddColumn("dbo.WorkingTime", "RoutePlanedArivedTime", c => c.Time(nullable: false, precision: 7));
        }
    }
}
