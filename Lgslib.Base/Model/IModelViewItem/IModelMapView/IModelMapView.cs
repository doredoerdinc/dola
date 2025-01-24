using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Model;
using DevExpress.Data;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp;

namespace LgsLib.Base
{
    [DomainComponent]
    [KeyProperty("Name")]
    public interface IModelMapView : IModelNode
    {
        [Category(ModelCategory.ViewList)]
        [DataSourceProperty("Application.ActionDesign.Actions")]
        IModelAction Action { get; set; }
    }
    

  
}

   