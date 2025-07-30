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
            this.mapViewAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.mapRouteTruckAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.mapRoutePlanCalculate = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // mapDistanceAddress
            // 
            this.mapDistanceAddress.Caption = "mapDistanceAddress";
            this.mapDistanceAddress.ConfirmationMessage = null;
            this.mapDistanceAddress.Id = "mapDistanceAddress";
            this.mapDistanceAddress.ToolTip = null;
            this.mapDistanceAddress.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.mapDistanceAddress_Execute);
            // 
            // mapDistanceAddressQuantity
            // 
            this.mapDistanceAddressQuantity.Caption = null;
            this.mapDistanceAddressQuantity.ConfirmationMessage = null;
            this.mapDistanceAddressQuantity.Id = "ff97bf55-2ee2-4346-a218-a6d2f3fdc78f";
            this.mapDistanceAddressQuantity.ToolTip = null;
            // 
            // mapViewAction
            // 
            this.mapViewAction.Caption = "MapViewAction";
            this.mapViewAction.Category = "Edit";
            this.mapViewAction.ConfirmationMessage = null;
            this.mapViewAction.Id = "MapViewAction";
            this.mapViewAction.ToolTip = null;
            this.mapViewAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.MapViewAction_Execute);
            // 
            // mapRouteTruckAction
            // 
            this.mapRouteTruckAction.Caption = "mapRouteTruckAction";
            this.mapRouteTruckAction.ConfirmationMessage = null;
            this.mapRouteTruckAction.Id = "mapRoutePlanTruck";
            this.mapRouteTruckAction.ToolTip = null;
            this.mapRouteTruckAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.MapRouteTruckAction_Execute);
            // 
            // mapRoutePlanCalculate
            // 
            this.mapRoutePlanCalculate.Caption = "mapRoutePlanCalculate";
            this.mapRoutePlanCalculate.Category = "Edit";
            this.mapRoutePlanCalculate.ConfirmationMessage = null;
            this.mapRoutePlanCalculate.Id = "mapRoutePlanCalculate";
            this.mapRoutePlanCalculate.ToolTip = null;
            this.mapRoutePlanCalculate.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.routePlanCalculate_Execute);
            // 
            // MapController
            // 
            this.Actions.Add(this.mapDistanceAddress);
            this.Actions.Add(this.mapDistanceAddressQuantity);
            this.Actions.Add(this.mapViewAction);
            this.Actions.Add(this.mapRouteTruckAction);
            this.Actions.Add(this.mapRoutePlanCalculate);

        }
        private DevExpress.ExpressApp.Actions.SimpleAction mapDistanceAddress;
        private DevExpress.ExpressApp.Actions.SimpleAction mapDistanceAddressQuantity;
        private DevExpress.ExpressApp.Actions.SimpleAction mapViewAction;
        private DevExpress.ExpressApp.Actions.SimpleAction mapRouteTruckAction;

        #endregion

        private SimpleAction mapRoutePlanCalculate;
    }
}
