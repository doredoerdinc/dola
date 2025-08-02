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
        private void mapDistanceAddress_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var newObjectSpace = Application.CreateObjectSpace();
            List<Address> orginAddressList = new List<Address>();
            List<LocationGeo> orginLocationList = new List<LocationGeo>();
            List<LocationGeo> destinationLocationList = new List<LocationGeo>();
            String filterCriteria = null;
            if (View.CurrentObject != null)
            {
                var fromKeyValue = newObjectSpace.GetKeyValue(View.CurrentObject);
                var fromAddress = newObjectSpace.GetObjectByKey<Address>(fromKeyValue);
                var sysCodes = fromAddress.AddressRouteMatrixies
                   .Cast<AddressRouteMatrix>()
                   .Where(x => x.ToAddress != null && !string.IsNullOrWhiteSpace(x.ToAddress.SysCode))
                   .Select(x => $"'{x.ToAddress.SysCode}'")
                   .Distinct()
                   .ToList();
                //'Not [SysCode] In ('1231', '123')'
                if (sysCodes.Count > 0)
                {
                    var joinedSysCodes = string.Join(",", sysCodes);
                    string criteriaString = $"Not [SysCode] In ({joinedSysCodes})";
                    filterCriteria = CriteriaOperator.Parse(criteriaString).ToString();
                }
            
            }

            List<Address> fromAddressRequestList = View.SelectedObjects.Cast<Address>().Where(a => a != null).ToList();

            //foreach (var item in View.SelectedObjects)
            //{
            //    var keyValue = newObjectSpace.GetKeyValue(item);
            //    var addressObject = newObjectSpace.GetObjectByKey<Address>(keyValue);
            //    var locationGeo = newObjectSpace.GetObject<LocationGeo>(addressObject.LocationGeo);
            //    if (locationGeo != null)
            //    {
            //        locationGeo.IntegrationCode = keyValue.ToString();
            //        orginLocationList.Add(locationGeo);
            //    }
            //}
          
            this.ExecutePopupSimpleDynamicList<Address>(filterCriteria, (ToAddressListView, nonobjectSpace) =>
            {
            },
            (executedListView) =>
            {
                List<Address> toAddressRequestList = executedListView.SelectedObjects.Cast<Address>().Where(a => a != null).ToList();

                //foreach (var item in executedListView.SelectedObjects)
                //{
                //    var keyValue = newObjectSpace.GetKeyValue(item);
                //    var addressObject = newObjectSpace.GetObjectByKey<Address>(keyValue);
                //    var locationGeo = newObjectSpace.GetObject<LocationGeo>(addressObject.LocationGeo); 
                //    if (locationGeo != null)
                //    {
                //        destinationLocationList.Add(locationGeo);
                //    }

                //}
                if (orginLocationList.Count>0&& destinationLocationList.Count>0)
                {
                    googleService.CalculateRouteMatrix(fromAddressRequestList, toAddressRequestList, newObjectSpace);
                }
                //getCountMapMatrix(orginAddressList, newObjectSpace);
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
                orginLocationList.Clear();
                destinationLocationList.Clear();
                orginAddressList.Clear();

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
                 
                    if (findMatRoute == null||findMatRoute.EncodedPolyline==null)
                    {
                        workObjectFrom.Description = "RotaHesaplanamadı";
                    }
                    else
                    { 
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

            }
            ShowMapFrmScript();
        }
        private void mapRoutePlanCalculate_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var objectSpace = Application.CreateObjectSpace();
            ReloadMap(View);
            object firstObject = View.CurrentObject ?? View.SelectedObjects.Cast<IMapPoint>().FirstOrDefault();

            double averageSpeed = 70; // Ortalama hız (km/h)
            double totalDistance = 0; // Toplam mesafe
            TimeSpan currentTime = TimeSpan.Zero; // Başlangıç zamanı
            int totalRestTimeInMinutes = 0; // Toplam dinlenme süresi (dakika cinsinden)
            int totalDaysForArrive = 1; // Günü 1'den başlatıyoruz
            double drivingMinutes = 0; // Sürücünün toplam sürüş süresi (dakika cinsinden)
            double totalDrivingTimeMinutes = 0; // Toplam sürüş süresi (dakika cinsinden)

            bool firstRestDone = false; // İlk dinlenme süresi yapılmadı

            foreach (var routeItem in View.SelectedObjects)
            {
                var routeObjectKey = objectSpace.GetKeyValue(routeItem);
                var routeObject = objectSpace.GetObjectByKey<RoutePlanTransport>(routeObjectKey);

                if (routeObject.WorkingTimes.Count() > 1)
                {
                    List<Address> requestAddressListMat = routeObject.WorkingTimes
                        .Select(w => w.Address)
                        .Where(a => a != null)
                        .ToList();

                    googleService.CalculateRouteMatrixMissing(requestAddressListMat, objectSpace);
                    List<AddressRouteMatrix> addressRouteMatrixList = objectSpace
                        .GetObjects(typeof(AddressRouteMatrix), null)
                        .Cast<AddressRouteMatrix>()
                        .ToList();

                    var routeList = RouteCalculateRow(routeObject.WorkingTime.Address, requestAddressListMat, addressRouteMatrixList);
                    
                    currentTime = routeObject.WorkingTime.FinishTime.Value; // İlk adresin başlangıcı, 00:00
                    for (int i = 0; i < routeList.Count(); i++)
                    {
                        WorkingTime wot = routeObject.WorkingTimes.FirstOrDefault(x => x.Address.SysCode == routeList[i].Address.SysCode);
                        if (wot != null)
                        {
                            double distance = wot.AddressRouteMatrix?.DictanceMeters ?? 0; // Mesafe (metre cinsinden)
                            double distanceKm = distance / 1000; // metreyi kilometreye çevir
                            double drivingTimeMinutes = (distanceKm / averageSpeed) * 60; // Dakika cinsinden
                            TimeSpan drivingTime = TimeSpan.FromMinutes(drivingTimeMinutes);
                            currentTime = currentTime.Add(drivingTime);
                            currentTime = currentTime.Add(TimeSpan.FromMinutes(30));
                            drivingMinutes += drivingTimeMinutes; // Toplam sürüş süresi (dakika cinsinden) 

                            while (drivingMinutes >= 540)
                            {
                                // Dinlenme süresi 900 dakika (15 saat) ekle
                                totalRestTimeInMinutes += 900; // Dinlenme süresi (dakika cinsinden)
                                drivingMinutes -= 540; // 540 dakikalık sürüş dilimini tamamladık, kalan sürüş süresi
                            }
                            // Günün saat kısmını al
                            TimeSpan timeOfDay = currentTime - TimeSpan.FromDays(totalDaysForArrive); // Saat kısmını al 
                            // Zamanın Negatif Olup Olmadığını Kontrol Etme
                            if (timeOfDay < TimeSpan.Zero)
                            {
                                timeOfDay = TimeSpan.Zero; // Zaman negatifse, sıfırla
                            }
                            // `RouteRestTime`'ı hesapla ve güncelle

                            // Eğer 540 dakika (9 saat) sürüş yapıldıysa, gün sayısını artırıyoruz
                            if (drivingMinutes >= 540)
                            {
                                totalDaysForArrive++; // Bir sonraki gün için gün sayısını artırıyoruz
                                drivingMinutes = 0; // Yeni gün başlıyor, sürüş saati sıfırlanıyor
                            }
                            wot.RouteRow = routeList[i].Row;
                            wot.RouteRestTime = totalRestTimeInMinutes; // Dinlenme süresi (dakika cinsinden) 
                            wot.RoutePlanedArrivedDay = totalDaysForArrive; // Gün sayısını hesapla
                            wot.RoutePlanedArivedTime = currentTime;//timeOfDay; // Planlanan varış zamanını saat, dakika, saniye olarak ata 
                        } 
                    }
                }

              
                routeObject.RouteTotalDuration = currentTime.TotalMinutes; 
                routeObject.RouteTotalKm = totalDistance / 1000; // Kilometreyi km olarak gösterelim 
               // routeObject.RouteTotalRestTime = totalRestTimeInMinutes; // Dinlenme süresi (dakika cinsinden) 
                routeObject.RouteTotalStation = routeObject.WorkingTimes.Count(); 
                objectSpace.CommitChanges();
            }
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

        public class RouteRowAddress
        {
            public Address Address { get; set; }
            public int Row { get; set; }
        }

        public List<RouteRowAddress> RouteCalculateRow(
          Address startAddress,
          List<Address> storeAddresses,
          List<AddressRouteMatrix> routeMatrix)
        {
            var routeRowAddress = new List<RouteRowAddress>();
            var remainingStores = new List<Address>(storeAddresses);
            var currentAddress = startAddress;
            int routeCounter = 1;

            var findStartAdress = storeAddresses.Where(x => x.SysCode == startAddress.SysCode).FirstOrDefault();

            if (findStartAdress != null)
            {
                routeRowAddress.Add(new RouteRowAddress
                {
                    Address = findStartAdress,
                    Row = 0
                });
            }

            while (remainingStores.Any())
            {
                var nextRoute = routeMatrix
                    .Where(r => r.FromAddress.SysCode == currentAddress.SysCode
                                && r.ToAddress != null
                                && remainingStores.Any(a => a.SysCode == r.ToAddress.SysCode)
                                && r.ToAddress.SysCode != startAddress.SysCode) // Başlangıç adresini alma
                    .OrderBy(r => r.DictanceMeters)
                    .FirstOrDefault(); 
                if (nextRoute == null)
                    break; 
                var nextAddress = nextRoute.ToAddress; 
                // Ek güvenlik: baştan gelen adresi listeye ekleme
                if (nextAddress.SysCode != startAddress.SysCode)
                {
                    routeRowAddress.Add(new RouteRowAddress
                    {
                        Address = nextAddress,
                        Row = routeCounter++
                    });
                } 
                remainingStores.RemoveAll(a => a.SysCode == nextAddress.SysCode);
                currentAddress = nextAddress;
            } 
            return routeRowAddress;
        }


    }
}
