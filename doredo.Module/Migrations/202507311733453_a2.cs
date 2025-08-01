namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkingTime", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkingTime", "Description");
        }
    }
}
