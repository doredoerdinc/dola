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

namespace Lgslib.Base.Web
{

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

        IObjectSpace mapObjectSpace;
        IList<IMapPoint> mapPoints = new List<IMapPoint>();
        WebMapsListEditor webMapsListEditor = null;


        Map MapView;
        MapViewer mapViewer;

        private IObjectSpace additionalObjectSpace;

        public MapController()
        {
            InitializeComponent();
            ShowOnMap.TargetObjectType = typeof(IMapPoint);
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
            if (View.Model.Id == "MapPointLGS_ListView")
            {


                webMapsListEditor = ((ListView)View).Editor as WebMapsListEditor;
                if (webMapsListEditor != null)
                {
                    mapViewer = webMapsListEditor.MapViewer;
                }

                if (webMapsListEditor != null)
                {
                    webMapsListEditor.MapViewer.ClientSideEvents.Customize = GetCustomizeRouteScript();
                }
            }

        }
  
       
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            mapObjectSpace = null;
            mapPoints = null;
            MapView = null; 
            if (additionalObjectSpace != null)
            {
                additionalObjectSpace.Dispose();
                additionalObjectSpace = null;
            }
        }

  

        private void ShowOnMap_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var points = new List<MapPointLGS>(); 
            var objectSpace = Application.CreateObjectSpace();
            var mapModelObjectSpace = Application.CreateObjectSpace(typeof(Map));
            foreach (var obj in View.SelectedObjects)
            {
                var keyValue = objectSpace.GetKeyValue(obj);
                var objectType = objectSpace.GetObjectType(obj);
                var cobject = objectSpace.GetObjectByKey(objectType, keyValue);
                if (cobject is IMapMultiplePoint)
                {
                    var route = cobject as IMapMultiplePoint;

                    foreach (var point in route.Points)
                    {
                        var mapPoint = mapModelObjectSpace.CreateObject<MapPointLGS>();
                        mapPoint.Key = point.Key;
                        mapPoint.Latitude = point.Latitude;
                        mapPoint.Longitude = point.Longitude;
                        mapPoint.Title = point.Title;
                        points.Add(mapPoint); 
                    } 
                }
                else if (cobject is IMapPoint)
                { 
                    var obJectPoint = cobject as IMapPoint; 

                    var mapPoint = mapModelObjectSpace.CreateObject<MapPointLGS>();
                    mapPoint.Key = obJectPoint.Key;
                    mapPoint.Latitude = obJectPoint.Latitude;
                    mapPoint.Longitude = obJectPoint.Longitude;
                    mapPoint.Title = obJectPoint.Title; 
                    points.Add(mapPoint);  
                }
                 
            
            }

            CheckMapview();
            MapView.Points = points;
        //    MapView.ObjectSpace.Refresh();
       
            GetRoutes(); 

        } 
         private void CheckMapview()
        { 
            if (mapObjectSpace == null || MapView == null)
            {
                mapObjectSpace = Application.CreateObjectSpace(typeof(Map));
                MapView = mapObjectSpace.CreateObject<Map>();

            }

            string mapViewID = Application.FindDetailViewId(typeof(Map));
            DetailView detailView = Application.CreateDetailView(mapObjectSpace, mapViewID, false, MapView);

            detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;

            ShowViewParameters showViewParameters = new ShowViewParameters();
            //showViewParameters.Controllers.Add(controller);
            showViewParameters.CreatedView = detailView;
            showViewParameters.TargetWindow = TargetWindow.NewWindow;
            showViewParameters.Context = TemplateContext.PopupWindow;
            Application.ShowViewStrategy.ShowView(showViewParameters, new ShowViewSource(null, null)); 
        }

        private string GetCustomizeScript()
        {
            var jsSerializer = new JavaScriptSerializer();
            string routes = jsSerializer.Serialize(GetRoutes());
            return string.Format(@"function(sender, map) {{  
                    var customRoutes = {0};  
                    map.option('routes', customRoutes);  
                }}", routes);
        }
        private string GetCustomizeRouteScript()
        {
            var jsSerializer = new JavaScriptSerializer();
            string routes = jsSerializer.Serialize(GetRoutes());
            return string.Format(@"function(sender, map) {{  
                    var customRoutes = {0};  
                    map.option('routes', customRoutes);  
                }}", routes);
        }

        private string GetCustomizePolyLineScript()
        {
            var jsSerializer = new JavaScriptSerializer();
            string polylines = jsSerializer.Serialize(mapPoints);
            return string.Format(
                    @"function(sender, map) {{
                    map.on('ready', function(e) {{
                        var googleMap = e.originalMap;
                        var flightPlanCoordinates = {0};
                        var flightPath = new google.maps.Polyline({{
                        path: flightPlanCoordinates,
                        strokeColor: '#FF0000',
                        strokeOpacity: 1.0,
                        strokeWeight: 2
                        }});

                        flightPath.setMap(googleMap);
                    }});
                }}", polylines);
        }

        private IList<JSRouteSettings> GetRoutes()
        {
            JSRouteSettings route1 = new JSRouteSettings();
            route1.weight = 6;
            route1.color = "blue";
            route1.opacity = 0.5;
            route1.mode = "driving";
            route1.locations = new List<MapPoint>();
            route1.locations.Add(new MapPoint(40.782500, -73.966111));
            route1.locations.Add(new MapPoint(40.755833, -73.986389));
            JSRouteSettings route2 = new JSRouteSettings();
            route2.weight = 8;
            route2.color = "red";
            route2.opacity = 0.5;
            route2.mode = "driving";
            route2.locations = new List<MapPoint>();
            route2.locations.Add(new MapPoint(40.753889, -73.981389));
            route2.locations.Add(new MapPoint(40.723889, -73.951389));
            IList<JSRouteSettings> routes = new List<JSRouteSettings>();
            routes.Add(route1);
            routes.Add(route2);
            return routes;
        }
    }
}
