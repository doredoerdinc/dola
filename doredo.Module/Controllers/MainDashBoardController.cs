using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace dola.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MainDashBoardController : ViewController<DashboardView>
    {
        public MainDashBoardController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        } 
        private void RelationVehicleOrder_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            EntryLocation vehicle = null;
            List<Order> Orders = new List<Order>();
            var newobjectSpace = Application.CreateObjectSpace(); 

            var orderDBV =  View.Items.FirstOrDefault(x=>x.Id== "Order_Ready_DashBoardView") as DashboardViewItem;
            var orderList = orderDBV.InnerView as ListView;

            var entryVehicleDBV = View.Items.FirstOrDefault(x => x.Id == "Entry_Vehicle_DashBoardView") as DashboardViewItem;
            var vehicleList = entryVehicleDBV.InnerView as ListView;

            if (vehicleList.SelectedObjects.Count != 1)
            {
                throw new UserFriendlyException("Lütfen Araç seçimi yapınız...");
            }
            else
            {
                var vehicleKeyvalue = newobjectSpace.GetKeyValue(vehicleList.CurrentObject);
                vehicle = newobjectSpace.GetObjectByKey<EntryLocation>(vehicleKeyvalue);
            }

            if (orderList.SelectedObjects.Count < 1)
            {
                throw new UserFriendlyException("Lütfen en az bir Sipariş seçimi yapınız...");
            }
            {
                foreach (var ord in orderList.SelectedObjects)
                {
                    var keyvalue = newobjectSpace.GetKeyValue(ord);
                    var gOrder = newobjectSpace.GetObjectByKey<Order>(keyvalue);
                   
                    Orders.Add(gOrder);
                }
            }

            if (vehicle != null && Orders.Count > 0)
            {
                foreach (var sord in Orders)
                {
                    vehicle.Orders.Add(sord);
                    sord.LastEntryLocation = vehicle;
                }
                newobjectSpace.CommitChanges();
            }

            vehicleList.ObjectSpace.Refresh();
       //     entryVehicleDBV.View.Refresh();
            Orders.Clear();
        }
        private void RelationTripOrder_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Trip Trip = null;
            List<Order> Orders = new List<Order>();
            var newobjectSpace = Application.CreateObjectSpace();

            var orderDBV = View.Items.FirstOrDefault(x => x.Id == "Order_Ready_DashBoardView") as DashboardViewItem;
            var orderList = orderDBV.InnerView as ListView;

            var TripDBV = View.Items.FirstOrDefault(x => x.Id == "Trip_DashBoardView") as DashboardViewItem;
            var vehicleList = TripDBV.InnerView as ListView;

            if (vehicleList.SelectedObjects.Count != 1)
            {
                throw new UserFriendlyException("Lütfen Sefer seçimiyapınız yapınız...");
            }
            else
            {
                var vehicleKeyvalue = newobjectSpace.GetKeyValue(vehicleList.CurrentObject);
                Trip = newobjectSpace.GetObjectByKey<Trip>(vehicleKeyvalue);
            }

            if (orderList.SelectedObjects.Count < 1)
            {
                throw new UserFriendlyException("Lütfen en az bir Sipariş seçimi yapınız...");
            }
            {
                foreach (var ord in orderList.SelectedObjects)
                {
                    var keyvalue = newobjectSpace.GetKeyValue(ord);
                    var gOrder = newobjectSpace.GetObjectByKey<Order>(keyvalue);

                    Orders.Add(gOrder);
                }
            }

            if (Trip != null && Orders.Count > 0)
            {
                //foreach (var sord in Orders)
                //{
                //    Trip.Orders.Add(sord);
                //   // sord.LastTrip = Trip;
                //}
                newobjectSpace.CommitChanges();
            }

            vehicleList.ObjectSpace.Refresh();
            //     entryVehicleDBV.View.Refresh();
            Orders.Clear();
        }
    }
}
