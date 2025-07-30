using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Maps.Web;
using DevExpress.ExpressApp.Maps.Web.Helpers;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Controls;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using LgsLib.Base;
using LgsLib.Base.Web;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Web;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
//using System.Web.Script.Serialization;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.TestScripts;


namespace dola.Module.Web
{ 
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MapController : ViewController
    {  
        public MapController()
        {
            InitializeComponent();
            
          //  SimpleAction mapViewAction = new SimpleAction(this, "MapViewAction", PredefinedCategory.Edit);
            SimpleAction mapViewTripCargoAction = new SimpleAction(this, "mapViewTripCargoAction", PredefinedCategory.Edit);
          //  mapViewAction.Execute += MapViewAction_Execute;
            mapViewTripCargoAction.Execute += MapViewTripCargoAction_Execute;
            mapViewTripCargoAction.TargetObjectType = typeof(TripCargo);
            mapDistanceAddress.TargetObjectType= typeof(Address);
            mapRouteTruckAction.TargetObjectType = typeof(RoutePlanTransport);
            mapRoutePlanCalculate.TargetObjectType = typeof(RoutePlanTransport);

           // mapViewAction.TargetObjectType = typeof(IMapPoint);
           // mapViewAction.TargetObjectType= typeof(IMapMultiplePoint);


            //mapViewTripCargoAction.SetClientScript(CallMapView());
        } 
 
        private void mapDistanceAddressQuatity_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var newObjectSpace = Application.CreateObjectSpace();
            List<Address> orginAddress = new List<Address>();
            foreach (var item in View.SelectedObjects)
            {
                var keyValue = newObjectSpace.GetKeyValue(item);
                var criteria = CriteriaOperator.Parse("SysCode =?", keyValue);
                var locationGeo = newObjectSpace.GetObjectByKey<Address>(keyValue);
                if (locationGeo != null)
                {
                    orginAddress.Add(locationGeo);
                }

            }
           // getCountMapMatrix(orginAddress, newObjectSpace);
            newObjectSpace.CommitChanges();
            View.ObjectSpace.CommitChanges();
        }


            private void mapDistanceAddress_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var newObjectSpace = Application.CreateObjectSpace();
            List<Address> orginAddressList = new List<Address>();
            List<LocationGeo> orginLocationList = new List<LocationGeo>();
            List<LocationGeo> destinationLocationList = new List<LocationGeo>();
          
            
            foreach (var item in View.SelectedObjects)
            {
                var keyValue = newObjectSpace.GetKeyValue(item);
                var addressObject = newObjectSpace.GetObjectByKey<Address>(keyValue);
                var locationGeo = newObjectSpace.GetObject<LocationGeo>(addressObject.LocationGeo);
                if (locationGeo != null)
                {
                    locationGeo.IntegrationCode = keyValue.ToString();
                    orginLocationList.Add(locationGeo);
                }
            }

            this.ExecutePopupSimpleList<Address>((clientModel, nonObjectSpace) =>
            {
               

            }, (clientModel) =>
            {
               
                foreach (var item in clientModel.SelectedObjects)
                {
                    var keyValue = newObjectSpace.GetKeyValue(item);
                    var addressObject = newObjectSpace.GetObjectByKey<Address>(keyValue);
                    var locationGeo = newObjectSpace.GetObject<LocationGeo>(addressObject.LocationGeo); 
                    if (locationGeo != null)
                    {
                        destinationLocationList.Add(locationGeo);
                    }
                    
                }
                if(orginLocationList.Count>0&& destinationLocationList.Count>0)
                {
                    googleService.ComputeRouteMatrix(orginLocationList, destinationLocationList,newObjectSpace);
                }
                //getCountMapMatrix(orginAddressList, newObjectSpace);
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            });  
        }

        //private void getCountMapMatrix(List<Address> adressList, IObjectSpace newObjectSpace)
        //{
        //    //foreach (var item in adressList)
        //    //{
        //    //    var criteria = CriteriaOperator.Parse("FromAddress.SysCode =?", item.SysCode);
        //    //    var getMatrixMap = newObjectSpace.GetObjects<AddressRouteMatrix>(criteria);
        //    //    item.DistanceMaptoAddressQuantity = getMatrixMap.Count();
        //    //}
        //}

        private void MapViewTripCargoAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var points = new List<MapPointLGS>();
            var objectSpace = Application.CreateObjectSpace();
            
            List<Order> selectedOrders = new List<Order>();
             
            foreach (var item in View.SelectedObjects)
            {
                var keyValue = objectSpace.GetKeyValue(item);
                var tripCargo = objectSpace.GetObjectByKey<TripCargo>(keyValue);
                foreach (var ord in tripCargo.Orders)
                {
                    var order = objectSpace.GetObject<Order>(ord);
                    selectedOrders.Add(order); 

                    var findMat = GroupOperator.Combine(
               GroupOperatorType.And
               , new BinaryOperator("FromAddress.Syscode", ord.FromAddress.SysCode, BinaryOperatorType.Equal)
               , new BinaryOperator("ToAddress.SysCode", ord.ToAddress.SysCode, BinaryOperatorType.Equal));

                    GoogleRouteDraw newRouteDraw=null;
                    var findMatRoute = objectSpace.FindObject<AddressRouteMatrix>(findMat);
                    if (MapStatic.StaticRouteDrawList != null)
                    {   newRouteDraw= MapStatic.StaticRouteDrawList.Find(x => x.SysCode == ord.SysCode);
                        if (newRouteDraw == null && findMatRoute!=null)
                        { 
                            newRouteDraw = new GoogleRouteDraw();
                            newRouteDraw.SysCode = ord.SysCode;
                            newRouteDraw.DictanceMeters = findMatRoute.DictanceMeters;
                            newRouteDraw.Duration = findMatRoute.Duration;
                            newRouteDraw.StaticDuration = findMatRoute.StaticDuration;
                            newRouteDraw.EncodedPolyline = findMatRoute.EncodedPolyline;
                            MapStatic.StaticRouteDrawList.Add(newRouteDraw);
                        } 
                    }
                    else
                    {
                        MapStatic.StaticRouteDrawList = new List<GoogleRouteDraw>();
                        newRouteDraw = new GoogleRouteDraw();
                        newRouteDraw.SysCode = ord.SysCode;
                        newRouteDraw.DictanceMeters = findMatRoute.DictanceMeters;
                        newRouteDraw.Duration = findMatRoute.Duration;
                        newRouteDraw.StaticDuration = findMatRoute.StaticDuration;
                        newRouteDraw.EncodedPolyline = findMatRoute.EncodedPolyline;
                        MapStatic.StaticRouteDrawList.Add(newRouteDraw);
                    }
                    
                } 
            }

            if (selectedOrders.Count > 0)
            {
                var groupOrderFrom = selectedOrders.GroupBy(x => new { x.Trip,x.FromAddress }).Select(group => new { groupOrder = group.Key, totalOut = group.Count() });

                foreach (var from in groupOrderFrom)
                {
                    var findPosition = MapStatic.StaticMapPointOrderTransferList.Find(x => x.Key == from.groupOrder.FromAddress.SysCode);
                    if (findPosition == null)
                    {
                        var mapTransfer = MapStatic.MapObjectSpace.CreateObject<MapPointOrderTransfer>();
                        mapTransfer.Key = from.groupOrder.FromAddress.SysCode;
                        mapTransfer.Latitude = from.groupOrder.FromAddress.LocationGeo.Latitude;
                        mapTransfer.Longitude = from.groupOrder.FromAddress.LocationGeo.Longitude;
                        mapTransfer.Title = from.groupOrder.FromAddress.Name;
                        mapTransfer.FromOrderQuantity = from.totalOut;
                        MapStatic.StaticMapPointOrderTransferList.Add(mapTransfer); 
                    }
                
                }
                var groupOrderTo = selectedOrders.GroupBy(x => new { x.Trip, x.ToAddress }).Select(group => new { groupOrder = group.Key, TotalIn = group.Count() });
                foreach (var to in groupOrderTo)
                {
                    var findPosition = MapStatic.StaticMapPointOrderTransferList.Find(x => x.Key == to.groupOrder.ToAddress.SysCode);
                    if (findPosition == null)
                    {
                        var mapTransfer = MapStatic.MapObjectSpace.CreateObject<MapPointOrderTransfer>();
                        mapTransfer.Key = to.groupOrder.ToAddress.SysCode;
                        mapTransfer.Latitude = to.groupOrder.ToAddress.LocationGeo.Latitude;
                        mapTransfer.Longitude = to.groupOrder.ToAddress.LocationGeo.Longitude;
                        mapTransfer.Title = to.groupOrder.ToAddress.Name;
                        mapTransfer.ToOrderQuantity = to.TotalIn;
                        MapStatic.StaticMapPointOrderTransferList.Add(mapTransfer);
                    }
                    else
                    {
                        findPosition.ToOrderQuantity= to.TotalIn;
                    } 
                } 
                
            }
        } 
        private void MapViewAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {          
            var objectSpace = Application.CreateObjectSpace();
            ReloadMap(View);
            object firstObject = null;
            if (View.CurrentObject != null)
            {
                firstObject = View.CurrentObject;
            }
            else
            {
                firstObject = View.SelectedObjects.Cast<IMapPoint>().FirstOrDefault();
            }
            foreach (var viewItem in View.SelectedObjects)
            {
                if (viewItem is IMapPoint)
                {
                    var point = viewItem as IMapPoint;
                    var isPointAdd = MapStatic.StaticPointList.Find(x => x.Key == point.Key);
                    if (isPointAdd == null)
                    {
                        var mapPoint1 = MapStatic.MapObjectSpace.CreateObject<MapPointLGS>();
                        mapPoint1.Key = point.Key;
                        mapPoint1.Latitude = point.Latitude;
                        mapPoint1.Longitude = point.Longitude;
                        mapPoint1.Title = point.Title;
                        mapPoint1.Object = View.ObjectTypeInfo.FullName;
                        MapStatic.StaticPointList.Add(mapPoint1);
                    }
                }
            }
            ShowMapFrmScript();

        }

        private void ShowMapFrmScript()
        {
           
            var MapviewActionPath = "../map/MapView.aspx";
            if (Frame is NestedFrame||View is DetailView)
            {
                MapviewActionPath = "../../map/MapView.aspx";
            }
            var callJavascript = MapStatic.CallMapView(MapviewActionPath);
            var filePath2 = String.Format(AppDomain.CurrentDomain.BaseDirectory + @"\{0}.js", View.ObjectTypeInfo.Name);
            System.IO.File.WriteAllText(filePath2, callJavascript);
            ((WebWindow)WebApplication.Instance.MainWindow).RegisterStartupScript("script", callJavascript);
        }

        private void MapRouteTruckAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        { 
            var objectSpace = Application.CreateObjectSpace();
            ReloadMap(View);
            object firstObject = null;
            if (View.CurrentObject != null)
            {
                firstObject = View.CurrentObject;
            }
            else
            {
                firstObject = View.SelectedObjects.Cast<IMapPoint>().FirstOrDefault();
            }

            foreach (var routeItem in View.SelectedObjects)
            {
                var routeObjectKey = objectSpace.GetKeyValue(routeItem);
                var routeObject = objectSpace.GetObjectByKey<RoutePlanTransport>(routeObjectKey);

                for (int workTm = 0; workTm < routeObject.WorkingTimes.Count() - 1; workTm++)
                {
                    var workObjectFromKeyValue = objectSpace.GetKeyValue(routeObject.WorkingTimes[workTm]);
                    var workObjectFrom = objectSpace.GetObjectByKey<WorkingTime>(workObjectFromKeyValue);

                    var workObjectToKeyValue = objectSpace.GetKeyValue(routeObject.WorkingTimes[workTm + 1]);
                    var workObjectTo = objectSpace.GetObjectByKey<WorkingTime>(workObjectToKeyValue);

                    var findMat = GroupOperator.Combine(
                       GroupOperatorType.And
                       , new BinaryOperator("FromAddress.Syscode", workObjectFrom.Address.SysCode, BinaryOperatorType.Equal)
                       , new BinaryOperator("ToAddress.SysCode", workObjectTo.Address.SysCode, BinaryOperatorType.Equal));

                    GoogleRouteDraw newRouteDraw = null;
                    var findMatRoute = objectSpace.FindObject<AddressRouteMatrix>(findMat);
                    if (findMatRoute == null)
                    {
                        throw new System.ArgumentException(string.Format("Rota Matrisi bulunamadı FromId={0},{1}->ToId={2},{3}", workObjectFrom.ID.ToString(), workObjectFrom.Address.Name, workObjectTo.ID.ToString(), workObjectTo.Address.Name));
                    }
                    var routeKey = routeObject.SysCode + findMatRoute.ID.ToString();
                    if (MapStatic.StaticRouteDrawList != null)
                    {
                        newRouteDraw = MapStatic.StaticRouteDrawList.Find(x => x.SysCode == routeKey);

                        if (newRouteDraw == null && findMatRoute != null)
                        {

                            newRouteDraw = new GoogleRouteDraw();
                            newRouteDraw.SysCode = routeKey;
                            newRouteDraw.DictanceMeters = findMatRoute.DictanceMeters;
                            newRouteDraw.Duration = findMatRoute.Duration;
                            newRouteDraw.StaticDuration = findMatRoute.StaticDuration;
                            newRouteDraw.EncodedPolyline = findMatRoute.EncodedPolyline;
                            newRouteDraw.Object = View.ObjectTypeInfo.FullName;

                            MapStatic.StaticRouteDrawList.Add(newRouteDraw);

                            MapPointLGS pointFrom = MapStatic.StaticPointList.Find(x => x.Key == workObjectFrom.Address.Key);
                            if (pointFrom == null)
                            {
                                pointFrom = MapStatic.MapObjectSpace.CreateObject<MapPointLGS>();
                                pointFrom.Key = workObjectFrom.Address.Key;
                                pointFrom.Latitude = workObjectFrom.Address.Latitude;
                                pointFrom.Longitude = workObjectFrom.Address.Longitude;
                                pointFrom.Title = workObjectFrom.Address.Title;
                                pointFrom.Object = View.ObjectTypeInfo.FullName;
                                MapStatic.StaticPointList.Add(pointFrom);
                            }

                            MapPointLGS pointto = MapStatic.StaticPointList.Find(x => x.Key == workObjectFrom.Address.Key);
                            if (pointto == null)
                            {
                                pointto = MapStatic.MapObjectSpace.CreateObject<MapPointLGS>();
                                pointto.Key = workObjectTo.Address.Key;
                                pointto.Latitude = workObjectTo.Address.Latitude;
                                pointto.Longitude = workObjectTo.Address.Longitude;
                                pointto.Title = workObjectTo.Address.Title;
                                pointto.Object = View.ObjectTypeInfo.FullName;
                                MapStatic.StaticPointList.Add(pointto);
                            }

                        }
                    }                   
                }

            }
            ShowMapFrmScript();
        }

        private void ReloadMap(View view)
        {
            if (MapStatic.StaticRouteDrawList != null)
            {
                MapStatic.StaticRouteDrawList.RemoveAll(x => x.Object == view.ObjectTypeInfo.FullName);
            }

            if (MapStatic.StaticPointList != null)
            {
                MapStatic.StaticPointList.RemoveAll(x => x.Object == view.ObjectTypeInfo.FullName);

            }
        }

        protected override void OnActivated()
        { 
            base.OnActivated();

            if (MapStatic.MapObjectSpace == null)
            {
                MapStatic.MapObjectSpace = Application.CreateObjectSpace(typeof(Map));
            }
            if (MapStatic.StaticPointList == null)
            {
                MapStatic.StaticPointList = new List<MapPointLGS>();
            }
            
            if (MapStatic.StaticMapPointOrderTransferList == null)
            {
                MapStatic.StaticMapPointOrderTransferList = new List<MapPointOrderTransfer>();
            }

        }

        protected override void OnAfterConstruction()
        {
            base.OnAfterConstruction();
      
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated(); 
        } 
        protected override void OnDeactivated()
        {
             base.OnDeactivated();
         
        }
        public AddressRouteMatrix FindClosestAddress(Address FromAddress, List<AddressRouteMatrix> routeMatrix, List<Address> candidateAddresses)
        {
            var candidateIds = candidateAddresses.Select(a => a.SysCode).ToHashSet();

            // fromAddressId'den candidateAddresses'a olan mesafeleri filtrele
            var filteredRoutes = routeMatrix
                .Where(r => r.FromAddress.SysCode == FromAddress.SysCode && candidateIds.Contains(r.ToAddress.SysCode))
                .ToList();

            if (!filteredRoutes.Any())
                return null;

            // En kısa mesafeyi bul
            var closestRoute = filteredRoutes.OrderBy(r => r.DictanceMeters).First();

            return closestRoute;
        }


        private void routePlanCalculate_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            ReloadMap(View);
            object firstObject = null;
            if (View.CurrentObject != null)
            {
                firstObject = View.CurrentObject;
            }
            else
            {
                firstObject = View.SelectedObjects.Cast<IMapPoint>().FirstOrDefault();
            }

            foreach (var routeItem in View.SelectedObjects)
            {
                var routeObjectKey = objectSpace.GetKeyValue(routeItem);
                var routeObject = objectSpace.GetObjectByKey<RoutePlanTransport>(routeObjectKey);
                List<Address> routeAddressList = new List<Address>();
                List<AddressRouteMatrix> routeMatList = new List<AddressRouteMatrix>();

                foreach (var item in routeObject.WorkingTimes)
                {
                    var address = objectSpace.GetObject<Address>(item.Address);

                    routeAddressList.Add(address);

                } 
                routeMatList.AddRange(routeObject.WorkingTime.Address.AddressRouteMatrixies);  
                var closest = FindClosestAddress(routeObject.WorkingTime.Address, routeMatList, routeAddressList);

                

                for (int workTm = 0; workTm < routeObject.WorkingTimes.Count() - 1; workTm++)
                {  

                }

            }
        }
    }
}
