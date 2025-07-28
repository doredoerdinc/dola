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
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.TestScripts;

namespace LgsLib.Base.Web
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
        public MapController()
        {
            InitializeComponent();
               // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {

            base.OnActivated();
            View.ObjectTypeInfo.GetType();
        
            // Perform various tasks depending on the target View.
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated(); 
        } 
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
         
        } 
      
    }
}
