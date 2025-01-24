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
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace LgsLib.Base.Web
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DetailViewControllerLGS : ViewController<DetailView>
    {
        public DetailViewControllerLGS()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            foreach (ASPxLookupPropertyEditor lookupProperty in View.GetItems<ASPxLookupPropertyEditor>())
            {
                lookupProperty.ControlCreated += LookupProperty_ControlCreated;
            }
            
        }

        private void LookupProperty_ControlCreated(object sender, EventArgs e)
        {
            var lookup = sender as ASPxLookupPropertyEditor;
            var lkview = lookup.WebLookupEditorHelper;
            if (View.ObjectSpace.IsNewObject(View.CurrentObject)) 
            { 
                if (lkview != null)
                {
                    if(lkview.LookupListViewModel.Criteria!=null)
                    {
                        lkview.LookupListViewModel.Criteria = "1=2";
                    }
                } 
            }
            else {
                if (lkview != null) {
                    if(lkview.LookupListViewModel.Criteria == "1=2")
                    {
                        lkview.LookupListViewModel.Criteria = "";
                    }
                    
                }
            }
            
        }

        private void lookupView_CurentObjectChange(object sender, EventArgs e)
        { 
        }

        private void ShowNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
             
        }
       

        // Perform various tasks depending on the target View.
    
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
    }
}
