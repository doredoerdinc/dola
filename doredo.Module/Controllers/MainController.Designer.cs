
namespace dola.Module
{
    partial class MainController
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
            this.OrderPlanedGoodsOutInTask = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.TaskAssignedOrderGoodsIn = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.LabelCreateFromOrderLine = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.TaskAssigned = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.TemplateLabelCreate = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.GenerateTaskForItemAcceptProcess = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.OrderPickReservation = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.GenerateTaskForItemPickProcess = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.GenerateTaskForVehicleLoadProcess = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.GenerateTaskForStockControlProcess = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.GenerateTaskForLocationItemReplenanchmentProcess = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.TaskAssignedStockControl = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.ItemTempToSTock = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.DeleteTaskStepTransactionTemplate = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.DeleteStockControlStep = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.GenerateTaskForItemPackingProcess = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.OrderCreateEmptyLabel = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.TaskAssignedTrip = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.OrderItemExtoMap = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.OrderItemPrint = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.OrderItemImport = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            
            // 
            // OrderPlanedGoodsOutInTask
            // 
            this.OrderPlanedGoodsOutInTask.Caption = "Order Planed Goods Out In Task";
            this.OrderPlanedGoodsOutInTask.ConfirmationMessage = null;
            this.OrderPlanedGoodsOutInTask.Id = "OrderPlanedGoodsOutInTask";
            this.OrderPlanedGoodsOutInTask.ToolTip = null;
            this.OrderPlanedGoodsOutInTask.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.OrderPlanedGoodsOutInTask_Execute);
            // 
            // TaskAssignedOrderGoodsIn
            // 
            this.TaskAssignedOrderGoodsIn.Caption = "TaskAssignedOrderGoodsIn";
            this.TaskAssignedOrderGoodsIn.ConfirmationMessage = null;
            this.TaskAssignedOrderGoodsIn.Id = "TaskAssignedOrderGoodsIn";
            this.TaskAssignedOrderGoodsIn.ToolTip = null;
            this.TaskAssignedOrderGoodsIn.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.TaskAssignedOrderGoodsIn_Execute);
            // 
            // LabelCreateFromOrderLine
            // 
            this.LabelCreateFromOrderLine.Caption = "Label Create From OrderLine";
            this.LabelCreateFromOrderLine.ConfirmationMessage = null;
            this.LabelCreateFromOrderLine.Id = "LabelCreateFromOrderLine";
            this.LabelCreateFromOrderLine.ToolTip = null;
            this.LabelCreateFromOrderLine.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.LabelCreateFromOrderLine_Execute);
            // 
            // TaskAssigned
            // 
            this.TaskAssigned.Caption = "Task Assigned";
            this.TaskAssigned.Category = "View";
            this.TaskAssigned.ConfirmationMessage = null;
            this.TaskAssigned.Id = "TaskAssigned";
            this.TaskAssigned.ToolTip = null;
            this.TaskAssigned.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AssignTask_Execute);
            // 
            // TemplateLabelCreate
            // 
            this.TemplateLabelCreate.Caption = "Template Container Create";
            this.TemplateLabelCreate.ConfirmationMessage = null;
            this.TemplateLabelCreate.Id = "TemplateLabelCreate";
            this.TemplateLabelCreate.ToolTip = null;
            // 
            // GenerateTaskForItemAcceptProcess
            // 
            this.GenerateTaskForItemAcceptProcess.Caption = "Generate Task Item AcceptProcess";
            this.GenerateTaskForItemAcceptProcess.ConfirmationMessage = null;
            this.GenerateTaskForItemAcceptProcess.Id = "GenerateTaskForItemAcceptProcess";
            this.GenerateTaskForItemAcceptProcess.ToolTip = null;
            this.GenerateTaskForItemAcceptProcess.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GenerateTaskForItemAcceptProcess_Execute);
            // 
            // OrderPickReservation
            // 
            this.OrderPickReservation.Caption = "OrderPickReservation";
            this.OrderPickReservation.ConfirmationMessage = null;
            this.OrderPickReservation.Id = "OrderPickReservation";
            this.OrderPickReservation.ToolTip = null;
            this.OrderPickReservation.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.OrderPickReservation_Execute);
            // 
            // GenerateTaskForItemPickProcess
            // 
            this.GenerateTaskForItemPickProcess.Caption = "Generate Task ForItem PickProcess";
            this.GenerateTaskForItemPickProcess.ConfirmationMessage = null;
            this.GenerateTaskForItemPickProcess.Id = "GenerateTaskForItemPickProcess";
            this.GenerateTaskForItemPickProcess.ToolTip = null;
            this.GenerateTaskForItemPickProcess.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GenerateTaskForItemPickProcess_Execute);
            // 
            // GenerateTaskForVehicleLoadProcess
            // 
            this.GenerateTaskForVehicleLoadProcess.Caption = "Generate Task For Vehicle LoadProcess";
            this.GenerateTaskForVehicleLoadProcess.ConfirmationMessage = null;
            this.GenerateTaskForVehicleLoadProcess.Id = "GenerateTaskForVehicleLoadProcess";
            this.GenerateTaskForVehicleLoadProcess.ToolTip = null;
            this.GenerateTaskForVehicleLoadProcess.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GenerateTaskForVehicleLoadProcess_Execute);
            // 
            // GenerateTaskForStockControlProcess
            // 
            this.GenerateTaskForStockControlProcess.Caption = "Generate Task For StockItem Control";
            this.GenerateTaskForStockControlProcess.ConfirmationMessage = null;
            this.GenerateTaskForStockControlProcess.Id = "GenerateTaskForStockControlProcess";
            this.GenerateTaskForStockControlProcess.ToolTip = null;
            this.GenerateTaskForStockControlProcess.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GenerateTaskForLocationItemCheckProcess_Execute);
            // 
            // GenerateTaskForLocationItemReplenanchmentProcess
            // 
            this.GenerateTaskForLocationItemReplenanchmentProcess.Caption = "Generate Task For LocationStock Replenanchment Process";
            this.GenerateTaskForLocationItemReplenanchmentProcess.ConfirmationMessage = null;
            this.GenerateTaskForLocationItemReplenanchmentProcess.Id = "GenerateTaskForLocationItemReplenanchmentProcess";
            this.GenerateTaskForLocationItemReplenanchmentProcess.ToolTip = null;
            this.GenerateTaskForLocationItemReplenanchmentProcess.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GenerateTaskForLocationReplenanchmentProcess_Execute);
            // 
            // TaskAssignedStockControl
            // 
            this.TaskAssignedStockControl.Caption = "Task Assigned StockItem Control";
            this.TaskAssignedStockControl.ConfirmationMessage = null;
            this.TaskAssignedStockControl.Id = "TaskAssignedStockControl";
            this.TaskAssignedStockControl.ToolTip = null;
            this.TaskAssignedStockControl.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.TaskAssignedStockControl_Execute);
            // 
            // ItemTempToSTock
            // 
            this.ItemTempToSTock.Caption = "Item Temp To STock";
            this.ItemTempToSTock.ConfirmationMessage = null;
            this.ItemTempToSTock.Id = "ItemTempToSTock";
            this.ItemTempToSTock.ToolTip = null;
            this.ItemTempToSTock.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ItemTempToSTock_Execute);
            // 
            // DeleteTaskStepTransactionTemplate
            // 
            this.DeleteTaskStepTransactionTemplate.Caption = "Delete Task Step Transaction Template";
            this.DeleteTaskStepTransactionTemplate.ConfirmationMessage = null;
            this.DeleteTaskStepTransactionTemplate.Id = "DeleteTaskStepTransactionTemplate";
            this.DeleteTaskStepTransactionTemplate.ToolTip = null;
            this.DeleteTaskStepTransactionTemplate.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeleteTaskStepTransactionTemplate_Execute);
            // 
            // DeleteStockControlStep
            // 
            this.DeleteStockControlStep.Caption = "Delete StockItem Control Step";
            this.DeleteStockControlStep.ConfirmationMessage = null;
            this.DeleteStockControlStep.Id = "DeleteStockControlStep";
            this.DeleteStockControlStep.ToolTip = null;
            this.DeleteStockControlStep.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeleteStockControlStep_Execute);
            // 
            // GenerateTaskForItemPackingProcess
            // 
            this.GenerateTaskForItemPackingProcess.Caption = "Generate Task For Item Packing Process";
            this.GenerateTaskForItemPackingProcess.ConfirmationMessage = null;
            this.GenerateTaskForItemPackingProcess.Id = "GenerateTaskForItemPackingProcess";
            this.GenerateTaskForItemPackingProcess.ToolTip = null;
            this.GenerateTaskForItemPackingProcess.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GenerateTaskForItemPackingProcess_Execute);
            // 
            // OrderCreateEmptyLabel
            // 
            this.OrderCreateEmptyLabel.Caption = "Order Create Empty Label";
            this.OrderCreateEmptyLabel.ConfirmationMessage = null;
            this.OrderCreateEmptyLabel.Id = "OrderCreateEmptyLabel";
            this.OrderCreateEmptyLabel.ToolTip = null;
            // 
            // TaskAssignedTrip
            // 
            this.TaskAssignedTrip.Caption = "Task Assigned Trip";
            this.TaskAssignedTrip.ConfirmationMessage = null;
            this.TaskAssignedTrip.Id = "TaskAssignedTrip";
            this.TaskAssignedTrip.ToolTip = null;
            this.TaskAssignedTrip.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.TaskAssignedTrip_Execute);
            // 
            // OrderItemExtoMap
            // 
            this.OrderItemExtoMap.Caption = "OrderItemExtoMap";
            this.OrderItemExtoMap.ConfirmationMessage = null;
            this.OrderItemExtoMap.Id = "OrderItemExtoMap";
            this.OrderItemExtoMap.ToolTip = null;
            this.OrderItemExtoMap.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.OrderItemMap_Execute);
            // 
            // OrderItemPrint
            // 
            this.OrderItemPrint.Caption = "OrderItemPrint";
            this.OrderItemPrint.ConfirmationMessage = null;
            this.OrderItemPrint.Id = "OrderItemPrint";
            this.OrderItemPrint.ToolTip = null;
            this.OrderItemPrint.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.OrderOneToOnePrint_Execute);
            // 
            // OrderItemImport
            // 
            this.OrderItemImport.Caption = "OrderItemImport";
            this.OrderItemImport.ConfirmationMessage = null;
            this.OrderItemImport.Id = "OrderItemImport";
            this.OrderItemImport.ToolTip = null;
            this.OrderItemImport.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.OrderItemImport_Execute);
            
            // 
            // MainController
            // 
            this.Actions.Add(this.OrderPlanedGoodsOutInTask);
            this.Actions.Add(this.TaskAssignedOrderGoodsIn);
            this.Actions.Add(this.LabelCreateFromOrderLine);
            this.Actions.Add(this.TaskAssigned);
            this.Actions.Add(this.TemplateLabelCreate);
            this.Actions.Add(this.GenerateTaskForItemAcceptProcess);
            this.Actions.Add(this.OrderPickReservation);
            this.Actions.Add(this.GenerateTaskForItemPickProcess);
            this.Actions.Add(this.GenerateTaskForVehicleLoadProcess);
            this.Actions.Add(this.GenerateTaskForStockControlProcess);
            this.Actions.Add(this.GenerateTaskForLocationItemReplenanchmentProcess);
            this.Actions.Add(this.TaskAssignedStockControl);
            this.Actions.Add(this.ItemTempToSTock);
            this.Actions.Add(this.DeleteTaskStepTransactionTemplate);
            this.Actions.Add(this.DeleteStockControlStep);
            this.Actions.Add(this.GenerateTaskForItemPackingProcess);
            this.Actions.Add(this.OrderCreateEmptyLabel);
            this.Actions.Add(this.TaskAssignedTrip);
            this.Actions.Add(this.OrderItemExtoMap);
            this.Actions.Add(this.OrderItemPrint);
            this.Actions.Add(this.OrderItemImport);
           

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction OrderPlanedGoodsOutInTask;
        private DevExpress.ExpressApp.Actions.SimpleAction TaskAssignedOrderGoodsIn;
        private DevExpress.ExpressApp.Actions.SimpleAction LabelCreateFromOrderLine;
        private DevExpress.ExpressApp.Actions.SimpleAction OrderPickReservation;
        private DevExpress.ExpressApp.Actions.SimpleAction TaskAssigned;
        private DevExpress.ExpressApp.Actions.SimpleAction TemplateLabelCreate;  

        private DevExpress.ExpressApp.Actions.SimpleAction GenerateTaskForItemPickProcess; 
        private DevExpress.ExpressApp.Actions.SimpleAction GenerateTaskForVehicleLoadProcess;
        private DevExpress.ExpressApp.Actions.SimpleAction GenerateTaskForItemAcceptProcess;
        private DevExpress.ExpressApp.Actions.SimpleAction GenerateTaskForStockControlProcess;
        private DevExpress.ExpressApp.Actions.SimpleAction GenerateTaskForLocationItemReplenanchmentProcess;
        private DevExpress.ExpressApp.Actions.SimpleAction TaskAssignedStockControl;
        private DevExpress.ExpressApp.Actions.SimpleAction ItemTempToSTock;
        private DevExpress.ExpressApp.Actions.SimpleAction DeleteTaskStepTransactionTemplate;
        private DevExpress.ExpressApp.Actions.SimpleAction DeleteStockControlStep;
        private DevExpress.ExpressApp.Actions.SimpleAction GenerateTaskForItemPackingProcess;
        private DevExpress.ExpressApp.Actions.SimpleAction OrderCreateEmptyLabel;
        private DevExpress.ExpressApp.Actions.SimpleAction TaskAssignedTrip;
        private DevExpress.ExpressApp.Actions.SimpleAction OrderItemExtoMap;
        private DevExpress.ExpressApp.Actions.SimpleAction OrderItemPrint;
        private DevExpress.ExpressApp.Actions.SimpleAction OrderItemImport;
    
    }
}
