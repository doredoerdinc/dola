using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.Validation;
using System.Linq;
using System;
using LgsLib.Base;
using LgsLib.Base.PermissionPolicy;
using System.ComponentModel.DataAnnotations.Schema;

namespace dola.Module
{
   public class UserDola:User
    {
        WorkingArea _WorkingArea;
        public virtual WorkingArea WorkingArea
        {
            get { return _WorkingArea; }
            set { _WorkingArea = value; }
        }

    }
}
