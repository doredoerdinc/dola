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


namespace LgsLib.Base.Win
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class WindowControllerLgs : WindowController
    {
        public WindowControllerLgs()
        {
            InitializeComponent();
            TargetWindowType = WindowType.Main;
           
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ShowNavigationItemController showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            showNavigationItemController.CustomShowNavigationItem += showNavigationItemController_CustomShowNavigationItem;
        }
        void showNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            var modelNavigationItem = e.ActionArguments.SelectedChoiceActionItem.Model as IModelNavigationItem;
            if (modelNavigationItem.View.AsObjectView is IModelDetailView)
            {
                var navigationView = modelNavigationItem.View;
                var modelDetailView = navigationView as IModelDetailView;
                var objectType = modelDetailView.ModelClass.TypeInfo.Type;
                IObjectSpace objectSpace = Application.CreateObjectSpace(objectType);
                var newObject = objectSpace.CreateObject(objectType);
                DetailView detailView = Application.CreateDetailView(objectSpace, modelDetailView, true, newObject);
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                e.ActionArguments.ShowViewParameters.CreatedView = detailView;
                e.Handled = true;

            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
