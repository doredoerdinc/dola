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
using LgsLib.Base;

namespace Lgslib.Base.Web
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MapViewController : ViewController
    {
        IObjectSpace mapObjectSpace;
        public static IList<IMapPoint> mapPoints;
        public static IList<IMapMultiplePoint> mapMultiplePoint;
        public static Map mapView;
       

        public MapViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            if(View.Id=="MapView")
            {
                mapView = View.CurrentObject as Map;
            }
            mapPoints = new List<IMapPoint>();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            mapObjectSpace = null;
            mapPoints = null;
            mapView = null; 
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        public IEnumerable<ActionBase> GetActions()
        {
            var controllers = Frame.Controllers.Values as System.Collections.Generic.List<DevExpress.ExpressApp.Controller>;
            return controllers.SelectMany(x => x.Actions.AsEnumerable());
        }

        private void CheckMapview()
        {
            if (mapView == null)
            {
                var execAction = GetActions().SingleOrDefault(x => x.Model.Id == "ShowMapView");
                if (execAction != null)
                {
                    var simpleAction = execAction as SimpleAction; 
                    simpleAction.DoExecute(); 
                }
            }
        }

        private void ShowOnMap_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            CheckMapview();
            var points = new List<MapPoint>();

            var objectSpace = Application.CreateObjectSpace();
            var mapModelObjectSpace = Application.CreateObjectSpace(typeof(Map));
            foreach (var mpo in View.SelectedObjects)
            {
                var keyValue = objectSpace.GetKeyValue(mpo);
                var objectType = objectSpace.GetObjectType(mpo);
                var cobject = objectSpace.GetObjectByKey(objectType, keyValue);
                var obJectPoint = cobject as IMapPoint;

                var mapPoint = mapModelObjectSpace.CreateObject<MapPoint>();
                mapPoint.Key = obJectPoint.Key;
                mapPoint.Latitude = obJectPoint.Latitude;
                mapPoint.Longitude = obJectPoint.Longitude;
                mapPoint.Title = obJectPoint.Title;
                points.Add(mapPoint);
            }
            mapView.Points.AddRange(points); 
        }
    }
}
