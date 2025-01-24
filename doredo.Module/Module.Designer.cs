using LgsLib.Base.PermissionPolicy;
using LgsLib.Base;
namespace dola.Module {
	partial class dolaModule {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            // 
            // dolaModule
            // 
            this.AdditionalExportedTypes.Add(typeof(LgsLib.Base.PermissionPolicy.User));
            this.AdditionalExportedTypes.Add(typeof(LgsLib.Base.PermissionPolicy.Role));
            this.AdditionalExportedTypes.Add(typeof(LgsLib.Base.ModelDifference));
            this.AdditionalExportedTypes.Add(typeof(LgsLib.Base.ModelDifferenceAspect));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.SystemModule.SystemModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.ValidationModule));
			this.RequiredModuleTypes.Add(typeof(LgsLib.StateMachine.StateMachineModule));

		}

		#endregion
	}
}
