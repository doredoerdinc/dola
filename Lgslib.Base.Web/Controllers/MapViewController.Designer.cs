namespace Lgslib.Base.Web
{
    partial class MapViewController
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
            this.ShowOnMap = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // ShowOnMap
            // 
            this.ShowOnMap.Caption = "Show On Map";
            this.ShowOnMap.ConfirmationMessage = null;
            this.ShowOnMap.Id = "ShowOnMap";
            this.ShowOnMap.ToolTip = null;
            this.ShowOnMap.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ShowOnMap_Execute);
            // 
            // MapViewController
            // 
            this.Actions.Add(this.ShowOnMap);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction ShowOnMap;
    }
}
