using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using LgsLib.Base;
using LgsLib.Base.PermissionPolicy;
using LgsLib.Base.StateLGS; 
namespace LgsLib.Base
{
    public class LgsLibDbContext : DbContext
    {
        public LgsLibDbContext(String connectionString) : base(connectionString)
        {
        }
        public LgsLibDbContext(DbConnection connection) : base(connection, false)
        {

        }
        public LgsLibDbContext()
        : base("name=ConnectionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Ignore<ClientMessage>();
            
        }

        
         public DbSet<LgsLib.Base.StateLGS.StateMachine> StateMachine { get; set; }
        public DbSet<ReportDataV2> ReportDataV2 { get; set; } 
        public DbSet<StateMachineState> StateMachineStates { get; set; }
        public DbSet<StateMachineTransition> StateMachineTransitions { get; set; }
        public DbSet<StateMachineAppearance> StateMachineAppearances { get; set; } 
        public DbSet<ModuleInfo> ModulesInfo { get; set; }
        public DbSet<DashboardData> DashboardData { get; set; }
        public DbSet<ModelDifference> ModelDifferences { get; set; }
        public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<StateHistory> StatusHistory { get; set; } 
        public DbSet<TaskLGS> TaskLGS { get; set; } 
        public DbSet<TaskLGSType> TaskLGSType { get; set; }
        public DbSet<LogLgs> LogLgs { get; set; } 
        public DbSet<State> State { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Owner> Owner { get; set; }


    public DbSet<MemberPermissionsObject> MemberPermissionsObject { get; set; }
        public DbSet<ObjectPermissionsObject> ObjectPermissionsObject { get; set; }
        public DbSet<TypePermissionObject> TypePermissionObject { get; set; }
      
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }

    public DbSet<District> District { get; set; }

    public DbSet<User> Users { get; set; }
      


    }
}