namespace LgsLib.Base.Web
{
    partial class WindowControllerLGSWeb
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
            this.HelpWindow = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.showOnMap = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.ShowMapView = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // HelpWindow
            // 
            this.HelpWindow.Caption = "Help Window";
            this.HelpWindow.Category = "Notifications";
            this.HelpWindow.ConfirmationMessage = null;
            this.HelpWindow.Id = "HelpWindow";
            this.HelpWindow.ToolTip = null;
            this.HelpWindow.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.HelpWindow_Execute);
            // 
            // showOnMap
            // 
            this.showOnMap.Caption = "show On Map";
            this.showOnMap.ConfirmationMessage = null;
            this.showOnMap.Id = "showOnMap";
            this.showOnMap.ToolTip = null;
            this.showOnMap.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.showOnMap_Execute);
            // 
            // ShowMapView
            // 
            this.ShowMapView.Caption = "Show Map View";
            this.ShowMapView.Category = "Notifications";
            this.ShowMapView.ConfirmationMessage = null;
            this.ShowMapView.Id = "ShowMapView";
            this.ShowMapView.ToolTip = null;
            this.ShowMapView.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ShowMapView_Execute);
            // 
            // WindowControllerLGSWeb
            // 
            this.Actions.Add(this.HelpWindow);
            this.Actions.Add(this.showOnMap);
            this.Actions.Add(this.ShowMapView);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction HelpWindow;
        private DevExpress.ExpressApp.Actions.SimpleAction showOnMap;
        private DevExpress.ExpressApp.Actions.SimpleAction ShowMapView;
    }
}
