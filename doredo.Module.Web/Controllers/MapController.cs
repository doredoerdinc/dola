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
//using System.Web.Script.Serialization;


namespace dola.Module.Web
{ 
    public static class MapStatic
    {
        static List<MapPointLGS> _StaticPointList;
        public static List<MapPointLGS> StaticPointList
        {
            get { return _StaticPointList; }
            set { _StaticPointList = value; }
        }

        static List<MapPointOrderTransfer> _StaticMapPointOrderTransferList;
        public static List<MapPointOrderTransfer> StaticMapPointOrderTransferList
        {
            get { return _StaticMapPointOrderTransferList; }
            set { _StaticMapPointOrderTransferList = value; }
        }

        static List<GoogleRouteDraw> _StaticRouteDrawList;
        static public List<GoogleRouteDraw> StaticRouteDrawList
        {
            get { return _StaticRouteDrawList; }
            set { _StaticRouteDrawList = value; }
        }

        public static List<PolyLatLng> DecodePolylinePoints(string encodedPoints)
        {
            if (encodedPoints == null || encodedPoints == "") return null;
            List<PolyLatLng> poly = new List<PolyLatLng>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            try
            {
                while (index < polylinechars.Length)
                {
                    // calculate next latitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length)
                        break;

                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                    //calculate next longitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;

                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    PolyLatLng p = new PolyLatLng();
                    p.lat = Convert.ToDouble(currentLat) / 100000.0;
                    p.lng = Convert.ToDouble(currentLng) / 100000.0;
                    poly.Add(p);
                }
            }
            catch (Exception ex)
            {
                // logo it
            }
            return poly;
        }


    }

    public class GoogleRouteDraw
    {

        String _SysCode;
        public String SysCode
        {
            get { return _SysCode; }
            set { _SysCode = value; }
        }



        double? _DictanceMeters;
        public double? DictanceMeters
        {
            get { return _DictanceMeters; }
            set { _DictanceMeters = value; }
        }

        string _Duration;
        public string Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }


        string _StaticDuration;
        public string StaticDuration
        {
            get { return _StaticDuration; }
            set { _StaticDuration = value; }
        }

        string _EncodedPolyline; 
        public string EncodedPolyline
        {
            get { return _EncodedPolyline; }
            set { _EncodedPolyline = value; }
        }

    }

    public class JSRouteSettings
    {
        public string mode;
        public double opacity;
        public int weight;
        public string color;
        public List<MapPoint> locations;
    }
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MapController : ViewController
    {  
        public MapController()
        {
            InitializeComponent();
            
            SimpleAction mapViewAction = new SimpleAction(this, "MapViewAction", PredefinedCategory.Edit);
            SimpleAction mapViewTripCargoAction = new SimpleAction(this, "mapViewTripCargoAction", PredefinedCategory.Edit);
            mapViewAction.Execute += MapViewAction_Execute;
            mapViewTripCargoAction.Execute += MapViewTripCargoAction_Execute;
            mapViewTripCargoAction.TargetObjectType = typeof(TripCargo);
            mapDistanceAddress.TargetObjectType= typeof(Address);
            mapDistanceAddressQuantity.TargetObjectType = typeof(Address);

            mapViewAction.SetClientScript(CallMapView());
            mapViewTripCargoAction.SetClientScript(CallMapView());
        } 
        private static string CallMapView()
        {
            return @"
                            let params = `scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,
width=600,height=300,left=100,top=100`;
                            function openOnce(url, target){
                                       var winref = window.open('','test',params);
                                        if (winref.location.href === 'about:blank')
                                        {
                                            winref.location.href = url;
                                        }
                             
                                        return winref;
                                    }
                                openOnce('../map/MapView.aspx', 'MyWindowName');
                            ";
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
            getCountMapMatrix(orginAddress, newObjectSpace);
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
                var criteria = CriteriaOperator.Parse("IntegrationCode =?", keyValue);
                var locationGeo = newObjectSpace.FindObject<LocationGeo>(criteria);
                if (locationGeo != null)
                {
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
                    var criteria = CriteriaOperator.Parse("IntegrationCode =?", keyValue);
                  
                    var locationGeo = newObjectSpace.FindObject<LocationGeo>(criteria);
                  
                    
                    if (locationGeo != null)
                    {
                        destinationLocationList.Add(locationGeo);
                    }
                    
                }
                if(orginLocationList.Count>0&& destinationLocationList.Count>0)
                {
                    googleService.GoogleComputeRoutes(orginLocationList, destinationLocationList,newObjectSpace);
                }
                getCountMapMatrix(orginAddressList, newObjectSpace);
                newObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();

            });  

        }

        private void getCountMapMatrix(List<Address> adressList, IObjectSpace newObjectSpace)
        {
            foreach (var item in adressList)
            {
                var criteria = CriteriaOperator.Parse("FromAddress.SysCode =?", item.SysCode);
                var getMatrixMap = newObjectSpace.GetObjects<AddressRouteMatrix>(criteria);
                item.DistanceMaptoAddressQuantity = getMatrixMap.Count();
            }
        }

        private void MapViewTripCargoAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var points = new List<MapPointLGS>();
            var objectSpace = Application.CreateObjectSpace();
            var mapModelObjectSpace = Application.CreateObjectSpace(typeof(Map));
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
               , new BinaryOperator("ToAddressCode.SysCode", ord.ToAddress.SysCode, BinaryOperatorType.Equal));

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
                        var mapTransfer = mapModelObjectSpace.CreateObject<MapPointOrderTransfer>();
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
                        var mapTransfer = mapModelObjectSpace.CreateObject<MapPointOrderTransfer>();
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
            var points = new List<MapPointLGS>();
            var objectSpace = Application.CreateObjectSpace();
            var mapModelObjectSpace = Application.CreateObjectSpace(typeof(Map));
           
            foreach (var obj in View.SelectedObjects)
            {
                var order = obj as Order;

                var mapPoint1 = mapModelObjectSpace.CreateObject<MapPointLGS>();
                mapPoint1.Key = order.FromAddress.SysCode;
                mapPoint1.Latitude = order.FromAddress.Latitude;
                mapPoint1.Longitude = order.FromAddress.Longitude;
                mapPoint1.Title = order.FromAddress.Name;
                MapStatic.StaticPointList.Add(mapPoint1);

                var mapPoint2 = mapModelObjectSpace.CreateObject<MapPointLGS>();
                mapPoint2.Key = order.SysCode;
                mapPoint2.Latitude = order.ToAddress.Latitude;
                mapPoint2.Longitude = order.ToAddress.Longitude;
                mapPoint2.Title = order.ToAddress.Name;
                MapStatic.StaticPointList.Add(mapPoint2); 
            } 
        } 
        protected override void OnActivated()
        { 
            base.OnActivated();
            MapStatic.StaticPointList = new List<MapPointLGS>();
            if (MapStatic.StaticMapPointOrderTransferList == null)
            {
                MapStatic.StaticMapPointOrderTransferList = new List<MapPointOrderTransfer>();
            }
            
        } 
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();  
        } 
        protected override void OnDeactivated()
        {
             base.OnDeactivated();
          
        }  
       
    }
}
