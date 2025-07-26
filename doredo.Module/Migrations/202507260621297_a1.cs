namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkingTime", "RouteRow", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkingTime", "RouteRow");
        }
    }
}
