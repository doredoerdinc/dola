namespace dola.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Owner", "Industry_SysCode", "dbo.Industry");
            DropForeignKey("dbo.Address", "City_SysCode", "dbo.City");
            DropForeignKey("dbo.District", "City_SysCode", "dbo.City");
            DropForeignKey("dbo.Owner", "LegalCity_SysCode", "dbo.City");
            DropForeignKey("dbo.FuelPriceChange", "City_SysCode", "dbo.City");
            DropForeignKey("dbo.Address", "District_SysCode", "dbo.District");
            DropForeignKey("dbo.Owner", "LegalDistrict_SysCode", "dbo.District");
            DropForeignKey("dbo.FuelPriceChange", "District_SysCode", "dbo.District");
            DropIndex("dbo.Owner", new[] { "Industry_SysCode" });
            DropIndex("dbo.Owner", new[] { "LegalCity_SysCode" });
            DropIndex("dbo.Owner", new[] { "LegalDistrict_SysCode" });
            DropIndex("dbo.Address", new[] { "City_SysCode" });
            DropIndex("dbo.Address", new[] { "District_SysCode" });
            DropIndex("dbo.District", new[] { "City_SysCode" });
            DropIndex("dbo.FuelPriceChange", new[] { "City_SysCode" });
            DropIndex("dbo.FuelPriceChange", new[] { "District_SysCode" });
            RenameColumn(table: "dbo.Owner", name: "LegalCity_SysCode", newName: "LegalCity_ID");
            RenameColumn(table: "dbo.Owner", name: "LegalDistrict_SysCode", newName: "LegalDistrict_ID");
            RenameColumn(table: "dbo.Address", name: "City_SysCode", newName: "City_ID");
            RenameColumn(table: "dbo.Address", name: "District_SysCode", newName: "District_ID");
            RenameColumn(table: "dbo.District", name: "City_SysCode", newName: "City_ID");
            RenameColumn(table: "dbo.FuelPriceChange", name: "City_SysCode", newName: "City_ID");
            RenameColumn(table: "dbo.FuelPriceChange", name: "District_SysCode", newName: "District_ID");
            DropPrimaryKey("dbo.City");
            DropPrimaryKey("dbo.District");
            CreateTable(
                "dbo.AddressRouteMatrix",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DictanceMeters = c.Double(),
                        Duration = c.String(),
                        StaticDuration = c.String(),
                        EncodedPolyline = c.String(unicode: false),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        CreatedBy_ID = c.Int(),
                        FromAddress_SysCode = c.String(maxLength: 100),
                        ToAddressCode_SysCode = c.String(maxLength: 100),
                        UpdateBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.Address", t => t.FromAddress_SysCode)
                .ForeignKey("dbo.Address", t => t.ToAddressCode_SysCode)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.FromAddress_SysCode)
                .Index(t => t.ToAddressCode_SysCode)
                .Index(t => t.UpdateBy_ID);
            
            CreateTable(
                "dbo.WorkingArea",
                c => new
                    {
                        SysCode = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        CreatedBy_ID = c.Int(),
                        DeliveryPerson1_SysCode = c.String(maxLength: 100),
                        DeliveryPerson2_SysCode = c.String(maxLength: 100),
                        ResponsiblePerson_SysCode = c.String(maxLength: 100),
                        UpdateBy_ID = c.Int(),
                        Vehicle_SysCode = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SysCode)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.Person", t => t.DeliveryPerson1_SysCode)
                .ForeignKey("dbo.Person", t => t.DeliveryPerson2_SysCode)
                .ForeignKey("dbo.Person", t => t.ResponsiblePerson_SysCode)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .ForeignKey("dbo.Vehicle", t => t.Vehicle_SysCode)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.DeliveryPerson1_SysCode)
                .Index(t => t.DeliveryPerson2_SysCode)
                .Index(t => t.ResponsiblePerson_SysCode)
                .Index(t => t.UpdateBy_ID)
                .Index(t => t.Vehicle_SysCode);
            
            CreateTable(
                "dbo.Neighborhood",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PostalCode = c.Double(),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        City_ID = c.Int(),
                        CreatedBy_ID = c.Int(),
                        District_ID = c.Int(),
                        Town_ID = c.Int(),
                        UpdateBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.City_ID)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.District", t => t.District_ID)
                .ForeignKey("dbo.Town", t => t.Town_ID)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .Index(t => t.City_ID)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.District_ID)
                .Index(t => t.Town_ID)
                .Index(t => t.UpdateBy_ID);
            
            CreateTable(
                "dbo.Town",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        City_ID = c.Int(),
                        CreatedBy_ID = c.Int(),
                        District_ID = c.Int(),
                        UpdateBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.City", t => t.City_ID)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.District", t => t.District_ID)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .Index(t => t.City_ID)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.District_ID)
                .Index(t => t.UpdateBy_ID);
            
            CreateTable(
                "dbo.ExTernalDocument",
                c => new
                    {
                        SysCode = c.String(nullable: false, maxLength: 100),
                        DocumentName = c.String(),
                        RequestPlanedDate = c.DateTime(),
                        ImportDate = c.DateTime(),
                        DocumentImportCount = c.Int(),
                        DocumentMapSuccessCount = c.Int(),
                        DocumentMapProcessCount = c.Int(),
                        DocumentMapErrorCount = c.Int(),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        CreatedBy_ID = c.Int(),
                        Owner_SysCode = c.String(maxLength: 100),
                        UpdateBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.SysCode)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.Owner", t => t.Owner_SysCode)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.Owner_SysCode)
                .Index(t => t.UpdateBy_ID);
            
            CreateTable(
                "dbo.ExTernalOrderItem",
                c => new
                    {
                        SysCode = c.String(nullable: false, maxLength: 100),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        Message = c.String(),
                        Teslimat_Kod = c.String(),
                        Bolge_Muduru = c.String(),
                        Gonderen_Magaza_Kod = c.String(),
                        Gonderen_Magaza_Adi = c.String(),
                        Gonderen_Magaza_iletisim_tel = c.String(),
                        Teslim_Magaza_Kod = c.String(),
                        Teslim_Magaza_Adi = c.String(),
                        Teslim_Magaza_iletisim_tel = c.String(),
                        Urun_Kod = c.String(),
                        Urun_Adi = c.String(),
                        Tasima_Tipi = c.String(),
                        Miktar = c.String(),
                        Adet_GR = c.String(),
                        Region = c.String(),
                        State = c.String(),
                        Description = c.String(),
                        LastLocation = c.String(),
                        CreatedBy_ID = c.Int(),
                        Document_SysCode = c.String(maxLength: 100),
                        Owner_SysCode = c.String(maxLength: 100),
                        StateIntegration_SysCode = c.String(maxLength: 100),
                        UpdateBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.SysCode)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.ExTernalDocument", t => t.Document_SysCode)
                .ForeignKey("dbo.Owner", t => t.Owner_SysCode)
                .ForeignKey("dbo.State", t => t.StateIntegration_SysCode)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.Document_SysCode)
                .Index(t => t.Owner_SysCode)
                .Index(t => t.StateIntegration_SysCode)
                .Index(t => t.UpdateBy_ID);
            
            CreateTable(
                "dbo.TripCargo",
                c => new
                    {
                        SysCode = c.String(nullable: false, maxLength: 100),
                        PlanedStartDate = c.DateTime(),
                        PlanedFinishDate = c.DateTime(),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        CreatedBy_ID = c.Int(),
                        Driver_SysCode = c.String(maxLength: 100),
                        StartAddress_SysCode = c.String(maxLength: 100),
                        State_SysCode = c.String(maxLength: 100),
                        TaskTemplate_SysCode = c.String(maxLength: 100),
                        UpdateBy_ID = c.Int(),
                        Vehicle_SysCode = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SysCode)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.Person", t => t.Driver_SysCode)
                .ForeignKey("dbo.Address", t => t.StartAddress_SysCode)
                .ForeignKey("dbo.State", t => t.State_SysCode)
                .ForeignKey("dbo.TaskTemplate", t => t.TaskTemplate_SysCode)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .ForeignKey("dbo.Vehicle", t => t.Vehicle_SysCode)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.Driver_SysCode)
                .Index(t => t.StartAddress_SysCode)
                .Index(t => t.State_SysCode)
                .Index(t => t.TaskTemplate_SysCode)
                .Index(t => t.UpdateBy_ID)
                .Index(t => t.Vehicle_SysCode);
            
            CreateTable(
                "dbo.WorkingTimes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        CreateTime = c.DateTime(),
                        UpdateTime = c.DateTime(),
                        Address_SysCode = c.String(maxLength: 100),
                        CreatedBy_ID = c.Int(),
                        UpdateBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Address", t => t.Address_SysCode)
                .ForeignKey("dbo.User", t => t.CreatedBy_ID)
                .ForeignKey("dbo.User", t => t.UpdateBy_ID)
                .Index(t => t.Address_SysCode)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.UpdateBy_ID);
            
            AddColumn("dbo.User", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.User", "Owner_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.User", "WorkingArea_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.Order", "TripCargo_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.OrderLine", "IntegrationCode", c => c.String());
            AddColumn("dbo.OrderLine", "Owner_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.Item", "TransportType", c => c.String());
            AddColumn("dbo.Item", "State_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.Address", "DistanceMaptoAddressQuantity", c => c.Double());
            AddColumn("dbo.Address", "Description2", c => c.String());
            AddColumn("dbo.Address", "ContactName", c => c.String());
            AddColumn("dbo.Address", "ContactPhone", c => c.String());
            AddColumn("dbo.Address", "IntegrationCode", c => c.String());
            AddColumn("dbo.Address", "Neighborhood_ID", c => c.Int());
            AddColumn("dbo.Address", "Town_ID", c => c.Int());
            AddColumn("dbo.Address", "WorkingArea_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.City", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.District", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.LocationGeo", "Title", c => c.String());
            AddColumn("dbo.LocationGeo", "Marker", c => c.String());
            AddColumn("dbo.Task", "TripCargo_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.RoutePlan", "PlannedStartTime", c => c.DateTime());
            AddColumn("dbo.RoutePlan", "PlannedFinishedTime", c => c.DateTime());
            AlterColumn("dbo.Owner", "LegalCity_ID", c => c.Int());
            AlterColumn("dbo.Owner", "LegalDistrict_ID", c => c.Int());
            AlterColumn("dbo.Address", "City_ID", c => c.Int());
            AlterColumn("dbo.Address", "District_ID", c => c.Int());
            AlterColumn("dbo.District", "City_ID", c => c.Int());
            AlterColumn("dbo.LocationGeo", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.LocationGeo", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.FuelPriceChange", "City_ID", c => c.Int());
            AlterColumn("dbo.FuelPriceChange", "District_ID", c => c.Int());
            AddPrimaryKey("dbo.City", "ID");
            AddPrimaryKey("dbo.District", "ID");
            CreateIndex("dbo.User", "Owner_SysCode");
            CreateIndex("dbo.User", "WorkingArea_SysCode");
            CreateIndex("dbo.Owner", "LegalCity_ID");
            CreateIndex("dbo.Owner", "LegalDistrict_ID");
            CreateIndex("dbo.District", "City_ID");
            CreateIndex("dbo.Address", "City_ID");
            CreateIndex("dbo.Address", "District_ID");
            CreateIndex("dbo.Address", "Neighborhood_ID");
            CreateIndex("dbo.Address", "Town_ID");
            CreateIndex("dbo.Address", "WorkingArea_SysCode");
            CreateIndex("dbo.Order", "TripCargo_SysCode");
            CreateIndex("dbo.OrderLine", "Owner_SysCode");
            CreateIndex("dbo.Item", "State_SysCode");
            CreateIndex("dbo.Task", "TripCargo_SysCode");
            CreateIndex("dbo.FuelPriceChange", "City_ID");
            CreateIndex("dbo.FuelPriceChange", "District_ID");
            AddForeignKey("dbo.User", "Owner_SysCode", "dbo.Owner", "SysCode");
            AddForeignKey("dbo.Address", "Neighborhood_ID", "dbo.Neighborhood", "ID");
            AddForeignKey("dbo.Address", "Town_ID", "dbo.Town", "ID");
            AddForeignKey("dbo.Address", "WorkingArea_SysCode", "dbo.WorkingArea", "SysCode");
            AddForeignKey("dbo.User", "WorkingArea_SysCode", "dbo.WorkingArea", "SysCode");
            AddForeignKey("dbo.Item", "State_SysCode", "dbo.State", "SysCode");
            AddForeignKey("dbo.OrderLine", "Owner_SysCode", "dbo.Owner", "SysCode");
            AddForeignKey("dbo.Order", "TripCargo_SysCode", "dbo.TripCargo", "SysCode");
            AddForeignKey("dbo.Task", "TripCargo_SysCode", "dbo.TripCargo", "SysCode");
            AddForeignKey("dbo.Owner", "LegalCity_ID", "dbo.City", "ID");
            AddForeignKey("dbo.District", "City_ID", "dbo.City", "ID");
            AddForeignKey("dbo.Address", "City_ID", "dbo.City", "ID");
            AddForeignKey("dbo.FuelPriceChange", "City_ID", "dbo.City", "ID");
            AddForeignKey("dbo.Owner", "LegalDistrict_ID", "dbo.District", "ID");
            AddForeignKey("dbo.Address", "District_ID", "dbo.District", "ID");
            AddForeignKey("dbo.FuelPriceChange", "District_ID", "dbo.District", "ID");
            DropColumn("dbo.User", "Warehouse");
            DropColumn("dbo.Owner", "Industry_SysCode");
            DropColumn("dbo.City", "SysCode");
            DropColumn("dbo.City", "Description");
            DropColumn("dbo.District", "SysCode");
            DropColumn("dbo.District", "Description");
            DropColumn("dbo.LocationGeo", "Name");
            DropColumn("dbo.RoutePlan", "PlanedStartTime");
            DropColumn("dbo.RoutePlan", "PlanedFinishedTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoutePlan", "PlanedFinishedTime", c => c.DateTime());
            AddColumn("dbo.RoutePlan", "PlanedStartTime", c => c.DateTime());
            AddColumn("dbo.LocationGeo", "Name", c => c.String());
            AddColumn("dbo.District", "Description", c => c.String());
            AddColumn("dbo.District", "SysCode", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.City", "Description", c => c.String());
            AddColumn("dbo.City", "SysCode", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Owner", "Industry_SysCode", c => c.String(maxLength: 100));
            AddColumn("dbo.User", "Warehouse", c => c.String());
            DropForeignKey("dbo.FuelPriceChange", "District_ID", "dbo.District");
            DropForeignKey("dbo.Address", "District_ID", "dbo.District");
            DropForeignKey("dbo.Owner", "LegalDistrict_ID", "dbo.District");
            DropForeignKey("dbo.FuelPriceChange", "City_ID", "dbo.City");
            DropForeignKey("dbo.Address", "City_ID", "dbo.City");
            DropForeignKey("dbo.District", "City_ID", "dbo.City");
            DropForeignKey("dbo.Owner", "LegalCity_ID", "dbo.City");
            DropForeignKey("dbo.WorkingTimes", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.WorkingTimes", "CreatedBy_ID", "dbo.User");
            DropForeignKey("dbo.WorkingTimes", "Address_SysCode", "dbo.Address");
            DropForeignKey("dbo.TripCargo", "Vehicle_SysCode", "dbo.Vehicle");
            DropForeignKey("dbo.TripCargo", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.TripCargo", "TaskTemplate_SysCode", "dbo.TaskTemplate");
            DropForeignKey("dbo.Task", "TripCargo_SysCode", "dbo.TripCargo");
            DropForeignKey("dbo.TripCargo", "State_SysCode", "dbo.State");
            DropForeignKey("dbo.TripCargo", "StartAddress_SysCode", "dbo.Address");
            DropForeignKey("dbo.Order", "TripCargo_SysCode", "dbo.TripCargo");
            DropForeignKey("dbo.TripCargo", "Driver_SysCode", "dbo.Person");
            DropForeignKey("dbo.TripCargo", "CreatedBy_ID", "dbo.User");
            DropForeignKey("dbo.ExTernalDocument", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.ExTernalDocument", "Owner_SysCode", "dbo.Owner");
            DropForeignKey("dbo.ExTernalOrderItem", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.ExTernalOrderItem", "StateIntegration_SysCode", "dbo.State");
            DropForeignKey("dbo.ExTernalOrderItem", "Owner_SysCode", "dbo.Owner");
            DropForeignKey("dbo.ExTernalOrderItem", "Document_SysCode", "dbo.ExTernalDocument");
            DropForeignKey("dbo.ExTernalOrderItem", "CreatedBy_ID", "dbo.User");
            DropForeignKey("dbo.ExTernalDocument", "CreatedBy_ID", "dbo.User");
            DropForeignKey("dbo.OrderLine", "Owner_SysCode", "dbo.Owner");
            DropForeignKey("dbo.Item", "State_SysCode", "dbo.State");
            DropForeignKey("dbo.AddressRouteMatrix", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.AddressRouteMatrix", "ToAddressCode_SysCode", "dbo.Address");
            DropForeignKey("dbo.AddressRouteMatrix", "FromAddress_SysCode", "dbo.Address");
            DropForeignKey("dbo.AddressRouteMatrix", "CreatedBy_ID", "dbo.User");
            DropForeignKey("dbo.User", "WorkingArea_SysCode", "dbo.WorkingArea");
            DropForeignKey("dbo.WorkingArea", "Vehicle_SysCode", "dbo.Vehicle");
            DropForeignKey("dbo.WorkingArea", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.WorkingArea", "ResponsiblePerson_SysCode", "dbo.Person");
            DropForeignKey("dbo.WorkingArea", "DeliveryPerson2_SysCode", "dbo.Person");
            DropForeignKey("dbo.WorkingArea", "DeliveryPerson1_SysCode", "dbo.Person");
            DropForeignKey("dbo.WorkingArea", "CreatedBy_ID", "dbo.User");
            DropForeignKey("dbo.Address", "WorkingArea_SysCode", "dbo.WorkingArea");
            DropForeignKey("dbo.Address", "Town_ID", "dbo.Town");
            DropForeignKey("dbo.Address", "Neighborhood_ID", "dbo.Neighborhood");
            DropForeignKey("dbo.Neighborhood", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.Neighborhood", "Town_ID", "dbo.Town");
            DropForeignKey("dbo.Town", "UpdateBy_ID", "dbo.User");
            DropForeignKey("dbo.Town", "District_ID", "dbo.District");
            DropForeignKey("dbo.Town", "CreatedBy_ID", "dbo.User");
            DropForeignKey("dbo.Town", "City_ID", "dbo.City");
            DropForeignKey("dbo.Neighborhood", "District_ID", "dbo.District");
            DropForeignKey("dbo.Neighborhood", "CreatedBy_ID", "dbo.User");
            DropForeignKey("dbo.Neighborhood", "City_ID", "dbo.City");
            DropForeignKey("dbo.User", "Owner_SysCode", "dbo.Owner");
            DropIndex("dbo.WorkingTimes", new[] { "UpdateBy_ID" });
            DropIndex("dbo.WorkingTimes", new[] { "CreatedBy_ID" });
            DropIndex("dbo.WorkingTimes", new[] { "Address_SysCode" });
            DropIndex("dbo.TripCargo", new[] { "Vehicle_SysCode" });
            DropIndex("dbo.TripCargo", new[] { "UpdateBy_ID" });
            DropIndex("dbo.TripCargo", new[] { "TaskTemplate_SysCode" });
            DropIndex("dbo.TripCargo", new[] { "State_SysCode" });
            DropIndex("dbo.TripCargo", new[] { "StartAddress_SysCode" });
            DropIndex("dbo.TripCargo", new[] { "Driver_SysCode" });
            DropIndex("dbo.TripCargo", new[] { "CreatedBy_ID" });
            DropIndex("dbo.FuelPriceChange", new[] { "District_ID" });
            DropIndex("dbo.FuelPriceChange", new[] { "City_ID" });
            DropIndex("dbo.ExTernalOrderItem", new[] { "UpdateBy_ID" });
            DropIndex("dbo.ExTernalOrderItem", new[] { "StateIntegration_SysCode" });
            DropIndex("dbo.ExTernalOrderItem", new[] { "Owner_SysCode" });
            DropIndex("dbo.ExTernalOrderItem", new[] { "Document_SysCode" });
            DropIndex("dbo.ExTernalOrderItem", new[] { "CreatedBy_ID" });
            DropIndex("dbo.ExTernalDocument", new[] { "UpdateBy_ID" });
            DropIndex("dbo.ExTernalDocument", new[] { "Owner_SysCode" });
            DropIndex("dbo.ExTernalDocument", new[] { "CreatedBy_ID" });
            DropIndex("dbo.Task", new[] { "TripCargo_SysCode" });
            DropIndex("dbo.Item", new[] { "State_SysCode" });
            DropIndex("dbo.OrderLine", new[] { "Owner_SysCode" });
            DropIndex("dbo.Order", new[] { "TripCargo_SysCode" });
            DropIndex("dbo.Town", new[] { "UpdateBy_ID" });
            DropIndex("dbo.Town", new[] { "District_ID" });
            DropIndex("dbo.Town", new[] { "CreatedBy_ID" });
            DropIndex("dbo.Town", new[] { "City_ID" });
            DropIndex("dbo.Neighborhood", new[] { "UpdateBy_ID" });
            DropIndex("dbo.Neighborhood", new[] { "Town_ID" });
            DropIndex("dbo.Neighborhood", new[] { "District_ID" });
            DropIndex("dbo.Neighborhood", new[] { "CreatedBy_ID" });
            DropIndex("dbo.Neighborhood", new[] { "City_ID" });
            DropIndex("dbo.Address", new[] { "WorkingArea_SysCode" });
            DropIndex("dbo.Address", new[] { "Town_ID" });
            DropIndex("dbo.Address", new[] { "Neighborhood_ID" });
            DropIndex("dbo.Address", new[] { "District_ID" });
            DropIndex("dbo.Address", new[] { "City_ID" });
            DropIndex("dbo.WorkingArea", new[] { "Vehicle_SysCode" });
            DropIndex("dbo.WorkingArea", new[] { "UpdateBy_ID" });
            DropIndex("dbo.WorkingArea", new[] { "ResponsiblePerson_SysCode" });
            DropIndex("dbo.WorkingArea", new[] { "DeliveryPerson2_SysCode" });
            DropIndex("dbo.WorkingArea", new[] { "DeliveryPerson1_SysCode" });
            DropIndex("dbo.WorkingArea", new[] { "CreatedBy_ID" });
            DropIndex("dbo.District", new[] { "City_ID" });
            DropIndex("dbo.Owner", new[] { "LegalDistrict_ID" });
            DropIndex("dbo.Owner", new[] { "LegalCity_ID" });
            DropIndex("dbo.User", new[] { "WorkingArea_SysCode" });
            DropIndex("dbo.User", new[] { "Owner_SysCode" });
            DropIndex("dbo.AddressRouteMatrix", new[] { "UpdateBy_ID" });
            DropIndex("dbo.AddressRouteMatrix", new[] { "ToAddressCode_SysCode" });
            DropIndex("dbo.AddressRouteMatrix", new[] { "FromAddress_SysCode" });
            DropIndex("dbo.AddressRouteMatrix", new[] { "CreatedBy_ID" });
            DropPrimaryKey("dbo.District");
            DropPrimaryKey("dbo.City");
            AlterColumn("dbo.FuelPriceChange", "District_ID", c => c.String(maxLength: 100));
            AlterColumn("dbo.FuelPriceChange", "City_ID", c => c.String(maxLength: 100));
            AlterColumn("dbo.LocationGeo", "Longitude", c => c.Double());
            AlterColumn("dbo.LocationGeo", "Latitude", c => c.Double());
            AlterColumn("dbo.District", "City_ID", c => c.String(maxLength: 100));
            AlterColumn("dbo.Address", "District_ID", c => c.String(maxLength: 100));
            AlterColumn("dbo.Address", "City_ID", c => c.String(maxLength: 100));
            AlterColumn("dbo.Owner", "LegalDistrict_ID", c => c.String(maxLength: 100));
            AlterColumn("dbo.Owner", "LegalCity_ID", c => c.String(maxLength: 100));
            DropColumn("dbo.RoutePlan", "PlannedFinishedTime");
            DropColumn("dbo.RoutePlan", "PlannedStartTime");
            DropColumn("dbo.Task", "TripCargo_SysCode");
            DropColumn("dbo.LocationGeo", "Marker");
            DropColumn("dbo.LocationGeo", "Title");
            DropColumn("dbo.District", "ID");
            DropColumn("dbo.City", "ID");
            DropColumn("dbo.Address", "WorkingArea_SysCode");
            DropColumn("dbo.Address", "Town_ID");
            DropColumn("dbo.Address", "Neighborhood_ID");
            DropColumn("dbo.Address", "IntegrationCode");
            DropColumn("dbo.Address", "ContactPhone");
            DropColumn("dbo.Address", "ContactName");
            DropColumn("dbo.Address", "Description2");
            DropColumn("dbo.Address", "DistanceMaptoAddressQuantity");
            DropColumn("dbo.Item", "State_SysCode");
            DropColumn("dbo.Item", "TransportType");
            DropColumn("dbo.OrderLine", "Owner_SysCode");
            DropColumn("dbo.OrderLine", "IntegrationCode");
            DropColumn("dbo.Order", "TripCargo_SysCode");
            DropColumn("dbo.User", "WorkingArea_SysCode");
            DropColumn("dbo.User", "Owner_SysCode");
            DropColumn("dbo.User", "Discriminator");
            DropTable("dbo.WorkingTimes");
            DropTable("dbo.TripCargo");
            DropTable("dbo.ExTernalOrderItem");
            DropTable("dbo.ExTernalDocument");
            DropTable("dbo.Town");
            DropTable("dbo.Neighborhood");
            DropTable("dbo.WorkingArea");
            DropTable("dbo.AddressRouteMatrix");
            AddPrimaryKey("dbo.District", "SysCode");
            AddPrimaryKey("dbo.City", "SysCode");
            RenameColumn(table: "dbo.FuelPriceChange", name: "District_ID", newName: "District_SysCode");
            RenameColumn(table: "dbo.FuelPriceChange", name: "City_ID", newName: "City_SysCode");
            RenameColumn(table: "dbo.District", name: "City_ID", newName: "City_SysCode");
            RenameColumn(table: "dbo.Address", name: "District_ID", newName: "District_SysCode");
            RenameColumn(table: "dbo.Address", name: "City_ID", newName: "City_SysCode");
            RenameColumn(table: "dbo.Owner", name: "LegalDistrict_ID", newName: "LegalDistrict_SysCode");
            RenameColumn(table: "dbo.Owner", name: "LegalCity_ID", newName: "LegalCity_SysCode");
            CreateIndex("dbo.FuelPriceChange", "District_SysCode");
            CreateIndex("dbo.FuelPriceChange", "City_SysCode");
            CreateIndex("dbo.District", "City_SysCode");
            CreateIndex("dbo.Address", "District_SysCode");
            CreateIndex("dbo.Address", "City_SysCode");
            CreateIndex("dbo.Owner", "LegalDistrict_SysCode");
            CreateIndex("dbo.Owner", "LegalCity_SysCode");
            CreateIndex("dbo.Owner", "Industry_SysCode");
            AddForeignKey("dbo.FuelPriceChange", "District_SysCode", "dbo.District", "SysCode");
            AddForeignKey("dbo.Owner", "LegalDistrict_SysCode", "dbo.District", "SysCode");
            AddForeignKey("dbo.Address", "District_SysCode", "dbo.District", "SysCode");
            AddForeignKey("dbo.FuelPriceChange", "City_SysCode", "dbo.City", "SysCode");
            AddForeignKey("dbo.Owner", "LegalCity_SysCode", "dbo.City", "SysCode");
            AddForeignKey("dbo.District", "City_SysCode", "dbo.City", "SysCode");
            AddForeignKey("dbo.Address", "City_SysCode", "dbo.City", "SysCode");
            AddForeignKey("dbo.Owner", "Industry_SysCode", "dbo.Industry", "SysCode");
        }
    }
}
