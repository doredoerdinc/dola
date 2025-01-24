using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Web.Routing;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using DevExpress.Web;
using DevExpress.ExpressApp.Web.Controls;
using System.Web.UI.WebControls;
using LgsLib.Base.Web;
using DevExpress.XtraReports.Security; 
using System.Data;
using DevExpress.Security.Resources;

namespace dola.Web {
    public class Global : System.Web.HttpApplication {
        public Global() {
            InitializeComponent();
        }
        protected void Application_Start(Object sender, EventArgs e) {
             RouteTable.Routes.RegisterXafRoutes();

            DevExpress.Security.Resources.AccessSettings.ReportingSpecificResources.SetRules(SerializationFormatRule.Allow(DevExpress.XtraReports.UI.SerializationFormat.Code, DevExpress.XtraReports.UI.SerializationFormat.Xml));

            DevExpress.ExpressApp.BaseObjectSpace.ThrowExceptionForNotRegisteredEntityType = true;
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
           // ErrorHandling.Instance.NeedToCacheErrorInfo += Instance_NeedToCacheErrorInfo;
#if EASYTEST
            DevExpress.ExpressApp.Web.TestScripts.TestScriptsManager.EasyTestEnabled = true;
#endif
        }
        protected void Session_Start(Object sender, EventArgs e) {
            Tracing.Initialize();
            DevExpress.ExpressApp.Web.WebApplication.OptimizationSettings.AllowFastProcessListViewRecordActions = true;

           // DevExpress.Security.Resources.AccessSettings.ReportingSpecificResources.SetRules(SerializationFormatRule.Allow(DevExpress.XtraReports.UI.SerializationFormat.Xml));
            WebApplication.SetInstance(Session, new dolaAspNetApplication());
            SecurityStrategy security = (SecurityStrategy)WebApplication.Instance.Security;
            security.RegisterEFAdapterProviders();
            WebApplication.Instance.Settings.DefaultVerticalTemplateContentPath ="DefaultLGS.ascx";
            DefaultLGS.ClearSizeLimit();
        
            // DevExpress.ExpressApp.Web.Templates.DefaultVerticalTemplateContentNew.ClearSizeLimit();
            DevExpress.ExpressApp.Web.WebApplication.OptimizationSettings.LockRecoverViewStateOnNavigationCallback = false;
            WebApplication.Instance.SwitchToNewStyle();
            if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
       
            if (System.Diagnostics.Debugger.IsAttached && WebApplication.Instance.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                WebApplication.Instance.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif

            WebApplication.Instance.Setup();
            WebApplication.Instance.SetFormattingCulture("tr-TR");
             
            WebApplication.Instance.Start();
        } 
        protected void Application_BeginRequest(Object sender, EventArgs e) {
             
        }
        protected void Application_EndRequest(Object sender, EventArgs e) {
             
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
             
        }
        protected void Application_Error(Object sender, EventArgs e) {
           
            ErrorHandling.Instance.ProcessApplicationError(); 

        }
       
        protected void Session_End(Object sender, EventArgs e) {
            
            WebApplication.LogOff(Session);
            WebApplication.DisposeInstance(Session);
        }
        protected void Application_End(Object sender, EventArgs e) {
        }
        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
