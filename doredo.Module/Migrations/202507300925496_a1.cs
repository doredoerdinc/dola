namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "UncalculateAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "UncalculateAddress");
        }
    }
}
