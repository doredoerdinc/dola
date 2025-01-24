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
using DevExpress.ExpressApp.Model;
using LgsLib.Base;


namespace LgsLib.Base.Web
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class WindowControllerLGSWeb : WindowController
    {
        IObjectSpace mapObjectSpace;
        IList<IMapPoint> mapPoints = new List<IMapPoint>();
        Map MapView;

        public WindowControllerLGSWeb()
        {
            InitializeComponent();
            TargetWindowType = WindowType.Main;
             // this.showOnMap.TargetObjectType = typeof(IMapsMarker);

            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ApplicationDefination.PopupSettingsInitial();
            ShowNavigationItemController showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            showNavigationItemController.CustomShowNavigationItem += showNavigationItemController_CustomShowNavigationItem;
            Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
            additionalObjectSpace = Application.CreateObjectSpace(typeof(ObjectCode));

            // Perform various tasks depending on the target Window.
        }
        void showNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        { 
            var modelNavigationItem = e.ActionArguments.SelectedChoiceActionItem.Model as IModelNavigationItem;
            if(modelNavigationItem.View.AsObjectView is IModelDetailView)
            {
                var objectSpace = Application.CreateObjectSpace();
                var navigationView = modelNavigationItem.View;
                var modelDetailView = navigationView as IModelDetailView;
                var objectType = modelDetailView.ModelClass.TypeInfo.Type; 
                var newObject= objectSpace.CreateObject(objectType);
                DetailView detailView = Application.CreateDetailView(objectSpace, modelDetailView,true,newObject); 
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                e.ActionArguments.ShowViewParameters.CreatedView = detailView;
                e.Handled = true;

            } 
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            mapObjectSpace = null;
            mapPoints = null;
            MapView = null;
            Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
            if (additionalObjectSpace != null)
            {
                additionalObjectSpace.Dispose();
                additionalObjectSpace = null;
            }
        } 

        private void HelpWindow_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ApplicationDefination.PopupSettings(80, 70);
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(TaskLGS));
            var newObject = objectSpace.CreateObject(typeof(TaskLGS)); 
            string mapViewID = Application.FindDetailViewId(typeof(TaskLGS));
            DetailView detailView = Application.CreateDetailView(objectSpace, mapViewID, true, newObject);

            detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;

            ShowViewParameters showViewParameters = new ShowViewParameters();
            //showViewParameters.Controllers.Add(controller);
            showViewParameters.CreatedView = detailView;
            showViewParameters.TargetWindow = TargetWindow.NewWindow;
            showViewParameters.Context = TemplateContext.PopupWindow;
            Application.ShowViewStrategy.ShowView(showViewParameters, new ShowViewSource(Frame, null));   
            ApplicationDefination.PopupSettingsInitial();

        }
        
        private IObjectSpace additionalObjectSpace;
        
        private void Application_ObjectSpaceCreated(Object sender, ObjectSpaceCreatedEventArgs e)
        {
            //if (e.ObjectSpace is NonPersistentObjectSpace)
            //{
            //    if (additionalObjectSpace != null)
            //    {
            //        ((NonPersistentObjectSpace)e.ObjectSpace).AdditionalObjectSpaces.Add(additionalObjectSpace);
            //    }
              
            //}
        }

        private void showOnMap_Execute(object sender, SimpleActionExecuteEventArgs e)
        { 
           
        }

        private void ShowMapView_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
        
        }

       
    }
}
