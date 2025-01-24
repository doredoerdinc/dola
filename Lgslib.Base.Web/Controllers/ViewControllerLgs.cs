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
using LgsLib.Base;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Web;

namespace LgsLib.Base.Web
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ViewControllerWebLGS : ViewController
    {
        public ViewControllerWebLGS()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
          
            // Perform various tasks depending on the target View.
        }
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned(); 
        }
         
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
         
        //protected void PrintReport(IReportDataV2 reportData)
        //{
        //    string reportContainerHandle = ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
        //    ((WebWindow)WebApplication.Instance.MainWindow).RegisterStartupScript(
        //        "InstantPrintReport", GetPrintingScript(reportContainerHandle), overwrite: true);
        //}
//        private string GetPrintingScript(string reportContainerHandle)
//        {
//            return string.Format(@"
//        if(!ASPx.Browser.Edge) {{
//            var iframe = document.getElementById('reportout');
//            if (iframe != null) {{
//                document.body.removeChild(iframe);
//            }}
//            iframe = document.createElement('iframe');
//            iframe.setAttribute('id', 'reportout');
//            iframe.style.width = 0;
//            iframe.style.height = 0;
//            iframe.style.border = 0;
//            document.body.appendChild(iframe);
//            document.getElementById('reportout').contentWindow.location = 
//'InstantPrintReport.aspx?reportContainerHandle={0}';
//            }} else {{
//                window.open('InstantPrintReport.aspx?reportContainerHandle={0}', '_blank');
//        }}
//        ", reportContainerHandle);
//        }

    
    }
}
