using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;


namespace LgsLib.Web.Base
{
    public  static class ApplicationConf
    {
        public static bool StoreApplicationModelInDatabase { get; private set; }
        public static bool IsAdminMode { get; private set; }

        private static bool _IsDebugMode;
        public static bool IsDebugMode { get { return _IsDebugMode && System.Diagnostics.Debugger.IsAttached; } }

        public static XafApplication Instance { get; set; }
       // public static ConnectionInfo ConnectionInfo { get; private set; }


        //public static NLog.Logger Logger = NLog.LogManager.GetLogger("ApplicationConf");
            
        public static class ConfigKeys
        {
            public const string AskAdminMode = "EXAskAdminMode";
            public const string AskConnection = "EXAskConnection";
            public const string SchemaUpdate = "EXSchemaUpdate";
            public const string DevartMonitorActive = "EXDevartMonitorActive";
            public const string IsAsyncServerMode = "EXAsyncServerMode";
        }
          
    }
}
