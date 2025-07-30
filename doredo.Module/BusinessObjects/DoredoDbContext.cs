using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EF.DesignTime;
using LgsLib.Base.PermissionPolicy;
using LgsLib.Base;
using LgsLib.Base.StateLGS;

namespace dola.Module {

    public class dolaDbContext : LgsLibDbContext
    {
        public dolaDbContext(String connectionString) : base("name=ConnectionString")
        {
            // Configuration.ProxyCreationEnabled = false;
        }
        public dolaDbContext(DbConnection connection) : base(connection)
        {
            //Configuration.ProxyCreationEnabled = false;
        }
        public dolaDbContext() : base()
        {
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.ValidateOnSaveEnabled = false; 

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Address>().HasRequired(a => a.LocationGeo).WithOptional(l => l.Address);
            // modelBuilder.Ignore<ClientMessage>();

        }


        public DbSet<Person> Driver { get; set; }
        //  public DbSet<OperationGroup> OperationGroup { get; set; }
        //public DbSet<Transport> Transport { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Attribute> Attribute { get; set; }
        public DbSet<LabelStatic> LabelStatic { get; set; }
        public DbSet<Trip> Operation { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ObjectCode> ObjectCode { get; set; }
        public DbSet<VehicleFuel> VehicleFuel { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<TaskOperationType> TaskOperationType { get; set; }
        public DbSet<LocationLevel> LocationLayer { get; set; }
        public DbSet<LocationColumn> LocationColumn { get; set; }
        public DbSet<LocationHoll> LocationHoll { get; set; } 
        public DbSet<TaskStepTransactionVW> TaskStepTransactionVW { get; set; } 
        public DbSet<UnitConvert> UnitConvert { get; set; } 
        public DbSet<RoutePlanTransport> RoutePlan { get; set; }
        public DbSet<StockControl> StockCheck { get; set; }
        public DbSet<StockControlStep> StockCheckLocation { get; set; }
        public DbSet<TaskStepStatic> TaskStepStatic { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<VehicleBrand> VehicleBrand { get; set; }
        public DbSet<VehicleModel> VehicleModel { get; set; }
        public DbSet<VehicleReport> VehicleReport { get; set; } 
        public DbSet<RotaOperationSmart> RotaOperationSmart { get; set; }
        public DbSet<TaskTemplate> TaskStepTemplate { get; set; }
        public DbSet<TaskTemplateItem> TaskStepTemplateItem { get; set; } 
        public DbSet<TaskItemCriteriaType> TaskItemCriteriaType { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemType> ItemType { get; set; }  
        public DbSet<LocationGeo> LocationGeo { get; set; } 
        public DbSet<LocationGeoPoints> LocationGeoPoints { get; set; }   
        public DbSet<Industry> Industry { get; set; } 
        public DbSet<PositionTemp> PositionTemp { get; set; } 
        public DbSet<ContainerTemplate> LabelTemplate { get; set; } 
        public DbSet<Container> Container { get; set; } 
        public DbSet<StockItem> StockItem { get; set; } 
        public DbSet<DeviceInfo> DeviceManagemant { get; set; }  
        //public DbSet<ShiftOperationVehicle> ShiftOperationVehicle { get; set; } 
        //public DbSet<VehicleDailyData> VehicleDailyData { get; set; }
        public DbSet<VehiclePosition> VehicleTrakingData { get; set; } 
        public DbSet<CurrentData> CuurentData { get; set; } 
        public DbSet<ContainerTemp> ContainerTemp { get; set; }
        public DbSet<JobTitle> JobTitle { get; set; }  
        public DbSet<FuelPriceChange> FuelPriceChange { get; set; } 
        //  public DbSet<Order> Order { get; set; }
        public DbSet<OrderType> OrderType { get; set; } 
        public DbSet<UnitType> UnitType { get; set; } 
        public DbSet<OrderFile> OrderFile { get; set; } 
        public DbSet<DocumentOcr> DocumentOcr { get; set; }  
        public DbSet<DriverWorkingDay> DriverWorkingDay { get; set; } 
        public DbSet<LocationOperation> LocationOperation { get; set; } 
        public DbSet<LocationOperationTariff> LocationOperationTariff { get; set; }
        public DbSet<TaskTransaction> StockTransaction { get; set; }  
        public DbSet<LocationWarehouse> LocationWarehouse{ get; set; }
        public DbSet<LocationFunction> LocationFunction { get; set; }
        public DbSet<LocationZone> LocationZone { get; set; } 
        public DbSet<VehicleWorkingDay> WorkingDay { get; set; } 
        public DbSet<FileType> FileType { get; set; } 
        public DbSet<GeoRoute> GeoRoute { get; set; }
        public DbSet<GeoRoutePoint> GeoRoutePoint { get; set; } 
        public DbSet<DocumentTracking> DocumentTracking { get; set; } 
        public DbSet<Task> Task { get; set; }
        public DbSet<TaskStep> TaskStep { get; set; } 
        public DbSet<TaskTransactionTempory> TaskTransactionTempory { get; set; } 
     //   public DbSet<OrderDailyPlaned> OrderDailyPlaned { get; set; } 
        public DbSet<ExTernalDocument> ExTernalDocument { get; set; }
        public DbSet<ExTernalOrderItem> ExTernalOrderItem { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; } 
        public DbSet<OperationStation> DeviceStation { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<DeviceType> DeviceType { get; set; } 
        public DbSet<TripCargo> TripCargo { get; set; }
        public DbSet<Town> Town { get; set; } 
        public DbSet<Neighborhood> Neighborhood { get; set; }
        public DbSet<WorkingTime> WorkingTimes { get; set; }
        public DbSet<WorkingArea> WorkingArea { get; set; }

        public DbSet<AddressRouteMatrix> AddressRouteMatrix { get; set; }
     //  public DbSet<UserDola> UserDola { get; set; }	
}
}