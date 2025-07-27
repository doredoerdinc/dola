using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using System;
using System.Web;

namespace dola.Module.Web
{
    partial class MapController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null; 

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
         
            this.components = new System.ComponentModel.Container();
            this.mapDistanceAddress = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.mapDistanceAddressQuantity = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.mapViewAction = new DevExpress.ExpressApp.Actions.SimpleAction(this, "MapViewAction", PredefinedCategory.Edit);
            this.MapRouteTruckAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);


            // 
            // OrderPlanedGoodsOutInTask
            // 
            this.mapDistanceAddress.Caption = "mapDistanceAddress";
            this.mapDistanceAddress.ConfirmationMessage = null;
            this.mapDistanceAddress.Id = "mapDistanceAddress";
            this.mapDistanceAddress.ToolTip = null;
            this.mapDistanceAddress.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.mapDistanceAddress_Execute);
            this.Actions.Add(this.mapDistanceAddress);

            this.mapDistanceAddress.Caption = "mapDistanceAddress";
            this.mapDistanceAddress.ConfirmationMessage = null;
            this.mapDistanceAddress.Id = "mapDistanceAddress";
            this.mapDistanceAddress.ToolTip = null;
            this.mapDistanceAddress.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.mapDistanceAddress_Execute);
            this.Actions.Add(this.mapDistanceAddress);

            

            // 
            // OrderPlanedGoodsOutInTask
            // 
            this.mapViewAction.Caption = "MapViewAction";
            this.mapViewAction.ConfirmationMessage = null;
            this.mapViewAction.Id = "MapViewAction";
            this.mapViewAction.ToolTip = null;
            this.mapViewAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.MapViewAction_Execute);


            // 
            // MapRouteTruck
            // 
            this.MapRouteTruckAction.Caption = "MapRouteTruckAction";
            this.MapRouteTruckAction.ConfirmationMessage = null;
            this.MapRouteTruckAction.Id = "mapRoutePlanTruck";
            this.MapRouteTruckAction.ToolTip = null;
            this.MapRouteTruckAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.MapRouteTruckAction_Execute);



            this.Actions.Add(this.mapViewAction);
            this.Actions.Add(this.MapRouteTruckAction);

        }
        private DevExpress.ExpressApp.Actions.SimpleAction mapDistanceAddress;
        private DevExpress.ExpressApp.Actions.SimpleAction mapDistanceAddressQuantity;
        private DevExpress.ExpressApp.Actions.SimpleAction mapViewAction;
        private DevExpress.ExpressApp.Actions.SimpleAction MapRouteTruckAction;

        #endregion

    }
}
