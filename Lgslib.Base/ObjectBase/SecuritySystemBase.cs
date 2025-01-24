using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LgsLib.Base.PermissionPolicy;

namespace LgsLib.Base
{
   public abstract class SecuritySystemBase
    {
        public static Warehouse Warehouse
        {
            get;set;
        }
           
        public static User  SUser
        {
            get;set;
        }



        public static string MainCompanyCode { get; set; }
        public static string AdressGLN { get; set; }
        public static string MainCompanyPortalKey { get; set; }
        public static string Service { get; set; }
        public static string PasssWord { get; set; }
        public static string User { get; set; }


    }
}
