namespace dola.Module.Controllers
{
    partial class MainDashBoardController
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
            this.RelationVehicleOrder = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.RelationTripOrder = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // RelationVehicleOrder
            // 
            this.RelationVehicleOrder.Caption = "Relation Vehicle Order";
            this.RelationVehicleOrder.ConfirmationMessage = null;
            this.RelationVehicleOrder.Id = "RelationVehicleOrder";
            this.RelationVehicleOrder.ToolTip = null;
            this.RelationVehicleOrder.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.RelationVehicleOrder_Execute);
            // 
            // RelationVehicleOrder
            // 
            this.RelationTripOrder.Caption = "Relation Trip Order";
            this.RelationTripOrder.ConfirmationMessage = null;
            this.RelationTripOrder.Id = "Relation Trip Order";
            this.RelationTripOrder.ToolTip = null;
            this.RelationTripOrder.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.RelationTripOrder_Execute);

            // 
            // MainDashBoardController
            // 
            this.Actions.Add(this.RelationVehicleOrder);
            this.Actions.Add(this.RelationTripOrder);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction RelationVehicleOrder;
        private DevExpress.ExpressApp.Actions.SimpleAction RelationTripOrder;
    }
}
